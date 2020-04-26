using System.Collections.Generic;
using UnityEngine;
using System;
using Battle.Data;

namespace Battle.Model.Jewel
{
    public interface IJewelData : IBaseData
    {
      JewelID JewelID { get; }
      string Lore { get; }
      //string description { get; }
      //string lore { get; }
      //Sprite artwork { get; }
      //RelicRarityType RType { get; }
      //EffectsSet Effects { get; }
  }
}