using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.Player;
using Patterns;
using UnityEngine;

namespace Battle.UI.RuntimeBoard.Mechanics
{
  public class StartGameBoardMechanics : BaseBoardMechanics, IListener, IStartGame
  {
    public StartGameBoardMechanics(IRuntimeBoard board) : base(board)
    {
      GameEvents.Instance.AddListener(this);
    }

    public void OnStartGame(IPlayer starter)
    {
      Execute();
    }

    /// <summary>
    ///     Execution of start game
    /// </summary>
    public void Execute()
    {
      //if (Game.IsGameStarted) return;

      //Game.IsGameStarted = true;

      ////calculus of the starting player
      //Game.TurnLogic.DecideStarterPlayer();

      //OnGameStarted(Game.TurnLogic.StarterPlayer);

      // Draw the initial jewels the board needs here!
      // The position relates to the offset position from center
      OnDrawJewel(null, new Vector2(-1, -1));
      OnDrawJewel(null, new Vector2(-1, 0));
      OnDrawJewel(null, new Vector2(-1, 1));
    }

    /// <summary>
    ///     Dispatch start game event to the listeners.
    /// </summary>
    /// <param name="starterPlayer"></param>
    private void OnDrawJewel(IRuntimeJewel jewel, Vector2 pos)
    {
      GameEvents.Instance.Notify<IBoardDrawJewel>(i => i.OnDraw(jewel, pos));
    }
  }
}
