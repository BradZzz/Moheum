using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BackgroundMeta {
  public string name;
  public Sprite background1;
  public Sprite background2;
  public Sprite background3;
  public Sprite background4;
  public Sprite background5;

  public void LoadIntoTransform(Transform parent){
    parent.Find("Bk1").GetComponent<Image>().sprite = background1;
    parent.Find("Bk2").GetComponent<Image>().sprite = background2;
    parent.Find("Bk3").GetComponent<Image>().sprite = background3;
    parent.Find("Bk4").GetComponent<Image>().sprite = background4;
    parent.Find("Bk5").GetComponent<Image>().sprite = background5;
  }
}
