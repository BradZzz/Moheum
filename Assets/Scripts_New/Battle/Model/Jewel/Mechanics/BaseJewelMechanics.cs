using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.ModelJewel.Mechanics
{
  public class BaseJewelMechanics
  {
    protected IRuntimeJewel Jewel { get; }

    protected BaseJewelMechanics(IRuntimeJewel jewel)
    {
      Jewel = jewel;
    }
  }
}