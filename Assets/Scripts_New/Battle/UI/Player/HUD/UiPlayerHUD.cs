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
    private void Awake()
    {
      UiActionButtons = GetComponentInChildren<UiActionButtons>();
      UiPlayerRoster = GetComponentInChildren<UiPlayerRoster>();
      UiNavButtons = GetComponentInChildren<UiNavButtons>();
      UiInfoPanel = GetComponentInChildren<UiInfoPanel>();
    }

    [SerializeField]
    private PlayerSeat seat;
    private UiActionButtons UiActionButtons;
    private UiPlayerRoster UiPlayerRoster;
    private UiNavButtons UiNavButtons;
    private UiInfoPanel UiInfoPanel;

    public PlayerSeat Seat => seat;
    public IPlayerTurn PlayerController => GameController.Instance.GetPlayerController(Seat);
    public bool IsPlayerTurn => PlayerController.IsMyTurn;

    public UiActionButtons UIActionButtons => UiActionButtons;
    public UiPlayerRoster UIPlayerRoster => UiPlayerRoster;
    public UiNavButtons UINavButtons => UiNavButtons;
    public UiInfoPanel UIInfoPanel => UiInfoPanel;
  }
}
