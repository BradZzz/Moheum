using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Patterns;
using UnityEngine;

namespace Battle.UI.ModelJewel.Mechanics
{
  public class UnselectJewelMechanics : BaseJewelMechanics
  {
    public UnselectJewelMechanics(IRuntimeJewel jewel) : base(jewel)
    {
      this.jewel = jewel;
      this.jewel.OnUnSelect += OnUnselect;
    }

    private IRuntimeJewel jewel;

    public void OnUnselect(IRuntimeJewel Jewel)
    {
      jewel.IsSelected = false;
    }
  }
}
