using System.Collections;
using System.Collections.Generic;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * A single gem has been selected
   */

  public class SelectedBoardState : BaseBoardState
  {
    public SelectedBoardState(BoardBasedFsm fsm, IBoardData boardData) : base(fsm, boardData)
    {

    }
  }
}
