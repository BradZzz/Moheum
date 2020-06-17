using Battle.GameEvent;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Model.Game.Mechanics
{
  /// <summary>
  ///     Start Current player Turn Implementation.
  /// </summary>
  public class StartPlayerTurnMechanics : BaseGameMechanics
  {
    public StartPlayerTurnMechanics(IPrimitiveGame game) : base(game)
    {
    }

    /// <summary>
    ///     Start current player turn logic.
    /// </summary>
    public void Execute()
    {
      if (Game.IsTurnInProgress)
        return;
      if (!Game.IsGameStarted)
        return;
      if (Game.IsGameFinished)
        return;


      //Game.IsTurnInProgress = true;
      //Game.TurnLogic.UpdateCurrentPlayer();
      //Game.TurnLogic.CurrentPlayer.StartTurn();

      // Confirm board init
      //InitBoard();
      // Send out ui notifications
      Game.IsTurnInProgress = true;
      Game.TurnLogic.UpdateCurrentPlayer();
      Game.TurnLogic.CurrentPlayer.StartTurn();

      OnStartedCurrentPlayerTurn(Game.TurnLogic.CurrentPlayer);
    }

    //private void InitBoard()
    //{
    //  GameEvents.Instance.Notify<ICascadeBoard>(i => i.OnBoardCascadeCheck());
    //}

    /// <summary>
    ///     Dispatch start current player turn to the listeners.
    /// </summary>
    /// <param name="currentPlayer"></param>
    private void OnStartedCurrentPlayerTurn(IPlayer currentPlayer)
    {
      //Debug.Log("OnStartedCurrentPlayerTurn");
      //Game.IsTurnInProgress = true;
      //Game.TurnLogic.UpdateCurrentPlayer();
      //Game.TurnLogic.CurrentPlayer.StartTurn();
      GameEvents.Instance.Notify<IStartPlayerTurn>(i => i.OnStartPlayerTurn(currentPlayer));
    }
  }
}