using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard.Data;
using Battle.Model.RuntimeBoard.Utils;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * A single gem has been selected
   */

  public class SelectedBoardState : BaseBoardState
  {
    public SelectedBoardState(BoardBasedFsm Fsm, IBoardData BoardData) : base(Fsm, BoardData)
    {
      boardData = BoardData;
    }

    private IBoardData boardData;

    public override void OnEnterState()
    {
      // Look through all jewels
      IRuntimeJewel[,] jewels = boardData.GetMap();

      // Right here is where I need to look through all the jewels and see if two jewels are clicked
      List<IRuntimeJewel> jewelsClicked = new List<IRuntimeJewel>();
      for (int x = 0; x < jewels.GetLength(0); x++)
        for (int y = 0; y < jewels.GetLength(1); y++)
          if (jewels[x, y].IsSelected)
            jewelsClicked.Add(jewels[x, y]);

      if (jewelsClicked.Count <= 1)
      {
        // Clean Board state
        GameEvents.Instance.Notify<ICleanBoard>(i => i.OnBoardCleanCheck());
      }
      else if (jewelsClicked.Count == 2 && FindMatchesUtil.WillCauseMatch(jewels, jewelsClicked[0], jewelsClicked[1]))
      {
        // If the two jewels will cause a match, swap the jewels, then swap to evaluate board state
        GameEvents.Instance.Notify<ISwapBoard>(i => i.OnBoardSwapCheck());
      }
      else
      {
        // Remove all selected board state
        GameEvents.Instance.Notify<IRemoveSelectedBoard>(i => i.OnBoardRemoveSelectedCheck());
      }
    }
  }
}
