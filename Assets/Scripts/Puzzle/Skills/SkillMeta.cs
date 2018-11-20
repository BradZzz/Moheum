using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillMeta {

  public SkillMeta(){
  }

  public SkillMeta(SkillMeta meta){
    this.name = meta.name;
    this.req1 = new SkillReq(meta.req1);
    this.req2 = new SkillReq(meta.req2);
    this.effects = meta.effects;
  }

  public string name;

  public SkillReq req1;
  public SkillReq req2;

  public SkillEffect[] effects;

  public bool hasReq(){
    return (req1 == null || req1.has == req1.req) && (req2 == null || req2.has == req2.req);
  }
}
