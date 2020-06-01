using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel.ExpTypes
{
  public class MediumSlowExpType : BaseExpType
  {
    public override int CalculateExp(int lvl)
    {
      return (int)Math.Floor((double) (6/5 * (Math.Pow(lvl, 3))) - (15 * Math.Pow(lvl, 2)) + (100 * lvl) - 140);
    }
  }
}
