using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoardMeta {
  public string mapName;
  public MonTreasMeta[] Items;
  public NPCMeta[] NPCs;
  public PosMeta playerPos;
}
