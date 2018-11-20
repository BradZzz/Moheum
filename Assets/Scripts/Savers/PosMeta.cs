using System;
using UnityEngine;

[Serializable]
public class PosMeta {
  public float x;
  public float y;
  public float z;

  public PosMeta(float x, float y, float z){
    this.x = x;
    this.y = y;
    this.z = z;
  }
  public PosMeta(Vector3 pos)
  {
    this.x = pos.x;
    this.y = pos.y;
    this.z = pos.z;
  }
  public bool Equals(PosMeta pos){
    return (int)(this.x * 1000) == (int)(pos.x * 1000) 
      && (int)(this.y * 1000) == (int)(pos.y * 1000) 
      && (int)(this.z * 1000) == (int)(pos.z * 1000);
  }
  public bool Equals2(PosMeta pos)
  {
    return (int)(this.x * 10) == (int)(pos.x * 10)
      && (int)(this.y * 10) == (int)(pos.y * 10)
      && (int)(this.z * 10) == (int)(pos.z * 10);
  }
  public override string ToString()
  {
    return "(" + this.x.ToString() + "," + this.y.ToString() + "," + this.z.ToString() + ")";
  }
}
