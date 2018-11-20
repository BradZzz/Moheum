using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillMetaSerializable {
  public string name;

  public SkillReq req1;
  public SkillReq req2;

  public SkillEffectSerializable[] effects;
}
