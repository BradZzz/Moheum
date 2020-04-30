using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Patterns;
using UnityEngine;

namespace Battle.UI.ModelJewel.Mechanics
{
  public class JewelFallMechanics : BaseJewelMechanics
  {
    public JewelFallMechanics(IRuntimeJewel jewel) : base(jewel)
    {
      this.jewel = jewel;
    }

    private IRuntimeJewel jewel;

    public void OnFall(IRuntimeJewel Jewel)
    {
      jewel.IsSelected = false;
    }
  }
}
