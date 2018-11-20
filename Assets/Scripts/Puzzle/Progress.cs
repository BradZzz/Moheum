using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Progress : MonoBehaviour {
  public int MAX_HEALTH = 20;
  Image foregroundImage;
  public int progress;

  void Start () {
    foregroundImage = gameObject.GetComponent<Image>();   
  } 

  public void updateHealth(int health){
    progress = health;
    MAX_HEALTH = health;
    updateHealth ();
  }

  private void updateHealth(){
    GameObject.Find (gameObject.name [0] + "HealthTxt").GetComponent<Text> ().text = progress.ToString() + " / " + MAX_HEALTH.ToString ();
  }

  public void UpdateProgress(int newVal)
  {
    Hashtable param = new Hashtable();
    param.Add("from", progress);
    param.Add("to", newVal);
    param.Add("time", .1f);
    param.Add("onupdate", "TweenedSomeValue");         
    param.Add("onComplete", "OnFullProgress");
    param.Add("onCompleteTarget", gameObject);
    iTween.ValueTo(gameObject, param);    
  }

  public void TweenedSomeValue (int val)
  {
    progress = val;
    gameObject.GetComponent<Image>().fillAmount = progress / (float) MAX_HEALTH;
    updateHealth ();
  }

  public void OnFullProgress()
  {
    //Debug.Log ("OnFullProgress: Done");
    StartCoroutine(lowHealthFlash());
    PanelManager.instance.playerActed();
  }

  private IEnumerator lowHealthFlash(){
    if (progress / (float)MAX_HEALTH < .2f)
    {
      Color fadeColor = gameObject.GetComponent<Image>().color;
      fadeColor.a = .2f;
      gameObject.GetComponent<Image>().color = fadeColor;
      yield return new WaitForSeconds(.1f);
      fadeColor.a = 1f;
      gameObject.GetComponent<Image>().color = fadeColor;
      yield return new WaitForSeconds(1.2f);
      StartCoroutine(lowHealthFlash());
    } else {
      Color fadeColor = gameObject.GetComponent<Image>().color;
      fadeColor.a = 1f;
      gameObject.GetComponent<Image>().color = fadeColor;
    }
  }
}