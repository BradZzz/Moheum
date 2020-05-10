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
   * Two gems have been selected. Evaluate the swap and move to RemoveBoardState or CleanBoardState depending on matches
   */

  public class SwapBoardState : BaseBoardState
  {
    public SwapBoardState(BoardBasedFsm Fsm, IBoardData BoardData) : base(Fsm, BoardData)
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

      if (jewelsClicked.Count == 2)
      {
        Vector2 pos1 = jewelsClicked[0].Pos;
        Vector2 pos2 = jewelsClicked[1].Pos;

        jewelsClicked[0].RotatePos(pos2);
        jewelsClicked[1].RotatePos(pos1);

        SetJewelData(jewelsClicked[0], jewelsClicked[0].Pos);
        SetJewelData(jewelsClicked[1], jewelsClicked[1].Pos);

        MarkPlayerSwap();
        OnSwapJewel(jewelsClicked[0], jewelsClicked[1]);
      }

      //OnSwapFinished();
    }

    private void MarkPlayerSwap()
    {
      GameData.Instance.RuntimeGame.TurnLogic.CurrentPlayer.SwapTurn();
    }

    private void SetJewelData(IRuntimeJewel jewel, Vector2 pos)
    {
      boardData.SetJewel(jewel, pos);
    }

    private void OnSwapJewel(IRuntimeJewel jewel, IRuntimeJewel jewel2)
    {
      GameEvents.Instance.Notify<ISwapJewel>(i => i.OnJewelSwap(jewel, jewel2));
    }

    private void OnSwapFinished()
    {
      //GameEvents.Instance.Notify<IRemoveSelectedBoard>(i => i.OnBoardRemoveSelectedCheck());
    }
  }
}
