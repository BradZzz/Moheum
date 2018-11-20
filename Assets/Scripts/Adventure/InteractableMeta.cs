using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InteractableMeta {
  public QuestMeta[] questsToAppear;
  public QuestMeta[] questsToDisappear;

  [HideInInspector]
  public bool visible;
}
