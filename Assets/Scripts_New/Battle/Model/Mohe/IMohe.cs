using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IMohe
  {
    List<IAbility> Abilities { get; }
    IMoheData Data { get; }
    // Current Stats
    IMoheStats Stats { get; }
  }
}
