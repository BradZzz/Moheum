using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Model.Game.Mechanics
{
  /// <inheritdoc />
  /// <summary>
  ///     Pre Start Game Step Implementation.
  /// </summary>
  public class PreStartGameMechanics : BaseGameMechanics
  {
    public PreStartGameMechanics(IPrimitiveGame game) : base(game)
    {
    }

    /// <summary>
    ///     Execution of start game
    /// </summary>
    public void Execute()
    {
      if (Game.IsGameStarted) return;

      //players draw starting cards
      LoadStartingJewels();

      OnGamePreStarted(Game.TurnLogic.Players);
    }


    private void LoadStartingJewels()
    {
      // Load starting jewels here

      //if (!Game.Configurations.WithStartingHands)
      //  return;

      //foreach (var player in Game.Players)
      //  player.DrawStartingHand();
    }

    /// <summary>
    ///     Dispatch pre start game event to the listeners
    /// </summary>
    /// <param name="players"></param>
    private void OnGamePreStarted(List<IPlayer> players)
    {
      GameEvents.Instance.Notify<IPreGameStart>(i => i.OnPreGameStart(players));
    }
  }
}