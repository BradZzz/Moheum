using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IRuntimeAbility : IBaseAbiliity
  {
    List<IRuntimeAbilityComponent> AbilityComponents { get; }

    void PowerAbility(JewelID jewel, int amount);
    bool AbilityCharged();
  }
}
