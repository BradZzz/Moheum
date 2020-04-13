using Battle.Controller;

namespace Battle.UI.Player
{
  /// <summary>
  ///     An interface that provides reference to the main game controller.
  /// </summary>
  public interface IUiController
  {
    IGameController Controller { get; }
  }
}