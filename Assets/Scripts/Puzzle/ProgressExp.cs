using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressExp : MonoBehaviour {

  Image foregroundImage;
  public int progress;
  public int MAX_EXP = 20;

  private MonsterMeta.lvlUpSpeed lvlUpSpeed;
  private PlayerRosterMeta meta;

  void Start () {
    foregroundImage = gameObject.GetComponent<Image>();   
  } 

  public void UpdateExperience(MonsterMeta.lvlUpSpeed lvlUpSpeed, PlayerRosterMeta meta){
    //Debug.Log("UpdateExperience");
    //Debug.Log(meta.ToString());

    this.lvlUpSpeed = lvlUpSpeed;
    this.meta = new PlayerRosterMeta(meta);

    //Returns [level, currentLvlExp, neededExp]
    int[] lvlCalc = MonsterMeta.CalcLvl(meta, lvlUpSpeed);
    meta.lvl = lvlCalc[0];
    progress = lvlCalc[1];
    MAX_EXP = lvlCalc[2];

    gameObject.GetComponent<Image>().fillAmount = progress / (float)MAX_EXP;
    GameObject.Find("HLvlTxt").GetComponent<Text>().text = lvlCalc[0].ToString();
    //Debug.Log("updateExp init: " + lvlCalc[0].ToString() + "/" + lvlCalc[1].ToString());
  }

  public void UpdateProgress(int exp)
  {

    Debug.Log ("UpdateProgress Exp: " + exp.ToString());

    Hashtable param = new Hashtable();
    param.Add("from", progress);
    param.Add("to", progress + exp);
    param.Add("time", 2f);
    param.Add("onupdate", "TweenedSomeValue");         
    param.Add("onComplete", "OnFullProgress");
    param.Add("onCompleteTarget", gameObject);
    iTween.ValueTo(gameObject, param);    
  }

  public void TweenedSomeValue (int val)
  {
    progress = val;
    //Debug.Log("Tween Monster: " + meta.ToString());
    int[] lvlCalc = MonsterMeta.CalcLvl(meta, lvlUpSpeed);
    meta.exp += progress - lvlCalc[1];
    UpdateExperience(lvlUpSpeed, meta);
  }

  public void OnFullProgress()
  {
    Debug.Log ("OnFullProgress: Done");
    Debug.Log("New Monster: " + meta.ToString());
  }
}