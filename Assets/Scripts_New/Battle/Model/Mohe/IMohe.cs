using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IMohe
  {
    List<AbilityID> Abilities { get; }
    IMoheData Data { get; }
    // Current Stats
    MoheData.MoheStatData Stats { get; }
  }
}
