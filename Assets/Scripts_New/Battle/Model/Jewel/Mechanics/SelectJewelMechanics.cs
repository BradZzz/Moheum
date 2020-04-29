using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Patterns;
using UnityEngine;

namespace Battle.UI.ModelJewel.Mechanics
{
  public class SelectJewelMechanics : BaseJewelMechanics
  {
    public SelectJewelMechanics(IRuntimeJewel jewel) : base(jewel)
    {
      this.jewel = jewel;
      this.jewel.OnSelect += OnSelect;
    }

    private IRuntimeJewel jewel;

    public void OnSelect(IRuntimeJewel Jewel)
    {
      jewel.IsSelected = !jewel.IsSelected;
    }
  }
}
