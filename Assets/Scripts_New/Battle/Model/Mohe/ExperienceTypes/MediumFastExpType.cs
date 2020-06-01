using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel.ExpTypes
{
  public class MediumFastExpType : BaseExpType
  {
    public override int CalculateExp(int lvl)
    {
      return (int)Math.Floor((double)Math.Pow(lvl, 3));
    }
  }
}
