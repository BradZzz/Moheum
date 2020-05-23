using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class RuntimeAbility : IRuntimeAbility
  {
    public RuntimeAbility(AbilityData Ability)
    {
      ability = Ability;
      abilityComponents = new List<IRuntimeAbilityComponent>();
      foreach (AbilityData.AbilityCostData comp in ability.abilityCost)
      {
        abilityComponents.Add(new RuntimeAbilityComponent(comp));
      }
    }

    private AbilityData ability;
    private List<IRuntimeAbilityComponent> abilityComponents;

    public AbilityData Ability => ability;
    public List<IRuntimeAbilityComponent> AbilityComponents => abilityComponents;

    public void PowerAbility(JewelID jewel, int amount)
    {
      foreach (IRuntimeAbilityComponent comp in abilityComponents)
      {
        comp.PowerComponent(jewel, amount);
      }
    }

    public void ResetAbility()
    {
      foreach (IRuntimeAbilityComponent comp in abilityComponents)
      {
        comp.ResetComponent();
      }
    }

    public bool AbilityCharged()
    {
      bool charged = true;
      foreach (IRuntimeAbilityComponent comp in abilityComponents)
      {
        if (!comp.IsPowered())
        {
          charged = false;
          break;
        }
      }
      return charged;
    }
  }
}
