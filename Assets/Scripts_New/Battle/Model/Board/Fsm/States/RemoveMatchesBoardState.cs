using System.Collections;
using System.Collections.Generic;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * A successful swap has been made, and the board needs to remove the matching gems then move to the CascadeBoard state
   */

  public class RemoveMatchesBoardState : BaseBoardState
  {
    public RemoveMatchesBoardState(BoardBasedFsm fsm, IBoardData boardData) : base(fsm, boardData)
    {

    }
  }
}
