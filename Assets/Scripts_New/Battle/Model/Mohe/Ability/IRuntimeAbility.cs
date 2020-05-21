using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IRuntimeAbility
  {
    List<IRuntimeAbilityComponent> AbilityComponents { get; }
    AbilityData Ability { get; }

    void PowerAbility(JewelID jewel, int amount);
    bool AbilityCharged();
  }
}
