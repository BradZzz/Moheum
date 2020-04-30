using System.Collections;
using System.Collections.Generic;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * A click has been made, and the next board state needs to be determined
   */

  public class EvaluateBoardState : BaseBoardState
  {
    public EvaluateBoardState(BoardBasedFsm fsm, IBoardData boardData) : base(fsm, boardData)
    {

    }
  }
}
