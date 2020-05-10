using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * The board has no selected gems
   */

  public class CleanBoardState : BaseBoardState
  {
    public CleanBoardState(BoardBasedFsm Fsm, IBoardData BoardData) : base(Fsm, BoardData)
    {
      boardData = BoardData;
    }

    private IBoardData boardData;

    public override void OnEnterState()
    {
      // check to see if the current player has swapped
      if (GameData.Instance.RuntimeGame.TurnLogic.CurrentPlayer.HasSwapped)
      {
        GameData.Instance.RuntimeGame.TurnLogic.CurrentPlayer.FinishTurn();
      }
    }
  }
}
