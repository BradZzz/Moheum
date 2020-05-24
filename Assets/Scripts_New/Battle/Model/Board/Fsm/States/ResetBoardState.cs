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

  public class ResetBoardState : BaseBoardState
  {
    public ResetBoardState(BoardBasedFsm Fsm, IRuntimeBoard Board) : base(Fsm, Board)
    {
      board = Board;
    }

    private IRuntimeBoard board;

    public override void OnEnterState()
    {
      // Look through all jewels
      IRuntimeJewel[,] jewelMap = board.GetBoardData().GetMap();
      List<SwapChoices> matchesBuff = FindMatchesUtil.FindBestMatches(jewelMap);
      if (matchesBuff.Count == 0)
      {
        int width = jewelMap.GetLength(0);
        int height = jewelMap.GetLength(1);
        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            OnRemove(jewelMap[x, y]);
          }
        }
      }
      Notify();
    }

    private void OnRemove(IRuntimeJewel jewel)
    {
      // The jewel has to be removed from the data here so that the cascade works properly
      board.GetBoardData().SetJewel(null, jewel.Pos);
      // Notify UI that the jewel needs to be removed
      GameEvents.Instance.Notify<IRemoveJewel>(i => i.OnRemoveJewel(jewel));
    }

    private void Notify()
    {
      // Notify UI that the jewel needs to be removed
      GameEvents.Instance.Notify<ICascadeBoard>(i => i.OnBoardCascadeCheck());
    }
  }
}
