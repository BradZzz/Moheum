using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{

  public static PanelManager instance;
  public GameObject glossaryObj;

  private Location currentLocation;
  private Glossary glossary;

  GameObject back, btnPnl, monPnl, itemPnl;

  List<SkillMeta> skills;
  List<SkillMeta> Mskills;

  List<GameObject> skillPanels;
  List<GameObject> MskillPanels;

  List<GameObject> skillLoaders;
  List<GameObject> MskillLoaders;

  List<GameObject> skillTxts;

  Text[] txt;
  Text[] dtls;
  Text[] Mtxt;
  Text[] Mdtls;
  GameObject[] MSklls;

  public GameObject critialHealthPanel;
  public GameObject[] critialHealthPanelSkills;

  GameObject[] Items;
  GameObject[] itemTxt;
  GameObject[] rMonsters;
  GameObject[] rMonstersDead;
  GameObject[] rMonstersLvl;
  GameObject[] rMonstersHealth;

  int activeSkill;

  //public static bool WILD_ENCOUNTER = true;
  //public static int E_MONSTER = 2;

  private bool isCritical;
  private bool isUsingItem;
  private int itemPos;
  private int currentMonster;
  //private MonsterMeta eMeta;
  private MonsterMeta wildMeta;

  private AdventureMeta adventure;

  private enum Location
  {
    Main, Action, Swap, Item, Run, None
  }

  public List<SkillMeta> getPlayerSkills()
  {
    return skills;
  }

  public List<SkillMeta> getComputerSkills()
  {
    return Mskills;
  }

  public void click(int pos)
  {
    Debug.Log("pos: " + pos.ToString());
    moveLocation(pos);
    loadLocation();
    SFXManager.instance.PlaySFX(Clip.Select);
  }

  private PlayerRosterMeta getTrainerMonster()
  {
    Debug.Log("getTrainerMonster");
    if (adventure.isTrainerEncounter)
    {
      Debug.Log("adventure.isTrainerEncounter");
      foreach (PlayerRosterMeta mons in adventure.trainer.roster)
      {
        Debug.Log("Monster: " + mons.ToString());
        if (mons.curHealth > 0)
        {
          Debug.Log("Returning");
          return mons;
        }
      }
    }
    return null;
  }

  //Iterate the fader transparency to 100%
  //The new monster is getting double turns when it comes out...
  IEnumerator KillMonster()
  {
    Debug.Log("Monster Killed!");

    iTween.ValueTo(GameObject.Find("MMonsterImg"), iTween.Hash(
      "from", 1,
      "to", 0,
      "time", 1f,
      "onupdatetarget", gameObject,
      "onupdate", "FadeOverlayEnemy"));
    yield return new WaitForSeconds(1f);

    loadEnemyMonster();
    loadLocation();

    iTween.ValueTo(GameObject.Find("MMonsterImg"), iTween.Hash(
      "from", 0,
      "to", 1,
      "time", 1f,
      "onupdatetarget", gameObject,
      "onupdate", "FadeOverlayEnemy"));
    yield return new WaitForSeconds(1f);

    if (!BoardManager.instance.getPlayerTurn())
    {
      Debug.Log("KillMonster");
      yield return new WaitForSeconds(1f);
      StartCoroutine(BoardManager.instance.loopAIMovement());
    }

    yield return null;
  }

  //Iterate the fader transparency to 100%
  //The new monster is getting double turns when it comes out...
  IEnumerator KillPlayerMonster()
  {
    Debug.Log("Player Monster Killed!");

    iTween.ValueTo(GameObject.Find("HMonsterImg"), iTween.Hash(
      "from", 1,
      "to", 0,
      "time", 1f,
      "onupdatetarget", gameObject,
      "onupdate", "FadeOverlayPlayer"));
    yield return new WaitForSeconds(1f);

    loadHeroMonster();
    loadLocation();

    iTween.ValueTo(GameObject.Find("HMonsterImg"), iTween.Hash(
      "from", 0,
      "to", 1,
      "time", 1f,
      "onupdatetarget", gameObject,
      "onupdate", "FadeOverlayPlayer"));
  }

  public void FadeOverlayEnemy(float alpha)
  {
    //Debug.Log("FadeOverlay: " + alpha.ToString());
    Color fadeCol = GameObject.Find("MMonsterImg").GetComponent<Image>().color;
    fadeCol.a = alpha;
    GameObject.Find("MMonsterImg").GetComponent<Image>().color = fadeCol;
  }

  public void FadeOverlayPlayer(float alpha)
  {
    //Debug.Log("FadeOverlay: " + alpha.ToString());
    Color fadeCol = GameObject.Find("HMonsterImg").GetComponent<Image>().color;
    fadeCol.a = alpha;
    GameObject.Find("HMonsterImg").GetComponent<Image>().color = fadeCol;
  }

  IEnumerator MoveOn(AdventureMeta meta)
  {
    yield return new WaitForSeconds(.5f);
    GUIManager.instance.EndGame(true, false, true, meta);
    yield return null;
  }

  IEnumerator WaitForLevelUp(int exp)
  {
    yield return new WaitForSeconds(1f);
    int counter = 0;
    while (BoardManager.instance.IsThinking() && counter < 30)
    {
      yield return new WaitForSeconds(1f);
      counter++;
    }
    GUIManager.instance.LevelUp(adventure.roster[currentMonster], exp);
  }

  public PlayerRosterMeta getCurrentMonster(){
    if  (BoardManager.instance.getPlayerTurn()) {
      return adventure.roster[currentMonster];
    } else {
      if (adventure.isTrainerEncounter)
      {
        return getTrainerMonster();
      }
      else
      {
        return adventure.wild;
      }
    }
  }

  public void continueUpdate(){
    Debug.Log("continueUpdate");
    if (getTrainerMonster() == null)
    {
      if (adventure.isTrainerEncounter)
      {
        adventure.trainer.defeated = true;
      }
      BaseSaver.putAdventure(adventure);
      StartCoroutine(MoveOn(adventure));
      //GUIManager.instance.EndGame(true, false, true, adventure);
    }
    else
    {
      StartCoroutine(KillMonster());
    }
  }

  public void updateCurrent(PlayerRosterMeta meta){
    adventure.roster[currentMonster] = meta;
    /*Refresh skills if monster learned anything new*/
    loadSkills();
  }

  public void DelayedUpdateFromTile(bool playerDead){
    Debug.Log("UpdateTile");
    StartCoroutine(WaitUntilTileUpdate(playerDead));
  }

  public IEnumerator WaitUntilTileUpdate(bool playerDead)
  {
    Debug.Log("WaitUntilTileUpdate");
    int counter = 0;
    int checksPassed = 0;
    while (checksPassed < 6 && counter < 30)
    {
      yield return new WaitForSeconds(.25f);
      counter++;
      if (!BoardManager.instance.IsThinking())
      {
        checksPassed++;
      }
      else
      {
        checksPassed = 0;
      }
    }
    Debug.Log("Done WaitUntilTileUpdate");
    yield return new WaitForSeconds(1f);
    UpdateFromTile(playerDead);
  }

  public void UpdateFromTile(bool playerDead){
    Debug.Log("Update From Tile");

    PlayerRosterMeta enemyMonster;
    if (adventure.isTrainerEncounter) {
      enemyMonster = getTrainerMonster();
    } else {
      enemyMonster = adventure.wild;
    }
    while(GameObject.Find("MOverlay").GetComponent<Progress>().Updating() || GameObject.Find("HOverlay").GetComponent<Progress>().Updating())
    {

    }
    adventure.roster[currentMonster].curHealth = GameObject.Find("HOverlay").GetComponent<Progress>().progress;
    enemyMonster.curHealth = GameObject.Find("MOverlay").GetComponent<Progress>().progress;
    if (!playerDead) {
      //enemyMonster.curHealth = GameObject.Find("MOverlay").GetComponent<Progress>().progress;

      int addedExp = MonsterMeta.getExperience(enemyMonster, !adventure.isTrainerEncounter);
      //int[] lvlData = MonsterMeta.CalcLvl(adventure.roster[currentMonster],glossary.GetMonsterMain(adventure.roster[currentMonster].name).meta.lvlSpeed);
      //Debug.Log("Level Up Data: " + lvlData[0].ToString() + ":" + lvlData[1].ToString() + ":" + lvlData[2].ToString());
      GameObject.Find("HExpOverlay").GetComponent<ProgressExp>().UpdateProgress(addedExp);
      BoardManager.instance.emitStars(true);

      // You won!
      //Debug.Log("You won!");
      //adventure.roster[currentMonster].curHealth = GameObject.Find("HOverlay").GetComponent<Progress>().progress;
      //adventure.roster[currentMonster].exp += addedExp;
      int[] lvlData = MonsterMeta.CalcLvl(adventure.roster[currentMonster], glossary.GetMonsterMain(adventure.roster[currentMonster].name).meta.lvlSpeed);
      if (lvlData[1] + addedExp >= lvlData[2]) {
        StartCoroutine(WaitForLevelUp(addedExp));
      } else {
        adventure.roster[currentMonster].exp += addedExp;
        continueUpdate();
      }
    } else {
      Debug.Log("Player Lost Monster");

      //enemyMonster.curHealth = GameObject.Find("MOverlay").GetComponent<Progress>().progress;
      //GameObject.Find("HOverlay").GetComponent<Progress>().UpdateProgress(0);
      //adventure.roster[currentMonster].curHealth = 0;
      bool monstersLeft = false;
      for (int i = 0; i < adventure.roster.Length; i++) {
        if (adventure.roster[i].curHealth > 0) {
          monstersLeft = true;
          currentMonster = i;
          StartCoroutine(KillPlayerMonster());
//          loadHeroMonster();
//          loadLocation();
          break;
        }
      }
      if (!monstersLeft){
        BaseSaver.putAdventure(adventure);
        GUIManager.instance.EndGame(false, false, false, adventure);
      }
    }

    updateUI(adventure.trainer);
  }

  //private void saveAdventure(){
  //  PlayerRosterMeta vComp = adventure.roster[currentMonster];
  //  vComp.curHealth = GameObject.Find("HOverlay").GetComponent<Progress>().progress;
  //  adventure.roster[currentMonster] = vComp;
  //}

  public void swap(int pos)
  {
    if (!isUsingItem)
    {
      Debug.Log("swap: " + pos.ToString());
      adventure.roster[currentMonster].curHealth = GameObject.Find("HOverlay").GetComponent<Progress>().progress;
      if (pos < adventure.roster.Length && adventure.roster[pos].curHealth > 0 && pos != currentMonster)
      {
        currentMonster = pos;
        loadHeroMonster();
        SFXManager.instance.PlaySFX(Clip.Select);
        currentLocation = Location.Action;
        loadLocation();
      }
    } else {
      /*
       * give monster the item bonuses
       * subtract from inventory
       * close the monster menu
       * update ui
       */
      TreasureMain itemToUse = glossaryObj.GetComponent<Glossary>().GetItem(getItem(itemPos));
      if (GameUtilities.CanUseItem(itemToUse, adventure, pos, skills)) {
        adventure = GameUtilities.UseItem(itemToUse, adventure, pos, skills);
        adventure.AddToTreasureList(getItem(itemPos), -1);
      }
      monPnl.SetActive(false);
      isUsingItem = false;
      GameObject.Find("HOverlay").GetComponent<Progress>().UpdateProgress(adventure.roster[currentMonster].curHealth);
      loadLocation();
      updateUI(adventure.trainer);
    }
  }

  public void item(int pos)
  {
    /*
     * When an item is clicked, the monster panel needs to open up next
     * swap needs to be caught if the computer is waiting for an item input
     */

    isUsingItem = true;
    monPnl.SetActive(true);
    setSwapMonsters();
    itemPos = pos;
  }

  // Use this for initialization
  void Start () {
    GUIManager.instance.StartIntroduction(glossaryObj.GetComponent<Glossary>());

    currentMonster = 0;
    isCritical = false;
    isUsingItem = false;
    adventure = BaseSaver.getAdventure();
    for (int i = 0; i < adventure.roster.Length; i++){
      if (adventure.roster[i].curHealth > 0){
        currentMonster = i;
        break;
      }
    }

    //Debug.Log("Adventurer Pos: " + adventure.playerPos.ToString());
    foreach(PlayerRosterMeta meta in adventure.roster){
      Debug.Log("Monster: " + meta.name.ToString());
    }

    instance = GetComponent<PanelManager>();
    glossary = glossaryObj.GetComponent<Glossary> ();

    if (!adventure.isTrainerEncounter) {
      wildMeta = glossary.GetMonsterMain(adventure.wild.name).meta;
    } else {
      wildMeta = null;
    }
    //eMeta = glossary.GetMonsterMain(E_MONSTER).meta;
    //eMeta.currHealth = eMeta.mxHealth;
    //eMeta.currHealth = 8;

    //currentLocation = Location.Action;
    //setActiveSkill(-1);

    //critialHealthPanel = GameObject.Find("MCatchPanelHolder");
    //critialHealthPanelSkills = new GameObject[] { GameObject.Find("MCatchSkillA"), GameObject.Find("MCatchSkillB") };

    Items = new GameObject[6];
    Items[0] = GameObject.Find("IImg1");
    Items[1] = GameObject.Find("IImg2");
    Items[2] = GameObject.Find("IImg3");
    Items[3] = GameObject.Find("IImg4");
    Items[4] = GameObject.Find("IImg5");
    Items[5] = GameObject.Find("IImg6");

    itemTxt = new GameObject[6];
    itemTxt[0] = GameObject.Find("IText1");
    itemTxt[1] = GameObject.Find("IText2");
    itemTxt[2] = GameObject.Find("IText3");
    itemTxt[3] = GameObject.Find("IText4");
    itemTxt[4] = GameObject.Find("IText5");
    itemTxt[5] = GameObject.Find("IText6");

    rMonsters = new GameObject[6];
    rMonsters[0] = GameObject.Find("MImg1");
    rMonsters[1] = GameObject.Find("MImg2");
    rMonsters[2] = GameObject.Find("MImg3");
    rMonsters[3] = GameObject.Find("MImg4");
    rMonsters[4] = GameObject.Find("MImg5");
    rMonsters[5] = GameObject.Find("MImg6");

    rMonstersDead = new GameObject[6];
    rMonstersDead[0] = GameObject.Find("MDead1");
    rMonstersDead[1] = GameObject.Find("MDead2");
    rMonstersDead[2] = GameObject.Find("MDead3");
    rMonstersDead[3] = GameObject.Find("MDead4");
    rMonstersDead[4] = GameObject.Find("MDead5");
    rMonstersDead[5] = GameObject.Find("MDead6");

    rMonstersHealth = new GameObject[6];
    rMonstersHealth[0] = GameObject.Find("HealthTxt1");
    rMonstersHealth[1] = GameObject.Find("HealthTxt2");
    rMonstersHealth[2] = GameObject.Find("HealthTxt3");
    rMonstersHealth[3] = GameObject.Find("HealthTxt4");
    rMonstersHealth[4] = GameObject.Find("HealthTxt5");
    rMonstersHealth[5] = GameObject.Find("HealthTxt6");

    rMonstersLvl = new GameObject[6];
    rMonstersLvl[0] = GameObject.Find("LvlTxt1");
    rMonstersLvl[1] = GameObject.Find("LvlTxt2");
    rMonstersLvl[2] = GameObject.Find("LvlTxt3");
    rMonstersLvl[3] = GameObject.Find("LvlTxt4");
    rMonstersLvl[4] = GameObject.Find("LvlTxt5");
    rMonstersLvl[5] = GameObject.Find("LvlTxt6");

    txt = new Text[4];
    txt [0] = GameObject.Find ("HClick1Txt").GetComponent<Text> ();
    txt [1] = GameObject.Find ("HClick2Txt").GetComponent<Text> ();
    txt [2] = GameObject.Find ("HClick3Txt").GetComponent<Text> ();
    txt [3] = GameObject.Find ("HClick4Txt").GetComponent<Text> ();

    dtls = new Text[4];
    dtls [0] = GameObject.Find ("HClick1DtlTxt").GetComponent<Text> ();
    dtls [1] = GameObject.Find ("HClick2DtlTxt").GetComponent<Text> ();
    dtls [2] = GameObject.Find ("HClick3DtlTxt").GetComponent<Text> ();
    dtls [3] = GameObject.Find ("HClick4DtlTxt").GetComponent<Text> ();

    Mtxt = new Text[4];
    Mtxt[0] = GameObject.Find("MClick1Txt").GetComponent<Text>();
    Mtxt[1] = GameObject.Find("MClick2Txt").GetComponent<Text>();
    Mtxt[2] = GameObject.Find("MClick3Txt").GetComponent<Text>();
    Mtxt[3] = GameObject.Find("MClick4Txt").GetComponent<Text>();

    MSklls = new GameObject[4];
    MSklls[0] = GameObject.Find("MSkillPanel1");
    MSklls[1] = GameObject.Find("MSkillPanel2");
    MSklls[2] = GameObject.Find ("MSkillPanel3");
    MSklls[3] = GameObject.Find ("MSkillPanel4");

    Mdtls = new Text[4];
    Mdtls [0] = GameObject.Find ("MClick1DtlTxt").GetComponent<Text> ();
    Mdtls [1] = GameObject.Find ("MClick2DtlTxt").GetComponent<Text> ();
    Mdtls [2] = GameObject.Find ("MClick3DtlTxt").GetComponent<Text> ();
    Mdtls [3] = GameObject.Find ("MClick4DtlTxt").GetComponent<Text> ();

    back = GameObject.Find ("BackBtn");
    btnPnl = GameObject.Find ("HButtonPanel");
    monPnl = GameObject.Find ("HMonsterPanel");
    itemPnl = GameObject.Find("HItemPanel");

    skills = new List<SkillMeta> ();
    Mskills = new List<SkillMeta> ();

    skillPanels = new List<GameObject> ();
    MskillPanels = new List<GameObject> ();

    skillLoaders = new List<GameObject> ();
    MskillLoaders = new List<GameObject> ();

    skillTxts = new List<GameObject> ();

    loadSkills ();

    initSkillOverlays ();

    //List<string> actions = new List<string> ();
    //for(int i = 0; i < Mskills.Count; i++){
    //  Mtxt [i].text = Mskills[i].name;
    //  Mdtls[i].text = getEffectsString(Mskills[i].effects);
    //}

    for (int i = 0; i < 4; i++) {
      if (i < Mskills.Count) {
        //GameObject.Find("MSkillPanel" + (i + 1).ToString()).SetActive(false);
        //Mtxt[i].gameObject.SetActive(false);
        //Mdtls[i].gameObject.SetActive(false);
        MSklls[i].gameObject.SetActive(true);
        Mtxt[i].gameObject.SetActive(true);
        Mdtls[i].gameObject.SetActive(true);
        updatePanels ('M', i);
      } else {
        //GameObject.Find("MSkillPanel" + (i + 1).ToString()).SetActive (false);
        MSklls[i].gameObject.SetActive(false);
        Mtxt [i].gameObject.SetActive (false);
        Mdtls [i].gameObject.SetActive (false);
      }
    }

    //loadSkillsOverlays();

    for (int i = 1; i < 5; i++) {
      string id = "MClick" + i.ToString ();
      GameObject.Find (id).GetComponent<Outline> ().enabled = false;
    }

    currentLocation = Location.Action;
    loadLocation ();
    setActiveSkill(-1);

    updateUI(adventure.trainer);
	}

  void updateUI(NPCMeta meta)
  {
    if (meta != null)
    {
      GameObject.Find("MTrainerResPnl").GetComponent<TrainerUpdateController>().updateMonsterUI(meta.roster, 
        adventure.isTrainerEncounter ? TrainerUpdateController.TrainerType.Trainer : TrainerUpdateController.TrainerType.Wild);
      GameObject.Find("HTrainerResPnl").GetComponent<TrainerUpdateController>().updateMonsterUI(adventure.roster, TrainerUpdateController.TrainerType.Player);
    }
  }

  public static string getEffectsString(SkillEffect[] effects){
    string buffer = "";
    foreach(SkillEffect effect in effects){
      switch(effect.effect){
      case SkillEffect.Effect.Change:
        buffer += ((SkillEffect.ChangeSkill)effect).effectString ();
        break;
      case SkillEffect.Effect.ChangeSome:
        buffer += ((SkillEffect.ChangeSomeSkill)effect).effectString ();
        break;
      case SkillEffect.Effect.Damage:
        buffer += ((SkillEffect.DamageSkill)effect).effectString ();
        break;
      case SkillEffect.Effect.Destroy:
        buffer += ((SkillEffect.DestroySkill)effect).effectString ();
        break;
      case SkillEffect.Effect.DestroySome:
        buffer += ((SkillEffect.DestroySomeSkill)effect).effectString ();
        break;
      case SkillEffect.Effect.Heal:
        buffer += ((SkillEffect.HealSkill)effect).effectString ();
        break;
      case SkillEffect.Effect.Poison:
        buffer += ((SkillEffect.PoisonSkill)effect).effectString ();
        break;
      case SkillEffect.Effect.Poke:
        buffer += ((SkillEffect.PokeSkill)effect).effectString();
        break;
      case SkillEffect.Effect.Reset:
        buffer += ((SkillEffect.ResetSkill)effect).effectString ();
        break;
      case SkillEffect.Effect.Sabotage:
        buffer += ((SkillEffect.SabotageSkill)effect).effectString();
        break;
      case SkillEffect.Effect.Slice:
        buffer += ((SkillEffect.SliceSkill)effect).effectString();
        break;
      case SkillEffect.Effect.xTurn:
        buffer += ((SkillEffect.xTurnSkill)effect).effectString ();
        break;
      }
      buffer += "\n";
    }
    return buffer.Remove (buffer.Length - 1);
  }

  public void setActiveSkill(int idx){
    Debug.Log("setActiveSkill: " + idx.ToString());
    activeSkill = idx;
    if (BoardManager.instance.getPlayerTurn ()) {
      if (activeSkill != -1 && !skills [activeSkill].hasReq ()) {
        activeSkill = -1;
      }
      for (int i = 1; i < 5; i++) {
        string id = "HClick" + i.ToString ();
        if (i == (activeSkill + 1)) {
          GameObject.Find (id).GetComponent<Outline> ().enabled = true;
        } else {
          GameObject.Find (id).GetComponent<Outline> ().enabled = false;
        }
      }
    } else {
      for (int i = 1; i < 5; i++) {
        string id = "MClick" + i.ToString ();
        if (i == (activeSkill + 1)) {
          GameObject.Find (id).GetComponent<Outline> ().enabled = true;
        } else {
          GameObject.Find (id).GetComponent<Outline> ().enabled = false;
        }
      }
    }
  }

  public void FillSkills () {
    foreach (SkillMeta skill in skills)
    {
      skill.req1.has = skill.req1.req;
    }
    loadSkillsOverlays();
  }

  public SkillEffect[] getActiveSkill(){
    if (activeSkill == -1) {
      return null;
    }
    if (BoardManager.instance.getPlayerTurn ()) {
      return skills [activeSkill].effects;
    } else {
      return Mskills [activeSkill].effects;
    }
  }

  public void useSkill(){
    if (activeSkill != -1) {
      if (BoardManager.instance.getPlayerTurn ()) {
        if (BoardManager.instance.hinty != null) { StopCoroutine(BoardManager.instance.hinty); }
        BoardManager.instance.hinty = BoardManager.instance.waitHint();
        StartCoroutine(BoardManager.instance.hinty);
        skills [activeSkill].req1.has = 0;
        skills [activeSkill].req2.has = 0;

        StartCoroutine(FlashButton(GameObject.Find("HClick" + (activeSkill + 1).ToString())));

        setActiveSkill (-1);
        Debug.Log ("useSkill player");
        loadSkillsOverlays ();
      } else {
        Mskills [activeSkill].req1.has = 0;
        Mskills [activeSkill].req2.has = 0;

        StartCoroutine(FlashButton(GameObject.Find("MClick" + (activeSkill + 1).ToString())));

        setActiveSkill (-1);
        Debug.Log ("useSkill comp");
        loadSkillsOverlays ();
      }
    }
  }

  IEnumerator FlashButton(GameObject button){
    Color baseColor = button.GetComponent<Image>().color;
    Color newColor = new Color(253/(float)255, 238/ (float)255, 85/ (float)255);
    float wait = .2f;

    button.GetComponent<Image>().color = newColor;
    yield return new WaitForSeconds(wait);
    button.GetComponent<Image>().color = baseColor;
    yield return new WaitForSeconds(wait);
    button.GetComponent<Image>().color = newColor;
    yield return new WaitForSeconds(wait);
    button.GetComponent<Image>().color = baseColor;
  }

  //This function is called if a 4-5 match occurs during the game
  //Adds a bonus to the affected player
  public void addBonus(GameObject[] matches){
    if (matches != null && matches.Length >= 3) {
      TileMeta.GemType pivotGem = matches[0].GetComponent<Tile>().type.type;
      bool fiver = matches.Length == 4;
      if (pivotGem != TileMeta.GemType.Fight) {
        foreach (TileMeta.GemType gem in Enum.GetValues(typeof(TileMeta.GemType))){
          if (/*gem != TileMeta.GemType.Fight && */gem != TileMeta.GemType.None) {
            int pride = (int) getCurrentMonster().pride;
            //- [ ] Pride => Receive bigger bonuses when matching 4/5 gems
            /*
             * 4-Match: 3 to matched gem. 1 to other gems
             * 5-Match: 5 to matched gem. 3 to other gems
             */
            int amount = gem == pivotGem ? 3 + pride : 1 + pride;
            if (fiver) {
              amount = gem == pivotGem ? 5 + pride : 3 + pride;
            }
            for (int i = 0; i < amount; i++)
            {
              addGem(gem);
            }
          }
        }
      }
    }
  }

  public void removeGems(int amount){
    foreach (SkillMeta skill in BoardManager.instance.getPlayerTurn() ? Mskills : skills)
    {
      foreach (SkillReq req in new SkillReq[] { skill.req1, skill.req2 })
      {
        req.has -= amount;
        req.has = req.has < 0 ? 0 : req.has;
      }
    }
    loadSkillsOverlays();
  }

  public void addGem(TileMeta.GemType type){
    foreach(SkillMeta skill in BoardManager.instance.getPlayerTurn() ? skills : Mskills){
      foreach(SkillReq req in new SkillReq[]{skill.req1,skill.req2}){
        if (req.gem == type && req.has < req.req) {
          req.has++;
        }
      }
    }
    if (BoardManager.instance.getPlayerTurn() && isCritical) {
      foreach(SkillReq req in wildMeta.catchReq){
        if (req.gem == type && req.has < req.req)
        {
          req.has++;
        }
      }
    }
    loadSkillsOverlays ();
    if (isCritical) 
    {
      updateCatchReq();
    }
  }

  SkillReq createReq(TileMeta.GemType gem, int has, int req){
    SkillReq retReq = new SkillReq ();
    retReq.gem = gem;
    retReq.has = has;
    retReq.req = req;
    return retReq;
  }


  void loadSkills(){
    loadHeroMonster();
    loadEnemyMonster();
  }

  void loadHeroMonster(){
    int myMonster = currentMonster;

    MonsterMeta playerMonster = glossary.GetMonsterMain(adventure.roster[myMonster].name).meta;
    GameObject.Find("HMonsterImg").GetComponent<Image>().sprite = glossary.GetMonsterImage(adventure.roster[myMonster].name);
    GameObject.Find("HMonsterImg").GetComponent<CharacterActionController>().RemoveBuff();
    GameObject.Find("HMonsterName").GetComponent<Text>().text = adventure.roster[myMonster].nickname.Length > 0 ? 
      adventure.roster[myMonster].nickname : adventure.roster[myMonster].name;
    GameObject.Find("HOverlay").GetComponent<Progress>().updateHealth(adventure.roster[myMonster].maxHealth);
    GameObject.Find("HOverlay").GetComponent<Progress>().UpdateProgress(adventure.roster[myMonster].curHealth);

    skills = new List<SkillMeta>(GameUtilities.parseSkills(adventure.roster[myMonster].skills, glossary));
    GameObject.Find("HExpOverlay").GetComponent<ProgressExp>().UpdateExperience(glossary.GetMonsterMain(playerMonster.name).meta.lvlSpeed,adventure.roster[myMonster]);
  }

  void loadEnemyMonster(){
    PlayerRosterMeta enemyMonster = getTrainerMonster();

    if (!adventure.isTrainerEncounter) {
      enemyMonster = adventure.wild;
    }

    if (enemyMonster != null)
    {
      Debug.Log("Loading Enemy...");
      Debug.Log(enemyMonster.ToString());

      GameObject.Find("MMonsterName").GetComponent<Text>().text = enemyMonster.name;
      GameObject.Find("MOverlay").GetComponent<Progress>().updateHealth(enemyMonster.maxHealth);
      GameObject.Find("MOverlay").GetComponent<Progress>().UpdateProgress(enemyMonster.curHealth);
      GameObject.Find("MMonsterImg").GetComponent<Image>().sprite = glossary.GetMonsterImage(enemyMonster.name);
      GameObject.Find("MMonsterImg").GetComponent<CharacterActionController>().RemoveBuff();

      GameObject.Find("MLvlTxt").GetComponent<Text>().text = enemyMonster.lvl.ToString();

      Mskills = new List<SkillMeta>(GameUtilities.parseSkills(enemyMonster.skills, glossary));

      List<string> actions = new List<string>();
      for (int i = 0; i < Mskills.Count; i++)
      {
        Mtxt[i].text = Mskills[i].name;
        Mdtls[i].text = getEffectsString(Mskills[i].effects);
      }

      for (int i = 0; i < 4; i++)
      {
        if (i < Mskills.Count)
        {
          MSklls[i].gameObject.SetActive(true);
          Mtxt[i].gameObject.SetActive(true);
          Mdtls[i].gameObject.SetActive(true);
          updatePanels('M', i);
        }
        else
        {
          MSklls[i].gameObject.SetActive(false);
          Mtxt[i].gameObject.SetActive(false);
          Mdtls[i].gameObject.SetActive(false);
        }
      }
    }
  }

  void initSkillOverlays(){
    for(int i = 0; i < 4; i++){
      string dirStr = (i + 1).ToString ();
      GameObject skilla = GameObject.Find ("HSkill" + dirStr + "A");
      GameObject skillb = GameObject.Find ("HSkill" + dirStr + "B");

      skillPanels.Add (skilla);
      skillPanels.Add (skillb);

      GameObject mskilla = GameObject.Find ("MSkill" + dirStr + "A");
      GameObject mskillb = GameObject.Find ("MSkill" + dirStr + "B");

      MskillPanels.Add (mskilla);
      MskillPanels.Add (mskillb);
    }
  }

  void updatePanels(char prefix, int i){
    List<TileMeta.GemType> gemTypes = new List<TileMeta.GemType>(BoardManager.instance.gemTypes);
    Sprite[] loaders = BoardManager.instance.gemLoaders;
    Sprite[] gems = BoardManager.instance.characters.ToArray();

    string dirStr = (i + 1).ToString ();
    GameObject overlaya = GameObject.Find (prefix + "Overlay" + dirStr + "A");
    GameObject overlayb = GameObject.Find (prefix + "Overlay" + dirStr + "B");
//
//    skillLoaders.Add (overlaya);
//    skillLoaders.Add (overlayb);

    GameObject gema = GameObject.Find (prefix + "Gem" + dirStr + "A");
    GameObject gemb = GameObject.Find (prefix + "Gem" + dirStr + "B");

    GameObject skilltxta = GameObject.Find (prefix + "SkillTxt" + dirStr + "A");
    GameObject skilltxtb = GameObject.Find (prefix + "SkillTxt" + dirStr + "B");

//    skillTxts.Add (skilltxta);
//    skillTxts.Add (skilltxtb);

    SkillMeta skill;

    if (prefix == 'H') {
      skill = skills [i];
    } else {
      skill = Mskills [i];
    }

    int idxA = gemTypes.IndexOf (skill.req1.gem);
    int idxB = gemTypes.IndexOf (skill.req2.gem);

    Sprite loaderA = loaders [idxA];
    Sprite loaderB = loaders [idxB];

    Sprite gemSpriteA = gems [idxA];
    Sprite gemSpriteB = gems [idxB];

//    Debug.Log (prefix + "Overlay" + dirStr + "A");

    overlaya.GetComponent<Image> ().sprite = loaderA;
    overlaya.GetComponent<Image> ().fillAmount = (float)skill.req1.has / (float)skill.req1.req;
    overlayb.GetComponent<Image> ().sprite = loaderB;
    overlayb.GetComponent<Image> ().fillAmount = (float)skill.req2.has / (float)skill.req2.req;

    gema.GetComponent<Image> ().sprite = gemSpriteA;
    gemb.GetComponent<Image> ().sprite = gemSpriteB;

    skilltxta.GetComponent<Text> ().text = skill.req1.has + " / " + skill.req1.req;
    skilltxtb.GetComponent<Text> ().text = skill.req2.has + " / " + skill.req2.req;
  }

  void loadSkillsOverlays(){
    List<TileMeta.GemType> gemTypes = new List<TileMeta.GemType>(BoardManager.instance.gemTypes);
    Sprite[] loaders = BoardManager.instance.gemLoaders;
    Sprite[] gems = BoardManager.instance.characters.ToArray();
    if (currentLocation == Location.Action) {
      for (int i = 0; i < 4; i++) {
        if (i < skills.Count) {
          txt [i].gameObject.SetActive (true);
          dtls [i].gameObject.SetActive (true);
          updatePanels ('H', i);
        } else {
          skillPanels [2 * i].SetActive (false);
          skillPanels [(2 * i) + 1].SetActive (false);
          txt [i].gameObject.SetActive (false);
          dtls [i].gameObject.SetActive (false);
        }
        if (i < Mskills.Count)
        {
          MSklls[i].gameObject.SetActive(true);
          Mtxt[i].gameObject.SetActive(true);
          Mdtls[i].gameObject.SetActive(true);
          updatePanels('M', i);
        }
        else
        {
          //GameObject.Find("MSkillPanel" + (i + 1).ToString()).SetActive (false);
          MSklls[i].gameObject.SetActive(false);
          Mtxt[i].gameObject.SetActive(false);
          Mdtls[i].gameObject.SetActive(false);
        }
      }
    }
  }

  void showSkills(){
    foreach (GameObject pnl in skillPanels) {
      pnl.SetActive (true);
    }
  }

  void hideSkills(){
    foreach(GameObject pnl in skillPanels) {
      pnl.SetActive (false);
    }
  }

  public void playerActed(){
    Debug.Log("playerActed");
    if (!adventure.isTrainerEncounter)
    {
      Debug.Log("Wild Encounter Critical");
      //critialHealthPanel
      //GameObject.Find("HOverlay").GetComponent<Progress>().progress
      float progress = (float)GameObject.Find("MOverlay").GetComponent<Progress>().progress;
      float max = (float)GameObject.Find("MOverlay").GetComponent<Progress>().MAX_HEALTH;

      if (progress / max <= (wildMeta.mxHealth >= 10 ? .25f : .5f) && !isCritical)
      {
        isCritical = true;
        critialHealthPanel.SetActive(true);
        loadCatchReq();
        updateCatchReq();
      }
      else if (!isCritical)
      {
        critialHealthPanel.SetActive(false);
      }
      else
      {
        updateCatchReq();
      }
    }
  }

  void loadCatchReq(){
    Sprite[] loaders = BoardManager.instance.gemLoaders;
    Sprite[] gems = BoardManager.instance.characters.ToArray();

    wildMeta.maxCatchTurns = 8;
    wildMeta.currCatchTurns = 0;

    wildMeta.catchReq = new SkillReq[wildMeta.weaknesses.Length];

    int strengthMult = wildMeta.lvl / 10;

    for (int i = 0; i < wildMeta.weaknesses.Length; i++){
      int gemIdx = (int) wildMeta.weaknesses[i];
      GameObject.Find("MCatchOverlay"+(i + 1).ToString()).GetComponent<Image>().sprite = loaders[gemIdx];
      GameObject.Find("MCatchGem" + (i + 1).ToString()).GetComponent<Image>().sprite = gems[gemIdx];

      SkillReq catchReq = new SkillReq();
      catchReq.gem = (TileMeta.GemType) gemIdx;
      catchReq.has = 0;
      catchReq.req = 2 + (4 * strengthMult);
      wildMeta.catchReq[i] = catchReq;
    }
  }

  public void IncrementCatchTurns(){
    if (isCritical)
    {
      Debug.Log("IncrementCatchTurns");

      wildMeta.currCatchTurns++;

      Debug.Log("eMeta.currCatchTurns: " + wildMeta.currCatchTurns.ToString());

      //updateCatchReq();

      //Monster ran away
      if (wildMeta.currCatchTurns == wildMeta.maxCatchTurns)
      {
        // The player didnt lose, but they did not catch the monster either...
        Debug.Log("Monster Ran Away");
        adventure.roster[currentMonster].curHealth = GameObject.Find("HOverlay").GetComponent<Progress>().progress;
        BaseSaver.putAdventure(adventure);
        adventure.wild = getCurrentWild();
        GUIManager.instance.EndGame(true, false, false, adventure);
      }
      //Player fulfilled req. monster caught and added to roster
      bool caught = true;
      foreach (SkillReq req in wildMeta.catchReq)
      {
        if (req.has < req.req)
        {
          caught = false;
        }
      }
      if (caught == true)
      {
        Debug.Log("Caught Monster");
        PlayerRosterMeta wildMonsterMeta = getCurrentWild();

        if (adventure.roster.Length < 6)
        {
          List<PlayerRosterMeta> rosterMetas = new List<PlayerRosterMeta>(adventure.roster);
          rosterMetas.Add(wildMonsterMeta);
          adventure.roster = rosterMetas.ToArray();
        } else {
          List<PlayerRosterMeta> vaultMetas = new List<PlayerRosterMeta>(adventure.vault);
          vaultMetas.Add(wildMonsterMeta);
          adventure.vault = vaultMetas.ToArray();
          //List<PlayerRosterMeta> computer = new List<PlayerRosterMeta>(BaseSaver.getComputer());
          //computer.Add(wildMonsterMeta);
          //BaseSaver.putComputer(computer.ToArray());
        }

        adventure.roster[currentMonster].curHealth = GameObject.Find("HOverlay").GetComponent<Progress>().progress;
        BaseSaver.putAdventure(adventure);
        adventure.wild = getCurrentWild();
        GUIManager.instance.EndGame(true, true, false, adventure);
      }
    }
  }

  PlayerRosterMeta getCurrentWild(){
    if (!adventure.isTrainerEncounter) {
      //PlayerRosterMeta wildMonsterMeta = new PlayerRosterMeta();

      PlayerRosterMeta wildMonsterMeta = MonsterMeta.returnMonster(glossary.GetMonsterMain(adventure.wild.name).meta, adventure.wild.lvl, true);
      wildMonsterMeta.curHealth = GameObject.Find("MOverlay").GetComponent<Progress>().progress;
      wildMonsterMeta.maxHealth = GameObject.Find("MOverlay").GetComponent<Progress>().MAX_HEALTH;
      wildMonsterMeta.skills = adventure.wild.skills;

      //wildMonsterMeta.curHealth = GameObject.Find("MOverlay").GetComponent<Progress>().progress;
      //wildMonsterMeta.lvl = adventure.wild.lvl;
      //wildMonsterMeta.maxHealth = GameObject.Find("MOverlay").GetComponent<Progress>().MAX_HEALTH;
      //wildMonsterMeta.name = adventure.wild.name;
      //wildMonsterMeta.skills = adventure.wild.skills;

      Debug.Log("Caught Monster");
      Debug.Log(wildMonsterMeta);
      return wildMonsterMeta;
    }
    return null;
  }

  void updateCatchReq()
  {
    Debug.Log("updateCatchReq");

    GameObject.Find("MCatchInfoTurns").GetComponent<Text>().text = (wildMeta.maxCatchTurns - wildMeta.currCatchTurns).ToString();
    for (int i = 0; i < 2; i++)
    {
      if (i < wildMeta.catchReq.Length)
      {
        critialHealthPanelSkills[i].SetActive(true);
        string progStr = wildMeta.catchReq[i].has.ToString() + " / " + wildMeta.catchReq[i].req.ToString();
        float progFloat = (float)wildMeta.catchReq[i].has / (float)wildMeta.catchReq[i].req;

        Debug.Log("progStr: " + progStr);
        Debug.Log("progFloat: " + progFloat.ToString());
        Debug.Log("elem: " + "MCatchTxt" + (i + 1).ToString());
        Debug.Log("elem: " + "MCatchOverlay" + (i + 1).ToString());

        GameObject.Find("MCatchTxt" + (i + 1).ToString()).GetComponent<Text>().text = progStr;
        GameObject.Find("MCatchOverlay" + (i + 1).ToString()).GetComponent<Image>().fillAmount = progFloat;
      }
      else
      {
        critialHealthPanelSkills[i].SetActive(false);
      }
    }

  }

  void TryToRun(){
    StartCoroutine(Run());
  }

  IEnumerator Run(){
    float roll = UnityEngine.Random.Range(0, 2);
    Debug.Log("Roll: " + roll.ToString());
    GameObject player = GameObject.Find("HMonsterImg");
    Vector3 pos = player.transform.position;

    Vector3 left = pos;
    left.x -= 2;
    iTween.MoveTo(player, left, 1f);
    yield return new WaitForSeconds(1f);

    if (roll > 0)
    {
      Debug.Log("Run Success");
      GUIManager.instance.EndGame(true, false, false, adventure);
    }
    else
    {
      Debug.Log("Run Failure");
      iTween.MoveTo(player, pos, 1f);
      yield return new WaitForSeconds(1f);
      BoardManager.instance.setClicked(true);
      StartCoroutine(BoardManager.instance.CheckForEnd());
    }
  }

  void moveLocation(int click){
    if (click == -1) {
      //If the player decides not to use an item, 
      //close the monster menu and go back
      if (isUsingItem)
      {
        monPnl.SetActive(false);
        isUsingItem = false;
      }
      else
      {
        currentLocation = Location.Main;
      }
    } else {
      switch(currentLocation){
      case Location.Main:
        switch(click){
          case 0:
            currentLocation = Location.Action;
            break;
          case 1:
            currentLocation = Location.Swap;
            break;
          case 2:
            currentLocation = Location.Item;
            break;
          case 3:
            if (!adventure.isTrainerEncounter) {
              TryToRun();
            }
            break;
        default:
          currentLocation = Location.Main;
          break;
        }
        break;
      case Location.Action:
        if (activeSkill == click) {
          setActiveSkill(-1);
          Debug.Log ("Active Skill: None");
        } else {
          Debug.Log (activeSkill);
          Debug.Log (click);
          setActiveSkill(click);
        }
        break;
      default:
        break;
      }
    }
  }
	
  void loadLocation(){
    hideSkills ();
    Debug.Log ("Current Location: " + currentLocation.ToString() );
    switch(currentLocation){
    case Location.Main:
      back.SetActive (false);
      btnPnl.SetActive (true);
      monPnl.SetActive (false);
      itemPnl.SetActive(false);
      setButtons(new string[]{"Action", "Swap", "Item", "Run"});
      break;
    case Location.Action:
      back.SetActive (true);
      btnPnl.SetActive (true);
      monPnl.SetActive (false);
      itemPnl.SetActive(false);
      List<string> actions = new List<string> ();
      List<string> details = new List<string> ();
      foreach(SkillMeta skill in skills){
        actions.Add (skill.name);
        details.Add (getEffectsString(skill.effects));
      }
      setButtons(actions.ToArray(), details.ToArray());
      showSkills ();
      Debug.Log ("loadLocation");
      loadSkillsOverlays ();
      break;
    case Location.Swap:
      back.SetActive (true);
      itemPnl.SetActive(false);
      btnPnl.SetActive (false);
      monPnl.SetActive (true);
      setSwapMonsters();
      break;
    case Location.Item:
      back.SetActive(true);
      itemPnl.SetActive(true);
      btnPnl.SetActive(false);
      monPnl.SetActive(false);
      setItems();
      break;
      default:
      break;
    }
  }

  void setButtons(string[] btnTxt){
    for(int i = 0; i < 4; i++){
      txt[i].gameObject.SetActive(true);
      dtls[i].gameObject.SetActive(true);
      if (i < btnTxt.Length) {
        dtls[i].gameObject.GetComponent<Text>().text = "";
        txt [i].text = btnTxt [i];
      } else {
        txt[i].gameObject.GetComponent<Text>().text = "";
        dtls[i].gameObject.GetComponent<Text>().text = "";
      }
    }
  }

  void setButtons(string[] btnTxt, string[] btnDtls){
    for(int i = 0; i < 4; i++){
      txt[i].gameObject.SetActive(true);
      dtls[i].gameObject.SetActive(true);
      if (i < btnTxt.Length) {
        txt [i].text = btnTxt [i];
        dtls[i].text = btnDtls [i];
      } else {
        txt[i].gameObject.GetComponent<Text>().text = "";
        dtls[i].gameObject.GetComponent<Text>().text = "";
      }
    }
  }

  void setSwapMonsters(){
    for(int i = 1; i < 7; i++){
      if (i <= adventure.roster.Length) {
        rMonsters[i - 1].SetActive(true);
        rMonstersDead[i - 1].SetActive(false);
        rMonstersLvl[i - 1].SetActive(true);
        rMonstersHealth[i - 1].SetActive(true);

        rMonsters[i - 1].GetComponent<Image>().sprite = glossary.GetMonsterImage(adventure.roster[i -1].name);
        rMonstersLvl[i - 1].GetComponent<Text>().text = adventure.roster[i - 1].lvl.ToString();
        if (i - 1 == currentMonster) {
          rMonstersHealth[i - 1].GetComponent<Text>().text = ((int)(((float)GameObject.Find("HOverlay").GetComponent<Progress>().progress / (float)adventure.roster[i - 1].maxHealth) * 100)).ToString() + "%";
        } else {
          rMonstersHealth[i - 1].GetComponent<Text>().text = ((int)(((float)adventure.roster[i - 1].curHealth / (float)adventure.roster[i - 1].maxHealth) * 100)).ToString() + "%";

        }
        //Debug.Log("Health: " + adventure.roster[i - 1].curHealth.ToString() + ":" + adventure.roster[i - 1].maxHealth.ToString());
        if (adventure.roster[i - 1].curHealth <= 0) {
          rMonstersDead[i - 1].SetActive(true);
        }
      } else {
        rMonsters[i - 1].SetActive(false);
        rMonstersDead[i - 1].SetActive(false);
        rMonstersLvl[i - 1].SetActive(false);
        rMonstersHealth[i - 1].SetActive(false);
      }
    }
  }

  void setItems()
  {
    Dictionary<string, int> itemDict = adventure.GetTreasureList();
    string[] keys = itemDict.Keys.ToArray();

    for (int i = 0; i < 6; i++)
    {
      if (i < keys.Length) {
        Items[i].SetActive(true);
        itemTxt[i].SetActive(true);
        Items[i].GetComponent<Image>().sprite = glossary.GetItem(keys[i]).gameObject.GetComponent<SpriteRenderer>().sprite;
        itemTxt[i].GetComponent<Text>().text = "x" + itemDict[keys[i]].ToString();
      } else {
        Items[i].SetActive(false);
        itemTxt[i].SetActive(false);
      }
    }
  }

  public string getItem(int pos){
    return adventure.GetTreasureList().Keys.ToArray()[pos];
  }
}
