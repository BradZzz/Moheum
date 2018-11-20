using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryHolder : MonoBehaviour {

  public string storyName;
  [TextArea(10, 30)]
  public string[] textHolder;
  public string nextScene;
  public Sprite[] spriteHolder;
  public sceneType scene;

  public enum sceneType
  {
    backdrop, slideshow, none
  };
}
