using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IMohe
  {
    List<IRuntimeAbility> Abilities { get; }
    IMoheData Data { get; }
    // Current Stats
    IMoheStats Stats { get; }
  }
}
