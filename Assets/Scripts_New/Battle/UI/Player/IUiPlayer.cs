using Battle.Model.Player;

namespace Battle.UI.Player
{
  /// <summary>
  ///     A player interface which has as references to controllers.
  /// </summary>
  public interface IUiPlayer : IUiController, IUiPlayerController
  {
    PlayerSeat Seat { get; }
  }
}