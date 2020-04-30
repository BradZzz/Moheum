using System.Collections;
using System.Collections.Generic;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * Two gems have been selected. Evaluate the swap and move to RemoveBoardState or CleanBoardState depending on matches
   */

  public class SwapBoardState : BaseBoardState
  {
    public SwapBoardState(BoardBasedFsm fsm, IBoardData boardData) : base(fsm, boardData)
    {

    }
  }
}
