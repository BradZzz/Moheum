using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * A successful swap has been made, and the board needs to remove the matching gems then move to the CascadeBoard state
   */

  public class RemoveMatchesBoardState : BaseBoardState, IRemoveJewel
  {
    public RemoveMatchesBoardState(BoardBasedFsm Fsm, IBoardData BoardData) : base(Fsm, BoardData)
    {
      boardData = BoardData;
    }

    private IBoardData boardData;

    public void OnRemoveJewel(IRuntimeJewel jewel, Vector2 pos)
    {
      boardData.SetJewel(null, pos);
    }
  }
}
