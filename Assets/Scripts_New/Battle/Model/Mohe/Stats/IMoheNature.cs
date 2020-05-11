using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IMoheNature
  {
    IMoheStats BaseStatsLow { get; }
    IMoheStats BaseStatsHigh { get; }

    IMoheStats LvlUpStatsLow { get; }
    IMoheStats LvlUpStatsHigh { get; }
  }
}
