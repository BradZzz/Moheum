using System.Collections;
using System.Collections.Generic;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * The board has no selected gems
   */

  public class CleanBoardState : BaseBoardState
  {
    public CleanBoardState(BoardBasedFsm fsm, IBoardData boardData) : base(fsm, boardData)
    {

    }
  }
}
