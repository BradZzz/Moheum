using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorySceneHolder : MonoBehaviour {

  public GameObject glossary;

  //public GameObject story;
  //public sceneType scene;

  //public enum sceneType
  //{
  //  backdrop, slideshow, none
  //};

  private string[] textHolder;
  private string nextScene;
  private StoryHolder meta;

  private Text textBox;
  private int idx;
  private int txtIdx = 0;
  private float percentsPerSecond = 0.2f;
  private float sceneProgress = 0;

  private List<Vector3> waypointArray = new List<Vector3>();
  private List<GameObject> storyImages;

  private GameObject clickToContinue;

  // Use this for initialization
  void Awake () {
    string storyName = BaseSaver.getSlideshow();
    Glossary glossy = glossary.GetComponent<Glossary>();
    meta = glossy.GetStory(storyName);
    textHolder = meta.textHolder;
    nextScene = meta.nextScene;
    clickToContinue = GameObject.Find("ClickToContinue");
    int imgCount = 1;
    storyImages = new List<GameObject>();
    while (true)
    {
      GameObject thisImg = GameObject.Find("StoryImage_0" + imgCount.ToString());
      if (thisImg == null)
      {
        break;
      }
      imgCount++;
      storyImages.Add(thisImg);
    }
    clickToContinue.SetActive(false);
  }

  // Use this for initialization
  public void Start()
  {
    idx = 0;
    textBox = GameObject.Find("StoryText").GetComponent<Text>();
    textBox.text = textHolder[idx];
    if (meta.scene == StoryHolder.sceneType.backdrop) {
      moveStoryShit(storyImages);
    } else {
      Color backdrop = Color.gray;
      storyImages[0].GetComponent<Image>().color = backdrop;
      for (int i = 1; i < storyImages.Count; i++) {
        storyImages[i].SetActive(false);
      }
    }
    setSlide();
    StartCoroutine(AnimateText());
  }

  void setSlide(){
    if (meta.scene == StoryHolder.sceneType.slideshow && meta.spriteHolder.Length > idx)
    {
      storyImages[0].GetComponent<Image>().sprite = meta.spriteHolder[idx];
    }
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    //moveStoryShit();
    if (Input.anyKeyDown)
    {
      if (idx < textHolder.Length - 1 || txtIdx < textHolder[idx].Length - 1)
      {
        if (txtIdx < textHolder[idx].Length - 1)
        {
          textBox.text = textHolder[idx];
          txtIdx = textHolder[idx].Length - 1;
          clickToContinue.SetActive(true);
        }
        else
        {
          clickToContinue.SetActive(false);
          SkipToNextText();
        }
      }
      else
      {
        if (BaseSaver.getSlideshow() != "TutorialBattleTrainer")
        {
          if (BaseSaver.getSlideshow() == "Intro")
          {
            BaseSaver.putSlideshow("TutorialWorld");
          } else if (BaseSaver.getSlideshow() == "TutorialWorld") {
            BaseSaver.putSlideshow("TutorialBattleWild");
          } else if (BaseSaver.getSlideshow() == "TutorialBattleWild") {
            BaseSaver.putSlideshow("TutorialBattleTrainer");
          }
          SceneManager.LoadSceneAsync("ScrollerScene");
        } else {
          SceneManager.LoadSceneAsync(nextScene);
        }
      }
    }
  }

  public void moveStoryShit(List<GameObject> storyImages)
  {
    /*
     * Foreground will move more than the background
     */
    for (int i = 1; i < storyImages.Count; i++)
    {
      if (i == 1)
      {
        iTween.MoveBy(storyImages[i], iTween.Hash("x", 30f, "time", 60f, "easetype", "linear", "looptype", iTween.LoopType.pingPong));
      }
      if (i == 2)
      {
        iTween.MoveBy(storyImages[i], iTween.Hash("x", -20f, "time", 20f, "easetype", "linear", "looptype", iTween.LoopType.pingPong));
      }
      if (i == 3) {
        iTween.MoveBy(storyImages[i], iTween.Hash("x", 5f, "time", 13f, "easetype", "linear", "looptype", iTween.LoopType.pingPong));
      }
      if (i == 4) {
        iTween.RotateBy(storyImages[i], iTween.Hash("z", .1f, "time", 40f, "easetype", "linear",  "looptype", iTween.LoopType.loop));
      }
    }
  }

  public void SkipToNextText()
  {
    StopAllCoroutines();
    idx++;
    setSlide();
    StartCoroutine(AnimateText());
  }

  //Note that the speed you want the typewriter effect to be going at is the yield waitforseconds (in my case it's 1 letter for every      0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
  IEnumerator AnimateText()
  {
    char[] stops = new char[] { '.','?','!'};
    for (txtIdx = 0; txtIdx < textHolder[idx].Length; txtIdx++)
    {
      textBox.text = textHolder[idx].Substring(0, txtIdx);
      if (txtIdx > 0 && Array.IndexOf(stops, textHolder[idx][txtIdx-1]) > -1){
        yield return new WaitForSeconds(1f);
      }
      yield return new WaitForSeconds(.08f);
    }
    clickToContinue.SetActive(true);
  }
}
