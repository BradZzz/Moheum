using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiFleeActionButton : UiBaseActionButton
  {
    public override bool Populate(PlayerSeat seat, int pos)
    {
      IPlayer contPlayer = GameData.Instance.RuntimeGame.Players.Find(player => player.Seat == seat);

      return true;
    }
  }
}
