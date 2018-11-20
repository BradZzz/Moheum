using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillReq{
  public SkillReq(SkillReq req){
    this.gem = req.gem;
    this.req = req.req;
    this.has = req.has;
  }
  public SkillReq(){
  }
  public TileMeta.GemType gem;
  public int req;
  public int has;
}