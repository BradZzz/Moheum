using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Patterns;
using UnityEngine;

namespace Battle.UI.ModelJewel.Mechanics
{
  public class TransformJewelMechanics : BaseJewelMechanics, ITransformJewel
  {
    public TransformJewelMechanics(IRuntimeJewel jewel) : base(jewel)
    {
      this.jewel = jewel;
    }

    private IRuntimeJewel jewel;

    public void OnTransformJewel(IRuntimeJewel jewel, JewelID transformType)
    {
      JewelData data = JewelDatabase.Instance.Get(transformType);
      jewel.TransformJewel(data);
    }
  }
}
