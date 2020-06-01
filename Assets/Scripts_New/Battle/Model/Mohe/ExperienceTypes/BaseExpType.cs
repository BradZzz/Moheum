using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel.ExpTypes
{
  public abstract class BaseExpType : IBaseExpType
  {
    public abstract int CalculateExp(int lvl);

    public int CalculateLevel(int exp)
    {
      int total = 0;
      int lvl = 1;
      while (total < exp)
      {
        total += CalculateLevel(lvl);
        lvl++;
      }

      return lvl;
    }

    public int IntFromLastLevel(int exp)
    {
      int prevTotal = 0;
      int total = 0;
      int lvl = 1;
      while (total < exp)
      {
        prevTotal = total;
        total += CalculateLevel(lvl);
        lvl++;
      }

      return prevTotal;
    }

    public int IntToNextLevel(int exp)
    {
      int total = 0;
      int lvl = 1;
      while (total < exp)
      {
        total += CalculateLevel(lvl);
        lvl++;
      }

      return total;
    }

    public static BaseExpType TranslateType(MoheExperienceGain expType)
    {
      switch (expType)
      {
        case MoheExperienceGain.Fast: return new FastExpType();
        case MoheExperienceGain.MediumFast: return new MediumFastExpType();
        case MoheExperienceGain.MediumSlow: return new MediumSlowExpType();
        case MoheExperienceGain.Slow: return new SlowExpType();
        case MoheExperienceGain.Fluctuating: return new FlucExpType();
        default: return new FastExpType();
      }
    }
  }
}
