using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class MoheData : IMoheData
  {
    public MoheData(string Name, Sprite Image, IMoheNature Nature)
    {
      name = Name;
      image = Image;
      nature = Nature;
    }

    public string Name => name;
    public Sprite Image => image;
    public IMoheNature Nature => nature;

    private string name;
    private Sprite image;
    private IMoheNature nature;
  }
}
