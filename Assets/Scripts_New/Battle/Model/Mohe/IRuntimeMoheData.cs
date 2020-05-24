using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IRuntimeMoheData
  {
    string InstanceID { get; }
    int Health { get; set; }
    IMohe BaseMohe { get; }
    List<IRuntimeAbility> Abilities { get; }

    void PopulateAbilities(JewelID jewel, int amount);
    bool UseableAbility();
    bool MoheDead();
  }
}
