using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  [CreateAssetMenu(menuName = "Effect/Transform")]
  public class TransformEffect : BaseEffect
  {
    public override bool Execute(IRuntimeJewel TriggerJewel)
    {
      return false;
    }
  }
}
