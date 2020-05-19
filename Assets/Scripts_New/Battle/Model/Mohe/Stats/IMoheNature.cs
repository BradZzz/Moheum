using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IMoheNature
  {
    MoheStats BaseStatsLow { get; }
    MoheStats BaseStatsHigh { get; }

    MoheStats LvlUpStatsLow { get; }
    MoheStats LvlUpStatsHigh { get; }
  }
}
