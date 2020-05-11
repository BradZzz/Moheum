using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class RuntimeAbilityComponent : IRuntimeAbilityComponent
  {
    public RuntimeAbilityComponent(IAbilityComponent AbilityComponent)
    {
      jewelType = AbilityComponent.JewelType;
      has = 0;
      needs = AbilityComponent.Needs;
    }

    public JewelID JewelType => jewelType;
    public int Has => has;
    public int Needs => needs;

    private JewelID jewelType;
    private int has;
    private int needs;

    public void PowerComponent(JewelID JewelType, int amount)
    {
      if (JewelType == jewelType)
      {
        has += amount;
      }
      if (has > needs)
      {
        has = needs;
      }
    }

    public void ResetComponent()
    {
      has = 0;
    }

    public bool IsPowered()
    {
      return has >= needs;
    }
  }
}
