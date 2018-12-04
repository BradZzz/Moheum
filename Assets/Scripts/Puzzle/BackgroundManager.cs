using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {
  public BackgroundMeta[] backgrounds;

  private void Start()
  {
    string backName = "night";
    if (System.DateTime.Now.Hour > 8 && System.DateTime.Now.Hour <= 16) {
      backName = "day";
    }
    foreach(BackgroundMeta background in backgrounds)
    {
      if (background.name.Equals(backName)){
        background.LoadIntoTransform(transform);
      }
    }
  }
}
