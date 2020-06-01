using System.Collections;
using System.Collections.Generic;
using Battle.Controller.TurnControllers;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.UI.Player
{
  public interface IUiPlayerHUD
  {
    PlayerSeat Seat { get; }
    IPlayerTurn PlayerController { get; }
    bool IsPlayerTurn { get; }

    UiActionButtons UIActionButtons { get; }
    UiPlayerRoster UIPlayerRoster { get; }
    UiNavButtons UINavButtons { get; }
    UiInfoPanel UIInfoPanel { get; }
  }
}