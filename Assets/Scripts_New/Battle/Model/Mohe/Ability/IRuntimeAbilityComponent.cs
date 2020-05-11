using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IRuntimeAbilityComponent : IAbilityComponent
  {
    int Has { get; }

    void PowerComponent(JewelID JewelType, int amount);
    bool IsPowered();
    void ResetComponent();
  }
}
