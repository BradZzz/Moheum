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
   * A click has been made, and the next board state needs to be determined
   */

  public class EvaluateBoardState : BaseBoardState
  {
    public EvaluateBoardState(BoardBasedFsm Fsm, IRuntimeBoard Board) : base(Fsm, Board)
    {
      board = Board;
    }

    private IRuntimeBoard board;
    //private List<IRuntimeJewel> toRemoveBuff = new List<IRuntimeJewel>();

    public override void OnEnterState()
    {

      //Logger.Log<EvaluateBoardState>("OnEnterState");
      base.OnEnterState();

      // Bring in board data
      IRuntimeJewel[,] jewelMap = board.GetBoardData().GetMap();
      List<IRuntimeJewel> toRemoveBuff = FindMatchesUtil.FindMatches(board.GetBoardData().GetMap());
      foreach (var jewel in toRemoveBuff)
      {
        OnRemove(jewel);
      }
      if (toRemoveBuff.Count > 0 || FindMatchesUtil.ContainsNullJewel(board.GetBoardData().GetMap()))
      {
        OnCascadeState();
      } else
      {
        OnCleanBoardState();
      }
    }

    private void OnCascadeState()
    {
      GameEvents.Instance.Notify<ICascadeBoard>(i => i.OnBoardCascadeCheck());
    }

    private void OnCleanBoardState()
    {
      GameEvents.Instance.Notify<ICleanBoard>(i => i.OnBoardCleanCheck());
    }

    private void OnRemove(IRuntimeJewel jewel)
    {
      // The jewel has to be removed from the data here so that the cascade works properly
      board.GetBoardData().SetJewel(null, jewel.Pos);
      // Notify UI that the jewel needs to be removed
      GameEvents.Instance.Notify<IRemoveJewel>(i => i.OnRemoveJewel(jewel));
    }
  }
}
