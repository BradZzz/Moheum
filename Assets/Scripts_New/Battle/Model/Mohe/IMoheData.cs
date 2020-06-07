using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IMoheData
  {
    MoheID MoheID { get; }
    string Name { get; }
    string Description { get; }
    Sprite Artwork { get; }

    MoheData.MoheStatData BaseStatsLow { get; }
    MoheData.MoheStatData BaseStatsHigh { get; }
    MoheData.MoheStatData LvlUpStatsLow { get; }
    MoheData.MoheStatData LvlUpStatsHigh { get; }

    MoheExperienceGain ExperienceType { get; }

    List<MoheData.MoheAbilityLevelData> Abilities { get; }

    /*
     * Need to add abilities here
     */
  }
}
