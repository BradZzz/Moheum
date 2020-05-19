using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class Ability : IAbility
  {
    public Ability(string Name, string Desc, List<IAbilityComponent> AbilityComponents, IAbilityEffect Effect)
    {
      name = Name;
      desc = Desc;
      abilityComponents = AbilityComponents;
      effect = Effect;
    }

    public string Name => name;
    public string Desc => desc;
    public List<IAbilityComponent> AbilityComponents => abilityComponents;
    public IAbilityEffect Effect => effect;

    private string name;
    private string desc;
    private List<IAbilityComponent> abilityComponents;
    private IAbilityEffect effect;
  }
}
