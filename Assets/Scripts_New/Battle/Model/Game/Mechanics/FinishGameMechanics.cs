using System;
using Battle.GameEvent;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Model.Game.Mechanics
{
  /// <summary>
  ///     Finish Game Step Implementation.
  /// </summary>
  public class FinishGameMechanics : BaseGameMechanics
  {
    public FinishGameMechanics(IPrimitiveGame game) : base(game)
    {
    }

    public void Execute()
    {
      if (!Game.IsGameStarted)
        return;
      if (Game.IsGameFinished)
        return;

      Game.IsGameFinished = true;

      OnGameFinished(CheckWinCondition());
    }

    public IPlayer CheckWinCondition()
    {
      var playerLeft = Game.TurnLogic.GetPlayer(PlayerSeat.Left);
      var playerRight = Game.TurnLogic.GetPlayer(PlayerSeat.Right);

      bool leftHasMohe = !playerLeft.Roster.AllVanquished;
      bool rightHasMohe = !playerRight.Roster.AllVanquished;

      return rightHasMohe ? playerRight : playerLeft;
    }


    /// <summary>
    ///     Dispatch end game to the listeners.
    /// </summary>
    /// <param name="winner"></param>
    private void OnGameFinished(IPlayer winner)
    {
      // NotifyUI / Show end of game button
      GameEvents.Instance.Notify<IFinishGame>(i => i.OnFinishGame(winner));

      // Save Game
      //var gameState = BaseSaver.GetGameData();
      //var playerLeft = Game.TurnLogic.GetPlayer(PlayerSeat.Left);
      //gameState.player.hp = playerLeft.Team.Captain.Attributes.Health;
      //BaseSaver.PutGame(gameState);
    }
  }
}