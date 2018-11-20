using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestMeta {
  public string questTarget;
  public QuestType goal;

  public enum QuestType {
    TalkTo, Defeated, InRoster, None
  }

  public override string ToString()
  {
    return "Quest Target: " + questTarget + " Type: " + goal.ToString();
  }
}
