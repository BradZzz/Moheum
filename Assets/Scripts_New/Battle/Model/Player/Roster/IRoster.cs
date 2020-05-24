using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.MoheModel;
using Patterns;
using UnityEngine;

namespace Battle.Model.Player
{
  public interface IRoster
  {
    int CurrentIdx { get; }
    List<IRuntimeMoheData> MoheRoster { get; }

    // Set roster to specific index
    void SetRoster(int idx);
    // Set roster to next available Mohe
    void AutoRoster();
    // Check if all Mohe are dead
    bool AllVanquished();
    // Current Mohe
    IRuntimeMoheData CurrentMohe();
  }
}