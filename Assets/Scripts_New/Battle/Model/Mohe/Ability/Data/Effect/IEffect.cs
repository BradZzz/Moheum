using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IEffect
  {
    JewelID Jewel { get; }

    int MinAmt { get; }
    int MaxAmt { get; }
    PlayerSeat EffectPlayer { get; }

    bool Execute(IRuntimeJewel TriggerJewel);
  }
}
