using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.MoheModel.ExpTypes;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IRuntimeMoheData
  {
    string InstanceID { get; }
    int Exp { get; set; }
    int Health { get; set; }
    IMohe BaseMohe { get; }
    List<IRuntimeAbility> Abilities { get; }
    BaseExpType BaseExpType { get; }
    PlayerSeat PlayerSeat { get; }

    void PopulateAbilities(JewelID jewel, int amount);
    bool UseableAbility();
    bool MoheDead();
  }
}
