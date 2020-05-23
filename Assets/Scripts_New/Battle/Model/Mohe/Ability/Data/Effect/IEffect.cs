using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IEffect
  {
    JewelID Jewel { get; }

    int MinAmt { get; }
    int MaxAmt { get; }

    bool Execute(IRuntimeJewel TriggerJewel);
  }
}
