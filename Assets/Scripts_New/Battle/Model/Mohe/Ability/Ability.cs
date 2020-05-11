using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class Ability : IAbility
  {
    public Ability(string Name, List<IAbilityComponent> AbilityComponents, IAbilityEffect Effect)
    {
      name = Name;
      abilityComponents = AbilityComponents;
      effect = Effect;
    }

    public string Name => Name;
    public List<IAbilityComponent> AbilityComponents => abilityComponents;
    public IAbilityEffect Effect => effect;

    private string name;
    private List<IAbilityComponent> abilityComponents;
    private IAbilityEffect effect;
  }
}
