using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel.ExpTypes
{
  public class FlucExpType : BaseExpType
  {
    public override int CalculateExp(int lvl)
    {
      if (lvl <= 15)
      {
        return (int)Math.Floor((double)Math.Pow(lvl, 3) * ((((lvl + 1) / 3)+24)/50));
      } else if (lvl <= 36)
      {
        return (int)Math.Floor((double)Math.Pow(lvl, 3) * ((lvl + 14) / 50));
      } else
      {
        return (int)Math.Floor((double)Math.Pow(lvl, 3) * (((lvl / 2) + 32) / 50));
      }
    }
  }
}
