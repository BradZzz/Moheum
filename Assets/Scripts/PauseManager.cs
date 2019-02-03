using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

  public static PauseManager instance;
  public GameObject glossaryObj;

  public GameObject openPnl;

  public GameObject mainPnl;
  public GameObject rosterPnl;
  public GameObject rosterInfoPnl;
  public GameObject vaultPnl;
  public GameObject itemsPnl;
  public GameObject statusPnl;
  public GameObject glossaryPnl;

  public GameObject clickSprite;

  //public GameObject mapsPnl;
  //public GameObject statsPnl;
  //public GameObject optsPnl;

  public GameObject[] rosterSubPnls;
  public GameObject[] itemSubPnls;

  private int currentRosterMonster;
  private GameObject rosterNickname;
  private GameObject rosterEdit;
  private GameObject rosterInput;
  private Location currentLocation;
  private Glossary glossy;
  private bool swapping;
  private int[] swapBuffer;
  private bool deleting;
  private int[] deleteBuffer;
  private bool panelOpen;
  private int vaultPos;
  private bool vaultDeleting;
  private int[] vaultDeleteBuffer;
  private bool vaultSwap;
  private int pageDistance;

  private int PAGE_DIST = 24;

  private enum Location
  {
    Main, Roster, RosterInfo, Items, Vault, Status, Glossary, None
  }

  public bool IsOpen(){
    return panelOpen;
  }

  public IEnumerator WaitForAction(float wait)
  {
    yield return new WaitForSeconds(wait);
    GameObject.Find("MenuTitle").SetActive(false);
    yield return null;
  }

  private IEnumerator WaitForExit()
  {
    yield return new WaitForSeconds(.1f);
    panelOpen = false;
  }

  private void Awake()
  {
    instance = GetComponent<PauseManager>();
    panelOpen = false;
    glossy = glossaryObj.GetComponent<Glossary>();
    SceneMain scene = glossy.GetScene(BaseSaver.getMap());
    GameObject.Find("MenuTitle").SetActive(true);
    GameObject.Find("MenuTitle").GetComponent<Text>().text = scene.meta.name;
    StartCoroutine(WaitForAction(3f));

    GameObject map = Instantiate(scene.map, new Vector3(0, 0, 0), Quaternion.identity);
    map.SetActive(true);
    swapping = false;
    deleting = false;

    Debug.Log("PauseManager Awake");

    AdventureMeta meta = BaseSaver.getAdventure();
    BoardMeta board = BaseSaver.getBoard(BaseSaver.getMap());

    GameObject hero = GameObject.FindWithTag("Player");
    if (BaseSaver.getMap().Equals("ShallowGrove") && BaseSaver.getMapPrev() != "") {
      hero.SetActive(false);
      hero = null;
      Destroy(GameObject.FindWithTag("Player"));
    }

    if (hero == null)
    {
      // Pull the hero out of the glossary and instantiate it in the right exit/entrance tile
      GameObject nHero = null;
      string dest = BaseSaver.getMapConnection();
      string prev = BaseSaver.getMapPrev();
      Debug.Log("Prev: " + prev);
      Debug.Log("Dest: " + dest);
      foreach (GameObject exit in GameObject.FindGameObjectsWithTag("Exit"))
      {
        ExitTile tile = exit.GetComponent<ExitTile>();
        if (tile.toScene.Equals(prev + '.' + dest))
        {
          nHero = Instantiate(glossy.hero, exit.transform.position, exit.transform.rotation, GameObject.Find("Units").transform);
          BaseSaver.setMapConnection("");
          BaseSaver.setMapPrevName("");
          break;
        }
      }
      if (nHero != null)
      {
        nHero.GetComponent<PlayerMain>().playerMeta = meta;
      } else {
        Debug.Log("Hero is null!");
        nHero = Instantiate(glossy.hero, board.playerPos.ToVector3(), Quaternion.identity, GameObject.Find("Units").transform);
        nHero.GetComponent<PlayerMain>().playerMeta = meta;
      }
    } else {
      Debug.Log("Hero is not null!");
      //GameObject.FindWithTag("Player").transform.position = new Vector3(board.playerPos.x, board.playerPos.y, board.playerPos.z);
      GameObject.FindWithTag("Player").GetComponent<PlayerMain>().playerMeta = meta;
    }

    //GameObject.FindWithTag("Player").GetComponent<PlayerMain>().playerMeta = meta;
    //BoardMeta board = BaseSaver.getBoard(BaseSaver.getMap());

    //Debug.Log("Player Pos: " + GameObject.Find("PlayerHero").transform.position.ToString());

    if (board != null)
    {
      Debug.Log("Board not null");

      GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
      GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

      Debug.Log("board: " + board.mapName);
      Debug.Log("player pos: " + board.playerPos);

      foreach (GameObject npc in npcs)
      {
        Debug.Log("board npc: " + (new PosMeta(npc.transform.position)).ToString());
        Debug.Log("equals battle: " + meta.trainer.pos.Equals(new PosMeta(npc.transform.position)).ToString());
        foreach (NPCMeta NPCMeta in board.NPCs)
        {
          if (NPCMeta.name.Equals(npc.GetComponent<NPCMain>().meta.name))
          {
            npc.GetComponent<NPCMain>().meta = new NPCMeta(NPCMeta);
          }
        }
        if (meta.trainer != null && meta.trainer.name.Equals(npc.GetComponent<NPCMain>().meta.name))
        {
          Debug.Log("Found: " + npc.GetComponent<NPCMain>().meta.ToString());
          npc.GetComponent<NPCMain>().meta = new NPCMeta(meta.trainer);
          Debug.Log("changed: " + npc.GetComponent<NPCMain>().meta.ToString());
        }
      }
      foreach (GameObject item in items)
      {
        item.GetComponent<TreasureMain>().UpdateInteractable();
      }
      //GameObject.FindWithTag("Player").transform.position = new Vector3(board.playerPos.x, board.playerPos.y, board.playerPos.z);
      BaseSaver.putBoard(GameUtilities.getBoardState(BaseSaver.getMap(), new PosMeta(GameObject.FindWithTag("Player").transform.position)));
    }
    else
    {
      Debug.Log("Board is null");
    }
  }

  void Start()
  {
    Debug.Log("Panel Manager Start");

    currentLocation = Location.None;

    rosterSubPnls = new GameObject[]{
      GameObject.Find("RosterPnl01"),
      GameObject.Find("RosterPnl02"),
      GameObject.Find("RosterPnl03"),
      GameObject.Find("RosterPnl04"),
      GameObject.Find("RosterPnl05"),
      GameObject.Find("RosterPnl06")
    };

    itemSubPnls = new GameObject[]{
      GameObject.Find("ItemPnl01"),
      GameObject.Find("ItemPnl02"),
      GameObject.Find("ItemPnl03"),
      GameObject.Find("ItemPnl04"),
      GameObject.Find("ItemPnl05"),
      GameObject.Find("ItemPnl06"),
      GameObject.Find("ItemPnl07"),
      GameObject.Find("ItemPnl08"),
      GameObject.Find("ItemPnl09")
    };

    clearAll();

    openPnl.SetActive(true);
  }

  public void click(int pos)
  {
    SFXManager.instance.PlaySFX(Clip.Select);

    Debug.Log("pos: " + pos.ToString());
    if (!swapping && !deleting && !vaultSwap)
    {
      clearAll();
    }
    moveLocation(pos);
    //loadLocation();
    SFXManager.instance.PlaySFX(Clip.Select);
  }

  public void VaultClick(int pos){
    SFXManager.instance.PlaySFX(Clip.Select);
    AdventureMeta adventure = BaseSaver.getAdventure();
    if (adventure.vault.Length > pos) {
      if (vaultDeleting)
      {
        if (vaultDeleteBuffer[0] != -1)
        {
          Debug.Log("Attempt to remove at: " + pos.ToString());
          if (vaultDeleteBuffer[0] == pos) {
            Debug.Log("Removing at: " + pos.ToString());
            List<PlayerRosterMeta> vList = new List<PlayerRosterMeta>(adventure.vault);
            vList.RemoveAt(pos);
            adventure.vault = vList.ToArray();
            BaseSaver.putAdventure(adventure);
          }
          //adventure.vault = adventure.vault.Skip(pos+1).ToArray();
          notVaultDeleting();
          loadVault();
        }
        else
        {
          vaultDeleteBuffer[0] = pos;
          string posStr = pos + 1 < 10 ? 0 + (pos + 1).ToString() : (pos + 1).ToString();
          GameObject clicked = GameObject.Find("VBk" + posStr);
          clicked.AddComponent<Outline>();
          clicked.GetComponent<Outline>().enabled = true;
          clicked.GetComponent<Outline>().effectColor = new Color(.871f, .376f, .318f);
        }
      }
      else
      {
        vaultPos = pos;
        vaultSwap = true;
        vaultPnl.SetActive(false);
        rosterPnl.SetActive(true);
        loadRoster();
      }
    }
  }

  public void PageDir(bool left){
    SFXManager.instance.PlaySFX(Clip.Select);
    AdventureMeta meta = BaseSaver.getAdventure();
    if (left)
    {
      pageDistance -= PAGE_DIST;
      if (pageDistance < 0)
      {
        pageDistance = 0;
      }
    }
    else
    {
      if (pageDistance + PAGE_DIST < meta.vault.Length)
      {
        pageDistance += PAGE_DIST;
      }
    }
    loadVault();
  }

  public void swapClicked()
  {
    notDeleting();
    if (BaseSaver.getAdventure().roster.Length > 1)
    {
      swapping = true;
      swapBuffer = new int[] { -1, -1 };
      Debug.Log("Swapping!");
    }
    SFXManager.instance.PlaySFX(Clip.Select);
  }

  public void notDeleting(){
    deleting = false;
  }

  public void notSwapping()
  {
    swapping = false;
  }

  public void notVaultSwapping()
  {
    vaultSwap = false;
  }

  public void notVaultDeleting()
  {
    if (vaultDeleting) {
      vaultDeleting = false;

      GameObject trashIcon = GameObject.Find("RImage");
      Destroy(trashIcon.GetComponent<Outline>());

      if (vaultDeleteBuffer != null && vaultDeleteBuffer[0] != -1)
      {
        string posStr = vaultDeleteBuffer[0] + 1 < 10 ? 0 + (vaultDeleteBuffer[0] + 1).ToString() : (vaultDeleteBuffer[0] + 1).ToString();
        GameObject clicked = GameObject.Find("VBk" + posStr);
        Destroy(clicked.GetComponent<Outline>());
        vaultDeleteBuffer = new int[] { -1, -1 };
      }
    }
  }

  public void deleteClicked()
  {
    if (BaseSaver.getAdventure().roster.Length > 0)
    {
      deleting = true;
      deleteBuffer = new int[] { -1, -1 };
      Debug.Log("Deleting!");
    }
    SFXManager.instance.PlaySFX(Clip.Select);
  }

  void clearAll(){
    mainPnl.SetActive(false);
    rosterPnl.SetActive(false);
    rosterInfoPnl.SetActive(false);
    openPnl.SetActive(false);
    vaultPnl.SetActive(false);
    itemsPnl.SetActive(false);
    statusPnl.SetActive(false);
    glossaryPnl.SetActive(false);
  }

  public void vaultDeleteClicked()
  {
    if (BaseSaver.getAdventure().vault.Length > 0)
    {
      vaultDeleting = true;
      vaultDeleteBuffer = new int[] { -1, -1 };
      Debug.Log("Vault Deleting!");
      GameObject trashIcon = GameObject.Find("RImage");
      trashIcon.AddComponent<Outline>();
      trashIcon.GetComponent<Outline>().enabled = true;
      trashIcon.GetComponent<Outline>().effectColor = new Color(.871f, .376f, .318f);
    }
    SFXManager.instance.PlaySFX(Clip.Select);
  }

  void moveLocation(int clk)
  {
    StopCoroutine(WaitForExit());
    panelOpen = true;
    if (clk == -1)
    {
      notDeleting();
      notSwapping();
      if (currentLocation == Location.Main) {
        currentLocation = Location.None;
        clearAll();
        openPnl.SetActive(true);
        StartCoroutine(WaitForExit());
      } else if (currentLocation == Location.RosterInfo) {
        currentLocation = Location.Main;
        moveLocation(0);
      } else if (currentLocation == Location.Vault && vaultSwap)
      {
        notVaultSwapping();
        notVaultDeleting();
        vaultPnl.SetActive(true);
        rosterPnl.SetActive(false);
      }
      else {
        currentLocation = Location.Main;
        notVaultSwapping();
        notVaultDeleting();
        clearAll();
        mainPnl.SetActive(true);
      }
    }
    else
    {
      switch (currentLocation)
      {
        case Location.Main:
          switch (clk)
          {
            case 0:
              currentLocation = Location.Roster;
              rosterPnl.SetActive(true);
              loadRoster();
              break;
            case 1:
              currentLocation = Location.Vault;
              vaultPnl.SetActive(true);
              loadVault();
              break;
            case 2:
              currentLocation = Location.Items;
              itemsPnl.SetActive(true);
              loadItems();
              break;
            case 3:
              currentLocation = Location.Status;
              statusPnl.SetActive(true);
              break;
            case 4:
              currentLocation = Location.Glossary;
              glossaryPnl.SetActive(true);
              LoadGlossary();
              break;
            default:
              currentLocation = Location.None;
              break;
          }
          break;
        case Location.Vault:
          if (vaultSwap) {
            AdventureMeta meta = BaseSaver.getAdventure();
            meta.SwitchInVault(clk - 1, vaultPos);
            BaseSaver.putAdventure(meta);
            GameObject.FindWithTag("Player").GetComponent<PlayerMain>().playerMeta = meta;
            notVaultSwapping();
            vaultPnl.SetActive(true);
            rosterPnl.SetActive(false);
            loadVault();
          }
          break;
        case Location.Roster:
          if (!swapping && !deleting)
          {
            currentLocation = Location.RosterInfo;
            rosterInfoPnl.SetActive(true);
            loadRosterInfo(clk);
          } else if (swapping) {
            Debug.Log("Location.Roster: ");
            if (swapBuffer[0] == -1) {
              swapBuffer[0] = clk - 1;
              GameObject.Find("RosterPnl0" + clk).GetComponent<Outline>().effectColor = Color.red;
              GameObject.Find("RosterPnl0" + clk).GetComponent<Outline>().effectDistance = new Vector2(3,-3);
              //Debug.Log("swapBuffer[0]: " + swapBuffer[0].ToString());
            } else if (swapBuffer[0] != clk - 1) {
              GameObject.Find("RosterPnl0" + (swapBuffer[0] + 1).ToString()).GetComponent<Outline>().effectColor = Color.black;
              GameObject.Find("RosterPnl0" + (swapBuffer[0] + 1).ToString()).GetComponent<Outline>().effectDistance = new Vector2(1, -1);
              swapBuffer[1] = clk - 1;
              //Debug.Log("swapBuffer[1]: " + swapBuffer[1].ToString());
              AdventureMeta playerMeta = BaseSaver.getAdventure();
              PlayerRosterMeta temp = new PlayerRosterMeta(playerMeta.roster[swapBuffer[0]]);
              playerMeta.roster[swapBuffer[0]] = new PlayerRosterMeta(playerMeta.roster[swapBuffer[1]]);
              playerMeta.roster[swapBuffer[1]] = temp;
              BaseSaver.putAdventure(playerMeta);

              GameObject.FindWithTag("Player").GetComponent<PlayerMain>().playerMeta = playerMeta;

              swapping = false;
              loadRoster();
            }
          } else if (deleting)
          {
            if (deleteBuffer[0] == -1)
            {
              deleteBuffer[0] = clk - 1;
              GameObject.Find("RosterPnl0" + clk).GetComponent<Outline>().effectColor = Color.red;
              GameObject.Find("RosterPnl0" + clk).GetComponent<Outline>().effectDistance = new Vector2(3, -3);
            } else {
              GameObject.Find("RosterPnl0" + (deleteBuffer[0] + 1).ToString()).GetComponent<Outline>().effectColor = Color.black;
              GameObject.Find("RosterPnl0" + (deleteBuffer[0] + 1).ToString()).GetComponent<Outline>().effectDistance = new Vector2(1, -1);
              if (clk - 1 != deleteBuffer[0]) {
                notDeleting();
              } else {
                AdventureMeta playerMeta = BaseSaver.getAdventure();

                List<PlayerRosterMeta> roster = new List<PlayerRosterMeta>(playerMeta.roster);
                roster.RemoveAt(deleteBuffer[0]);
                if (playerMeta.vault.Length > 0)
                {
                  roster.Add(playerMeta.vault[0]);
                  playerMeta.vault = playerMeta.vault.Skip(1).ToArray();
                }
                playerMeta.roster = roster.ToArray();
                BaseSaver.putAdventure(playerMeta);

                notDeleting();

                loadRoster();
              }
            }
          }
          break;
        case Location.None:
          Debug.Log("Here None");
          currentLocation = Location.Main;
          mainPnl.SetActive(true);
          break;
        default:
          break;
      }
    }
  }

  public void ShowRosterNicknameInput(){
    rosterInput.SetActive(true);
  }

  public void EditedRosterNickname(UnityEngine.UI.Text nickname_edit)
  {
    Debug.Log("Submitting Nickname: " + nickname_edit.text);
    AdventureMeta playerMeta = BaseSaver.getAdventure();
    playerMeta.roster[currentRosterMonster].nickname = nickname_edit.text;
    BaseSaver.putAdventure(playerMeta);
    rosterInput.GetComponent<InputField>().text = "";
    loadRosterInfo(currentRosterMonster + 1);
  }

  void loadRosterInfo(int clk){
    currentRosterMonster = clk - 1;

    if (rosterNickname == null) {
      rosterNickname = GameObject.Find("LNickName");
    }
    if (rosterEdit == null)
    {
      rosterEdit = GameObject.Find("NickNameEdit");
    }
    if (rosterInput == null)
    {
      rosterInput = GameObject.Find("LNickNameInput");
    }
    rosterInput.SetActive(false);

    AdventureMeta playerMeta = BaseSaver.getAdventure();
    PlayerRosterMeta monsterMetaShort = playerMeta.roster[currentRosterMonster];
    MonsterMeta monsterMetaLong = glossy.GetMonsterMain(monsterMetaShort.name).meta;
    int[] lvlInfo = MonsterMeta.CalcLvl(monsterMetaShort, monsterMetaLong.lvlSpeed);

    GameObject.Find("LImg").GetComponent<Image>().sprite = glossy.GetMonsterImage(monsterMetaShort.name);
    GameObject.Find("Llvl").GetComponent<Text>().text = lvlInfo[0].ToString();
    GameObject.Find("LName").GetComponent<Text>().text = monsterMetaShort.name;
    rosterNickname.GetComponent<Text>().text = "\"" + monsterMetaShort.nickname + "\"";

    GameObject.Find("HealthTxt").GetComponent<Text>().text = "Health: " + monsterMetaShort.maxHealth.ToString();
    GameObject.Find("ExpTxt").GetComponent<Text>().text = "Exp: " + monsterMetaShort.exp.ToString();
    GameObject.Find("Stat01Txt").GetComponent<Text>().text = "Lust: " + monsterMetaShort.lust.ToString("0.00");
    GameObject.Find("Stat02Txt").GetComponent<Text>().text = "Greed: " + monsterMetaShort.greed.ToString("0.00");
    GameObject.Find("Stat03Txt").GetComponent<Text>().text = "Wrath: " + monsterMetaShort.wrath.ToString("0.00");
    GameObject.Find("Stat04Txt").GetComponent<Text>().text = "Pride: " + monsterMetaShort.pride.ToString("0.00");
    GameObject.Find("Stat05Txt").GetComponent<Text>().text = "Gluttony: " + monsterMetaShort.gluttony.ToString("0.00");
    GameObject.Find("Stat06Txt").GetComponent<Text>().text = "Sloth: " + monsterMetaShort.sloth.ToString("0.00");
    GameObject.Find("Stat07Txt").GetComponent<Text>().text = "Envy: " + monsterMetaShort.envy.ToString("0.00");
    GameObject.Find("Stat08Txt").GetComponent<Text>().text = "Lrn: " + monsterMetaLong.lvlSpeed.ToString();

    List<string> mSkills = new List<string>(monsterMetaShort.skills);
    List<SkillMeta> sMetas = new List<SkillMeta>(GameUtilities.parseSkills(monsterMetaShort.skills, glossy));

    for (int i = 0; i < 2; i++)
    {
      if (monsterMetaLong.strengths.Length > i) {
        GameObject.Find("LType" + (i + 1).ToString() + "Txt").GetComponent<Text>().text = monsterMetaLong.strengths[i].ToString();
        //GameObject.Find("LType" + (i + 1).ToString()).GetComponent<Image>().enabled = true;
        //GameObject.Find("LType" + (i+1).ToString()).GetComponent<Image>().sprite = glossy.getGemSprite(MonsterMeta.elementToGem(monsterMetaLong.strengths[i]));
      } else {
        //GameObject.Find("LType" + (i + 1).ToString()).GetComponent<Image>().enabled = false;
        GameObject.Find("LType" + (i + 1).ToString() + "Txt").GetComponent<Text>().text = "";
      }
    }

    for (int i = 0; i < 4; i++)
    {
      if (mSkills.Count > i)
      {
        GameObject.Find("Sk0" + (i + 1) + "Res01Gem").GetComponent<Image>().enabled = true;
        GameObject.Find("Sk0" + (i + 1) + "Res02Gem").GetComponent<Image>().enabled = true;

        GameObject.Find("LSk0" + (i + 1) + "C").GetComponent<Text>().text = mSkills[i];
        GameObject.Find("LSk0" + (i + 1) + "CDesc").GetComponent<Text>().text = PanelManager.getEffectsString(sMetas[i].effects);

        GameObject.Find("Sk0" + (i + 1) + "Res01Txt").GetComponent<Text>().text = sMetas[i].req1.req.ToString();
        GameObject.Find("Sk0" + (i + 1) + "Res01Gem").GetComponent<Image>().sprite = glossy.getGemSprite(sMetas[i].req1.gem);
        GameObject.Find("Sk0" + (i + 1) + "Res02Txt").GetComponent<Text>().text = sMetas[i].req2.req.ToString();
        GameObject.Find("Sk0" + (i + 1) + "Res02Gem").GetComponent<Image>().sprite = glossy.getGemSprite(sMetas[i].req2.gem);
      }
      else
      {
        GameObject.Find("LSk0" + (i + 1) + "C").GetComponent<Text>().text = "";
        GameObject.Find("LSk0" + (i + 1) + "CDesc").GetComponent<Text>().text = "";

        GameObject.Find("Sk0" + (i + 1) + "Res01Txt").GetComponent<Text>().text = "";
        GameObject.Find("Sk0" + (i + 1) + "Res02Txt").GetComponent<Text>().text = "";
        GameObject.Find("Sk0" + (i + 1) + "Res01Gem").GetComponent<Image>().enabled = false;
        GameObject.Find("Sk0" + (i + 1) + "Res02Gem").GetComponent<Image>().enabled = false;
      }
    }
  }

  void loadRoster(){
    AdventureMeta meta = BaseSaver.getAdventure();
    for (int i = 1; i < 7; i++){
      if (meta.roster.Length >= i) {
        Debug.Log("I: " + i.ToString());
        Debug.Log("Monster Name: " + meta.roster[i - 1].name);
        rosterSubPnls[i - 1].SetActive(true);
        string img = "M0" + i.ToString() + "Img";
        string health = "M0" + i.ToString() + "Health";
        string healthTxt = "M0" + i.ToString() + "HealthTxt";
        string mName = "M0" + i.ToString() + "Name";
        string mLvl = "M0" + i.ToString() + "lvl";
        string mExp = "M0" + i.ToString() + "Exp";
        string mExpTxt = "M0" + i.ToString() + "ExpTxt";
        string mPowTxt = "M0" + i.ToString() + "Pow";

        int[] lvlCalc = MonsterMeta.CalcLvl(meta.roster[i - 1],glossy.GetMonsterMain(meta.roster[i - 1].name).meta.lvlSpeed);

        GameObject.Find(img).GetComponent<Image>().sprite = glossy.GetMonsterImage(meta.roster[i - 1].name);

        GameObject.Find(health).GetComponent<Image>().fillAmount = (meta.roster[i - 1].curHealth/(float)meta.roster[i - 1].maxHealth);
        GameObject.Find(healthTxt).GetComponent<Text>().text = meta.roster[i - 1].curHealth.ToString() + " / " + meta.roster[i - 1].maxHealth.ToString();

        GameObject.Find(mExp).GetComponent<Image>().fillAmount = (lvlCalc[1] / (float)lvlCalc[2]);
        GameObject.Find(mExpTxt).GetComponent<Text>().text = (lvlCalc[2] - lvlCalc[1]).ToString();

        GameObject.Find(mName).GetComponent<Text>().text = meta.roster[i - 1].nickname.Length > 0 ? meta.roster[i - 1].nickname : meta.roster[i - 1].name;
        GameObject.Find(mLvl).GetComponent<Text>().text = lvlCalc[0].ToString();

        Debug.Log("Power: " + meta.roster[i - 1].getPower().ToString("0.00"));

        GameObject.Find(mPowTxt).GetComponent<Text>().text = "POW: <color=#ff0000>" + meta.roster[i-1].getPower().ToString("0.00") + "</color>";
      } else {
        rosterSubPnls[i - 1].SetActive(false);
      }
    }
  }

  void loadItems()
  {
    AdventureMeta meta = BaseSaver.getAdventure();
    Dictionary<string, int> treasure = meta.GetTreasureList();
    List<string> keys = new List<string>(treasure.Keys);
    GameObject.Find("MoneyTxt").GetComponent<Text>().text = "¥" + meta.getYen();
    for (int i = 1; i < 10; i++)
    {
      if (keys.Count >= i)
      {
        itemSubPnls[i - 1].SetActive(true);
        string img = "I0" + i.ToString() + "Img";
        string nme = "I0" + i.ToString() + "Name";
        string description = "I0" + i.ToString() + "Desc";
        string qty = "I0" + i.ToString() + "Qty";

        GameObject.Find(img).GetComponent<Image>().sprite = glossy.GetItem(keys[i-1]).GetComponent<SpriteRenderer>().sprite;
        GameObject.Find(nme).GetComponent<Text>().text = keys[i - 1];
        GameObject.Find(description).GetComponent<Text>().text = glossy.GetItem(keys[i - 1]).monTreas.description;
        GameObject.Find(qty).GetComponent<Text>().text = treasure[keys[i - 1]].ToString();
      }
      else
      {
        itemSubPnls[i - 1].SetActive(false);
      }
    }
  }

  void loadVault()
  {
    AdventureMeta meta = BaseSaver.getAdventure();
    //VBk01
    for (int i = 0 + pageDistance; i < 24 + pageDistance; i++) {
      GameObject vMohe;
      GameObject vMoheLvlOut;
      GameObject vMoheLvl;
      if ((i + 1) < 10)
      {
        vMohe = GameObject.Find("VMon0" + (i + 1).ToString());
        vMoheLvlOut = GameObject.Find("LvlOutline0" + (i + 1).ToString());
        vMoheLvl = GameObject.Find("VMonLvl0" + (i + 1).ToString());
      }
      else
      {
        vMohe = GameObject.Find("VMon" + (i + 1).ToString());
        vMoheLvlOut = GameObject.Find("LvlOutline" + (i + 1).ToString());
        vMoheLvl = GameObject.Find("VMonLvl" + (i + 1).ToString());
      }
      if (meta.vault == null) {
        meta.vault = new PlayerRosterMeta[0];
        BaseSaver.putAdventure(meta);
      }
      if (meta.vault.Length > i) {
        vMohe.GetComponent<Image>().enabled = true;
        vMohe.GetComponent<Image>().sprite = glossy.GetMonsterImage(meta.vault[i].name);
        vMoheLvlOut.GetComponent<Image>().enabled = true;
        vMoheLvl.GetComponent<Text>().enabled = true;
        vMoheLvl.GetComponent<Text>().text = meta.vault[i].lvl.ToString();
      } else {
        vMohe.GetComponent<Image>().enabled = false;
        vMoheLvlOut.GetComponent<Image>().enabled = false;
        vMoheLvl.GetComponent<Text>().enabled = false;
      }
    }
  }

  void LoadGlossary()
  {
    //- [ ] Pride => Receive bigger bonuses when matching 4/5 gems
    //- [ ] Greed => Destroy more gems with skill
    //- [ ] Lust => Change more gems with skill
    //- [ ] Envy => Increases level gains by a small amount. Sabotage skills more effective. 
    //- [ ] Gluttony => Receive more health on lvl up
    //- [ ] Wrath => Deal more damage with fight gems
    //- [ ] Sloth => Reset / Heal / Damage skills more effective

    /*
     * static string abbreviationEffect(Effect effect){
    switch(effect){
    case Effect.Change:
      return "Cng";
    case Effect.ChangeSome:
      return "Cng";
    case Effect.Damage:
      return "Dmg";
    case Effect.Destroy:
      return "Dest";
    case Effect.DestroySome:
      return "Dest";
    case Effect.Heal:
      return "Heal";
    case Effect.Poison:
      return "Buff";
    case Effect.Poke:
      return "Poke";
    case Effect.Reset:
      return "Reset";
    case Effect.Sabotage:
      return "Sabo";
    case Effect.Slice:
      return "Slice";
    case Effect.xTurn:
      return "xTurn";
    }
    return "";
  }
   Buff => Applies a shield that either blocks an opponents next attack or enhances your own.
   Chg => Change gem1 to gem2.
   Dmg => Deal damage directly to opponent's mohe.
   Dest => Destroy gem.
   Heal => Heal mohe that amount.
   Poke => Click on gem of type to destroy it. Adding strength increases the radius destroyed in a square.
   Reset => Resets the board.
   Sabo => Removes a certain amount of gems from your opponents skills.
   Slice => Click on gem of type to destroy it. Adding strength increases the radius destroyed in an X.
     */

  }
}
