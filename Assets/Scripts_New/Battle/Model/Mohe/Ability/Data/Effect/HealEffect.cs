using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  [CreateAssetMenu(menuName = "Effect/Heal")]
  public class HealEffect : BaseEffect
  {
    public override bool Execute(IRuntimeJewel TriggerJewel)
    {
      return false;
    }
  }
}
