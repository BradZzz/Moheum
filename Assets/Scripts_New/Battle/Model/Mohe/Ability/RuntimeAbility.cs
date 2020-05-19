using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class RuntimeAbility : IRuntimeAbility
  {
    public RuntimeAbility(IAbility ability)
    {
      name = ability.Name;
      desc = ability.Desc;
      effect = ability.Effect;
      abilityComponents = new List<IRuntimeAbilityComponent>();
      foreach (IAbilityComponent comp in ability.AbilityComponents)
      {
        abilityComponents.Add(new RuntimeAbilityComponent(comp));
      }
    }

    public string Name => name;
    public string Desc => desc;
    public List<IRuntimeAbilityComponent> AbilityComponents => abilityComponents;
    public IAbilityEffect Effect => effect;

    private string name;
    private string desc;
    private List<IRuntimeAbilityComponent> abilityComponents;
    private IAbilityEffect effect;

    public void PowerAbility(JewelID jewel, int amount)
    {
      foreach (IRuntimeAbilityComponent comp in abilityComponents)
      {
        comp.PowerComponent(jewel, amount);
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
