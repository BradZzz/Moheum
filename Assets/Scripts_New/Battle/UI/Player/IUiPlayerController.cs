using Battle.Controller;
using Battle.Controller.TurnControllers;

namespace Battle.UI.Player
{
  /// <summary>
  ///     An interface that provides reference to the main game controller.
  /// </summary>
  public interface IUiPlayerController
  {
    IPlayerTurn PlayerController { get; }
  }
}