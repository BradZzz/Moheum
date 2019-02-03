/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
	public static GUIManager instance;

  public GameObject introPanel;
  public GameObject gameOverPanel;
  public GameObject catchPanel;
  public GameObject trainerPanel;
  public GameObject lvlUpPanel;

  public Sprite genericTrainer;

  private bool choosingSkill;

  private GameObject boardManager;
  private PlayerRosterMeta lastMeta;
  private int skillSelected;
  private IEnumerator backgroundShift;

	void Awake() {
		instance = GetComponent<GUIManager>();
    boardManager = GameObject.Find("BoardManager");
    choosingSkill = false;
  }

  public void LevelUp(PlayerRosterMeta abbvMeta, int exp){
    StartCoroutine(WaitForShifting(abbvMeta, exp));
  }

  //public void HideBoard(){
  //  Debug.Log("Hide Board");
  //  Vector3 pos = boardManager.transform.position;
  //  pos.z = 0;
  //  boardManager.transform.position = pos;
  //}

  public void ShowBoard(){
    Debug.Log("Show Board");
    //if (!introPanel.activeInHierarchy)
    //{
      //Debug.Log("Intro Panel Closed");
    boardManager.GetComponent<BoardManager>().StartBoardCreate();
    //}
    //else
    //{
    //  Debug.Log("Intro Panel Open!");
    //}
    //boardManager.GetComponent<BoardManager>().StartBoardCreate();
    //Vector3 pos = boardManager.transform.position;
    //pos.z = 90;
    //boardManager.transform.position = pos;
  }

  public void StartIntroduction(Glossary glossy)
  {
    StartCoroutine(StartIntro(glossy, BaseSaver.getAdventure()));
  }

  IEnumerator StartIntro(Glossary glossy, AdventureMeta meta){
    /*
     * Make color = black
     * move to center
     * shake
     * Make color = normal
     */
    //HideBoard();
    yield return new WaitForSeconds(.5f);
    GameObject toCopy = introPanel.transform.Find("Image").gameObject;
    GameObject descObj = introPanel.transform.Find("Desc").gameObject;
    Color oldColor = toCopy.GetComponent<Image>().color;
    if (meta.isTrainerEncounter)
    {
      toCopy.GetComponent<Image>().sprite = genericTrainer;
    }
    else
    {
      toCopy.GetComponent<Image>().sprite = glossy.GetMonsterImage(meta.wild.name);
      toCopy.GetComponent<Image>().color = Color.black;
    }
    descObj.GetComponent<Text>().text = "";

    //GameObject character = Instantiate(toCopy, toCopy.transform.position, Quaternion.identity);
    iTween.MoveTo(toCopy, new Vector3(descObj.transform.position.x, toCopy.transform.position.y, toCopy.transform.position.z), 1f);
    yield return new WaitForSeconds(1f);
    if (!meta.isTrainerEncounter) {
      iTween.ShakePosition(toCopy, new Vector3(1, 0, 0), .2f);
    }
    yield return new WaitForSeconds(1f);
    //iTween.ValueTo(introPanel, iTween.Hash(
    //"from", 0,
    //"to", 1,
    //"time", .5f,
    //"onupdatetarget", gameObject,
    //"onupdate", "RevealCharacter"));
    toCopy.GetComponent<Image>().color = oldColor;
    if (meta.isTrainerEncounter)
    {
      descObj.GetComponent<Text>().text = "Challenged By : " + meta.trainer.name;
    }
    else
    {
      descObj.GetComponent<Text>().text = "Wild " + meta.wild.name + " Found!";
    }
  }

  public void RevealCharacter(float alpha)
  {
    Color fadeCol = introPanel.GetComponent<Image>().color;
    fadeCol.a = alpha;
    introPanel.GetComponent<Image>().color = fadeCol;
  }

  public void StartFadeIntro(){
    StartCoroutine(FadeIntro());
  }

  IEnumerator FadeIntro(){
    //GUIManager.instance.GetComponent<PanelManager>().StartPanelManager();
    iTween.ValueTo(introPanel, iTween.Hash(
      "from", 1,
      "to", 0,
      "time", .5f,
      "onupdatetarget", gameObject,
      "onupdate", "FadeIntroWrap"));
    PanelManager.instance.StartGame();
    yield return new WaitForSeconds(.2f);
    introPanel.transform.Find("Title").gameObject.SetActive(false);
    introPanel.transform.Find("Desc").gameObject.SetActive(false);
    introPanel.transform.Find("Image").gameObject.SetActive(false);
    //PanelManager.instance.StartGame();
    yield return new WaitForSeconds(.3f);
    //PanelManager.instance.StartGame();
    introPanel.SetActive(false);
    ShowBoard();
  }

  public void FadeIntroWrap(float alpha)
  {
    Color fadeCol = introPanel.GetComponent<Image>().color;
    fadeCol.a = alpha;
    introPanel.GetComponent<Image>().color = fadeCol;
  }



  //private IEnumerator BreakBuff()
  //{
  //  if (buff > 0)
  //  {
  //    //iTween.ShakePosition(buffWrap, new Vector3(1, 0, 0), .5f);
  //    //yield return new WaitForSeconds(.5f);
  //    iTween.ValueTo(buffWrap, iTween.Hash(
  //      "from", 1,
  //      "to", 0,
  //      "time", .5f,
  //      "onupdatetarget", gameObject,
  //      "onupdate", "FadeOverlayWrap"));
  //    yield return new WaitForSeconds(.5f);

  //  }
  //  buff = 0;
  //  buffWrap.SetActive(false);
  //}

  //public void FadeOverlayWrap(float alpha)
  //{
  //  Color fadeCol = buffWrap.GetComponent<Image>().color;
  //  fadeCol.a = alpha;
  //  buffWrap.GetComponent<Image>().color = fadeCol;
  //}



  public void LevelUpSkillClick(int pos){
    Debug.Log("Click at: " + pos.ToString());
    if (choosingSkill) {
      skillSelected = pos;
    }
  }

  private IEnumerator shiftLvlUpBackgroundColors(){

    System.Collections.Hashtable hash = new System.Collections.Hashtable();
    hash.Add("amount", new Vector3(.35f, .35f, 0f));
    hash.Add("time", 1f);
    iTween.PunchScale(GameObject.Find("lvlOutline"), hash);

    //Color currentColor = lvlUpPanel.GetComponent<Image>().color;

    //Color color1 = new Color32(0x75, 0x9A, 0x8F, 0xFF);
    //Color color2 = new Color32(0x9F, 0xC1, 0xB7, 0xFF);
    //Color color3 = new Color32(0xD1, 0xE7, 0xE1, 0xFF);
    //Color color4 = new Color32(0x9F, 0xC1, 0xB7, 0xFF);

    //if (currentColor.Equals(color1)) {
    //  lvlUpPanel.GetComponent<Image>().color = color2;
    //} else if (currentColor.Equals(color2)) {
    //  lvlUpPanel.GetComponent<Image>().color = color3;
    //} else if (currentColor.Equals(color3)) {
    //  lvlUpPanel.GetComponent<Image>().color = color4;
    //} else {
    //  lvlUpPanel.GetComponent<Image>().color = color1;
    //}

    yield return new WaitForSeconds(2f);
    backgroundShift = shiftLvlUpBackgroundColors();
    StartCoroutine(backgroundShift);
  }

  public IEnumerator IncrementLvlUpTxt(PlayerRosterMeta meta, int[] lvlInfoAfter, float[] increases, int healthInc)
  {
    for (float i = 1; i < 101; i++){
      GameObject.Find("HealthTxt").GetComponent<Text>().text = "Health: " + meta.maxHealth.ToString() + " (<color=#ff0000>+" + ((i / 100f) * healthInc) + "</color>)";
      GameObject.Find("ExpTxt").GetComponent<Text>().text = "Exp: " + meta.exp.ToString() + " (<color=#0000ff>" + ((i / 100f) * (lvlInfoAfter[2] - lvlInfoAfter[1])).ToString() + "</color>)";
      GameObject.Find("Stat01Txt").GetComponent<Text>().text = "Lust: " + meta.lust.ToString("0.00") + " (<color=#ff0000>+" + ((i / 100f) * increases[0]).ToString("0.00") + "</color>)";
      GameObject.Find("Stat02Txt").GetComponent<Text>().text = "Greed: " + meta.greed.ToString("0.00") + " (<color=#ff0000>+" + ((i / 100f) * increases[1]).ToString("0.00") + "</color>)";
      GameObject.Find("Stat03Txt").GetComponent<Text>().text = "Wrath: " + meta.wrath.ToString("0.00") + " (<color=#ff0000>+" + ((i / 100f) * increases[2]).ToString("0.00") + "</color>)";
      GameObject.Find("Stat04Txt").GetComponent<Text>().text = "Pride: " + meta.pride.ToString("0.00") + " (<color=#ff0000>+" + ((i / 100f) * increases[3]).ToString("0.00") + "</color>)";
      GameObject.Find("Stat05Txt").GetComponent<Text>().text = "Glutt: " + meta.gluttony.ToString("0.00") + " (<color=#ff0000>+" + ((i / 100f) * increases[4]).ToString("0.00") + "</color>)";
      GameObject.Find("Stat06Txt").GetComponent<Text>().text = "Sloth: " + meta.sloth.ToString("0.00") + " (<color=#ff0000>+" + ((i / 100f) * increases[5]).ToString("0.00") + "</color>)";
      GameObject.Find("Stat07Txt").GetComponent<Text>().text = "Envy: " + meta.envy.ToString("0.00") + " (<color=#ff0000>+" + ((i / 100f) * increases[6]).ToString("0.00") + "</color>)";
      yield return new WaitForSeconds(.05f);
    }
    yield return null;
  }

  public void ShowLevelUpScreen(PlayerRosterMeta meta, int exp)
  {
    lvlUpPanel.SetActive(true);
    boardManager.SetActive(false);
    backgroundShift = shiftLvlUpBackgroundColors();
    StartCoroutine(backgroundShift);

    Glossary glossy = PanelManager.instance.glossaryObj.GetComponent<Glossary>();
    MonsterMeta fullMeta = glossy.GetMonsterMain(meta.name).meta;

    int[] lvlInfoBefore = MonsterMeta.CalcLvl(meta, fullMeta.lvlSpeed);
    meta.exp += exp;
    int[] lvlInfoAfter = MonsterMeta.CalcLvl(meta, fullMeta.lvlSpeed);

    List<string> newSkills = new List<string>(meta.skills);
    float[] increases = new float[] { 0,0,0,0,0,0,0 };
    int lvlsGained = lvlInfoAfter[0] - lvlInfoBefore[0];
    for (int i = 0; i < lvlsGained; i++){
      float[] updates = MonsterMeta.returnLvlUpdates(fullMeta, meta);
      increases[0] += updates[0];
      increases[1] += updates[1];
      increases[2] += updates[2];
      increases[3] += updates[3];
      increases[4] += updates[4];
      increases[5] += updates[5];
      increases[6] += updates[6];

      foreach (lvlUpSkills skill in fullMeta.skillsGainedOnLvlUp){
        if (meta.lvl + i + 1 == skill.lvl){
          newSkills.Add(skill.skill);
        }
      }
    }

    if (newSkills.Count > 4)
    {
      choosingSkill = true;
      GameObject.Find("NxtBtnTxt").GetComponent<Text>().text = "Toss";
      GameObject.Find("WarningTxt").GetComponent<Text>().enabled = true;
      skillSelected = -1;
    }
    else
    {
      GameObject.Find("WarningTxt").GetComponent<Text>().enabled = false;
    }

    meta.lust += increases[0];
    meta.greed += increases[1];
    meta.wrath += increases[2];
    meta.pride += increases[3];
    meta.gluttony += increases[4];
    meta.sloth += increases[5];
    meta.envy += increases[6];

    int healthInc = 0;

    meta.gluttony_bonus += (meta.gluttony + .45f) * lvlsGained;
    if (meta.gluttony_bonus >= 1)
    {
      healthInc = (int)meta.gluttony_bonus;
      meta.maxHealth += healthInc;
      meta.gluttony_bonus = meta.gluttony_bonus - ((int)meta.gluttony_bonus);
    }

    GameObject.Find("LImg").GetComponent<Image>().sprite = glossy.GetMonsterImage(meta.name);
    GameObject.Find("Llvl").GetComponent<Text>().text = lvlInfoAfter[0].ToString();
    GameObject.Find("LName").GetComponent<Text>().text = meta.name;

    //GameObject.Find("HealthTxt").GetComponent<Text>().text = "Health: " + meta.maxHealth.ToString() + " (<color=#ff0000>+" + healthInc + "</color>)";
    //GameObject.Find("ExpTxt").GetComponent<Text>().text = "Exp: " + meta.exp.ToString() + " (<color=#0000ff>" + (lvlInfoAfter[2] - lvlInfoAfter[1]).ToString() + "</color>)";
    //GameObject.Find("Stat01Txt").GetComponent<Text>().text = "Lust: " + meta.lust.ToString("0.00") + " (<color=#ff0000>+" + increases[0].ToString("0.00") + "</color>)";
    //GameObject.Find("Stat02Txt").GetComponent<Text>().text = "Greed: " + meta.greed.ToString("0.00") + " (<color=#ff0000>+" + increases[1].ToString("0.00") + "</color>)";
    //GameObject.Find("Stat03Txt").GetComponent<Text>().text = "Wrath: " + meta.wrath.ToString("0.00") + " (<color=#ff0000>+" + increases[2].ToString("0.00") + "</color>)";
    //GameObject.Find("Stat04Txt").GetComponent<Text>().text = "Pride: " + meta.pride.ToString("0.00") + " (<color=#ff0000>+" + increases[3].ToString("0.00") + "</color>)";
    //GameObject.Find("Stat05Txt").GetComponent<Text>().text = "Gluttony: " + meta.gluttony.ToString("0.00") + " (<color=#ff0000>+" + increases[4].ToString("0.00") + "</color>)";
    //GameObject.Find("Stat06Txt").GetComponent<Text>().text = "Sloth: " + meta.sloth.ToString("0.00") + " (<color=#ff0000>+" + increases[5].ToString("0.00") + "</color>)";
    //GameObject.Find("Stat07Txt").GetComponent<Text>().text = "Envy: " + meta.envy.ToString("0.00") + " (<color=#ff0000>+" + increases[6].ToString("0.00") + "</color>)";
    StartCoroutine(IncrementLvlUpTxt(meta, lvlInfoAfter, increases, healthInc));
    GameObject.Find("Stat08Txt").GetComponent<Text>().text = "Lrn: " + fullMeta.lvlSpeed.ToString();

    List<string> mSkills = new List<string>(meta.skills);

    //for (int i = 0; i < 4; i++){
    //  if (newSkills.Count > i) {
    //    GameObject.Find("LSk0" + (i + 1) + "C").GetComponent<Text>().text =  mSkills.Contains(newSkills[i]) ? newSkills[i] : "<color=#0000ff>" + newSkills[i] + "</color>";
    //  } else {
    //    GameObject.Find("LSk0" + (i + 1) + "C").GetComponent<Text>().text = "";
    //  }
    //}

    //GameObject.Find("LSk05C").GetComponent<Text>().text = "";

    loadLvlUpSkills(mSkills, newSkills);

    meta.skills = newSkills.ToArray();
    meta.curHealth = meta.maxHealth;

    lastMeta = meta;
    //PanelManager.instance.updateCurrent(meta);
  }

  public void loadLvlUpSkills(List<string> mSkills, List<string> newSkills){
    List <SkillMeta> Skills = new List<SkillMeta>(GameUtilities.parseSkills(newSkills.ToArray(), PanelManager.instance.glossaryObj.GetComponent<Glossary>()));
    for (int i = 0; i < 4; i++)
    {
      if (newSkills.Count > i)
      {
        GameObject.Find("LSk0" + (i + 1) + "C").GetComponent<Text>().text = mSkills.Contains(newSkills[i]) ? newSkills[i] : "<color=#0000ff>" + newSkills[i] + "</color>";
        GameObject.Find("LSk0" + (i + 1) + "D").GetComponent<Text>().text = PanelManager.getEffectsString(Skills[i].effects);
      }
      else
      {
        GameObject.Find("LSk0" + (i + 1) + "C").GetComponent<Text>().text = "";
        GameObject.Find("LSk0" + (i + 1) + "D").GetComponent<Text>().text = "";
      }
    }
    if (newSkills.Count > 4) {
      GameObject.Find("LSk05C").GetComponent<Text>().text = "<color=#0000ff>" + newSkills[4] + "</color>";
      GameObject.Find("LSk05D").GetComponent<Text>().text = PanelManager.getEffectsString(Skills[4].effects);
    } else {
      GameObject.Find("LSk05C").GetComponent<Text>().text = "";
      GameObject.Find("LSk05D").GetComponent<Text>().text = "";
    }
  }

  public void HideLevelUpScreen(){
    if (choosingSkill && skillSelected > -1)
    {
      List<string> skills = new List<string>(lastMeta.skills);
      skills.RemoveAt(skillSelected - 1);
      loadLvlUpSkills(skills, skills);
      lastMeta.skills = skills.ToArray();
      if (lastMeta.skills.Length < 5) {
        choosingSkill = false;
        GameObject.Find("NxtBtnTxt").GetComponent<Text>().text = "Next";
        GameObject.Find("WarningTxt").GetComponent<Text>().enabled = false;
        skillSelected = -1;
      }
    }
    else if (!choosingSkill)
    {
      StopCoroutine(backgroundShift);
      PanelManager.instance.updateCurrent(lastMeta);
      lvlUpPanel.SetActive(false);
      boardManager.SetActive(true);
      PanelManager.instance.continueUpdate();
    }
  }

  public void EndGame(bool win, bool caught, bool killed, AdventureMeta meta){
    StartCoroutine(WaitForShifting(win, caught, killed, meta));
  }

	// Show the game over panel
  private void GameOver(bool win, bool caughtSomething, bool killed, AdventureMeta meta) {
		GameManager.instance.gameOver = true;
    boardManager.SetActive(false);
    if (GameObject.Find("MCatchPanelHolder") != null) {
      GameObject.Find("MCatchPanelHolder").SetActive(false);
    }

		gameOverPanel.SetActive(true);

    Text outcome = GameObject.Find ("OutComeTxt").GetComponent<Text> ();

    // We have beaten a wild monster
    if (!meta.isTrainerEncounter){
      if (win)
      {
        catchPanel.SetActive(true);

        if (caughtSomething)
        {
          outcome.text = "Monster Caught!";
          populateCatchPanel(false, false, meta.wild);
        }
        else if (killed)
        {
          outcome.text = "Monster Slaughtered!";
          populateCatchPanel(true, false, meta.wild);
        }
        else
        {
          outcome.text = "Monster Escaped...";
          populateCatchPanel(false, true, meta.wild);
        }
      }
      else
      {
        catchPanel.SetActive(false);
        outcome.text = "Wild Monster Wins...";
        restorePlayerToLastHeal();
      }
    // We have beaten a trainer
    } else {
      trainerPanel.SetActive(true);
      Glossary glossy = PanelManager.instance.glossaryObj.GetComponent<Glossary>();
      int yenGained = win ? GameUtilities.getNPCYenValue(meta) : 0;
      trainerPanel.GetComponent<TrainerGameoverController>().updateMonsterUI(meta.trainer, glossy, yenGained);
      if (win) {
        outcome.text = "Trainer Defeated";
        AdventureMeta adventure = BaseSaver.getAdventure();
        adventure.addYen(yenGained);
        adventure.UpdateTempleList(adventure.trainer.name);
        BaseSaver.putAdventure(adventure);
      } else {
        outcome.text = "Trainer Wins...";
        restorePlayerToLastHeal();
      }
    }
	}

  void restorePlayerToLastHeal(){
    //Glossary glossy = PanelManager.instance.glossaryObj.GetComponent<Glossary>();
    //PlayerPrefs.DeleteAll();
    ////BaseSaver.setMapName("CobblesTown");
    ////PlayerRosterMeta monster1 = MonsterMeta.returnMonster(glossy.GetMonsterMain("Beanlock").meta, 1);
    //AdventureMeta meta = new AdventureMeta();
    //meta.roster = GameManager.returnTestRoster(glossy);
    ////meta.roster = new PlayerRosterMeta[] { monster1 };
    //BaseSaver.putAdventure(meta);
    BaseSaver.restoreState();
  }

  void populateCatchPanel(bool monsterDead, bool monsterEscape, PlayerRosterMeta monsterFaught){
    Debug.Log("populateCatchPanel: " + monsterDead.ToString() + ":" + monsterEscape.ToString());

    if (monsterDead) {
      GameObject.Find("CDead").SetActive(true);
    } else {
      GameObject.Find("CDead").SetActive(false);
    }

    if (monsterEscape)
    {
      GameObject.Find("CEscape").SetActive(true);
    } else {
      GameObject.Find("CEscape").SetActive(false);
    }

    AdventureMeta meta = BaseSaver.getAdventure();

    Glossary glossy = PanelManager.instance.glossaryObj.GetComponent<Glossary>();
    GameObject.Find("CImg").GetComponent<Image>().sprite = glossy.GetMonsterImage(monsterFaught.name);
    GameObject.Find("CName").GetComponent<Text>().text = monsterFaught.name;
    GameObject.Find("CHealth").GetComponent<Image>().fillAmount = (float) monsterFaught.curHealth / (float) monsterFaught.maxHealth;
    GameObject.Find("CHealthTxt").GetComponent<Text>().text = monsterFaught.curHealth.ToString() + "/" + monsterFaught.maxHealth.ToString();
    GameObject.Find("Clvl").GetComponent<Text>().text = monsterFaught.lvl.ToString();

    for (int i = 0; i < 4; i++){
      if (monsterFaught.skills.Length > i) {
        GameObject.Find("CSk0" + (i + 1) + "C").GetComponent<Text>().text = monsterFaught.skills[i];
      } else {
        GameObject.Find("CSk0" + (i + 1) + "C").SetActive(false);
      }
    }
  }

  private IEnumerator WaitForShifting(bool win, bool caughtSomething, bool killed, AdventureMeta meta) {
    yield return new WaitUntil(()=> !BoardManager.instance.IsThinking());
    yield return new WaitForSeconds(.25f);
    GameOver(win, caughtSomething, killed, meta);
  }

  private IEnumerator WaitForShifting(PlayerRosterMeta abbvMeta, int exp)
  {
    yield return new WaitUntil(() => !BoardManager.instance.IsThinking());
    yield return new WaitForSeconds(.25f);
    ShowLevelUpScreen(abbvMeta, exp);
  }
}
