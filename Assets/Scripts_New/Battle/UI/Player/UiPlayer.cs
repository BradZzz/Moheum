using Battle.Controller;
using Battle.Controller.TurnControllers;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.UI.Player
{
  /// <summary>
  ///     Player UI Component. It resolves the dependencies accessing the game controller via Singleton.
  /// </summary>
  public class UiPlayer : MonoBehaviour, IUiPlayer
  {
    public virtual PlayerSeat Seat => PlayerSeat.Right;
    public IGameController Controller => GameController.Instance;
    public IPlayerTurn PlayerController => GameController.Instance.GetPlayerController(Seat);
  }
}