using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogMeta {
  [TextArea]
  public string msg;

  public DialogMeta(string msg)
  {
    this.msg = msg;
  }

  public DialogMeta(DialogMeta meta){
    this.msg = meta.msg;
  }

  public bool getFight(){
    return msg.Contains("<Fight/>");
  }

  public bool getHeal()
  {
    return msg.Contains("<Heal/>");
  }

  public bool getShop()
  {
    return msg.Contains("<Shop/>");
  }

  public string msgNoTags(){
    return msg.Replace("<Fight/>", "").Replace("<Heal/>", "").Replace("<Shop/>","");
  }
}
