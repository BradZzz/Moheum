using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * The board has no selected gems
   */

  public class CleanBoardState : BaseBoardState
  {
    public CleanBoardState(BoardBasedFsm Fsm, IRuntimeBoard Board) : base(Fsm, Board)
    {
      board = Board;
    }

    private IRuntimeBoard board;

    public override void OnEnterState()
    {
      // check for dead mohe
      EvaluateMoheDeath(PlayerSeat.Left);
      EvaluateMoheDeath(PlayerSeat.Right);

      // check to see if the current player has swapped
      if (GameData.Instance.RuntimeGame.TurnLogic.CurrentPlayer.HasSwapped)
      {
        GameData.Instance.RuntimeGame.TurnLogic.CurrentPlayer.FinishTurn();
      }
    }

    private void EvaluateMoheDeath(PlayerSeat seat)
    {
      IRoster roster = GameController.Instance.GetPlayerController(seat).Player.Roster;
      if (roster.CurrentMohe().MoheDead())
      {
        GameEvents.Instance.Notify<IMoheDeath>(i => i.OnMoheDeath(roster.CurrentMohe().InstanceID));
      }
    }
  }
}
