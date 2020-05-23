using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard.Data;
using Battle.Model.RuntimeBoard.Utils;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * This is to mark the spot between when a player clicks the action button and when the player clicks a jewel
   */

  public class PreActionBoardState : BaseBoardState
  {
    public PreActionBoardState(BoardBasedFsm Fsm, IRuntimeBoard Board) : base(Fsm, Board)
    {
      board = Board;
    }

    private IRuntimeBoard board;

    public override void OnEnterState()
    {
      // This is to mark the spot between when a player clicks the action button and when the player clicks a jewel
    }
  }
}
