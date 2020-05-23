using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IRuntimeMoheData
  {
    int Health { get; }
    IMohe BaseMohe { get; }
    List<IRuntimeAbility> Abilities { get; }

    void PopulateAbilities(JewelID jewel, int amount);
    bool MoheDead();
  }
}
