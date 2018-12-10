using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonTreasMeta : TreasureMeta
{
  public MonTreasMeta(MonTreasMeta meta, PosMeta pos)
  {
    this.description = meta.description;
    this.effects = meta.effects;
    this.value = meta.value;
    this.pos = pos;
  }

  public Type effects;
  public int value;

  public enum Type
  {
    Exp, Heal, Money, Revive, Skills, Stats, XBuff, None
  }
}
