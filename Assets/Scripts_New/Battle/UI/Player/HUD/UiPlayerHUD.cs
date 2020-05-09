using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.Controller.TurnControllers;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiPlayerHUD: MonoBehaviour, IUiPlayerHUD
  {
    [SerializeField]
    private PlayerSeat seat;

    public PlayerSeat Seat => seat;
    public IPlayerTurn PlayerController => GameController.Instance.GetPlayerController(Seat);
    public bool IsPlayerTurn => PlayerController.IsMyTurn;
  }
}
