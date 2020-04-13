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
        //RelicRarityType RType { get; }
        //EffectsSet Effects { get; }
    }
}