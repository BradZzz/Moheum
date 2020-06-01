using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel.ExpTypes
{
  public class SlowExpType : BaseExpType
  {
    public override int CalculateExp(int lvl)
    {
      return (int)Math.Floor((double)(5 * Math.Pow(lvl, 3)) / 4);
    }
  }
}
