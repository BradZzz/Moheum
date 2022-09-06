using System.Collections.Generic;
using UnityEngine;
using System;
using Battle.Data;

namespace Battle.Model.Jewel
{
    public interface IJewelData : IBaseData
    {
      JewelID JewelID { get; }
      string Name { get; }
      string Description { get; }
      string Lore { get; }
      Sprite Artwork { get; }
      RuntimeAnimatorController Animator { get; }
    }
}