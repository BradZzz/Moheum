using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using TMPro;
using UnityEngine;

namespace Battle.UI.Player
{
  public interface IUiAttackCost : IPlayerUpdateRuntime
  {
    MonoBehaviour MBehaviour { get; }

    JewelID ID { get; }
    TextMeshProUGUI TXT { get; }

    PlayerSeat Seat { get; }
    IRuntimeAbility Ability { get; }
    int ComponentIdx { get; }

    bool Populate(PlayerSeat seat, IRuntimeAbility ability, int componentIdx);
  }
}
