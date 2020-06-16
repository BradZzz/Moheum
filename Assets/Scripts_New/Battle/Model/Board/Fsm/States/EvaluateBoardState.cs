using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
      if (FindMatchesUtil.FindBestMatches(jewelMap).Count == 0)
      {
        OnResetState();
        return;
      }

      List<List<IRuntimeJewel>> toRemoveBuffs = FindMatchesUtil.FindMatchesBuffer(jewelMap);
      foreach (var toRemoveBuff in toRemoveBuffs)
      {
        if (toRemoveBuff.Count > 3)
        {
          OnBonus(toRemoveBuff.Select((jewel) => { return jewel.Data.JewelID; }).ToList());
        }
        foreach (var jewel in toRemoveBuff)
        {
          OnRemove(jewel);
        }
      }
      if (toRemoveBuffs.Count > 0 || FindMatchesUtil.ContainsNullJewel(board.GetBoardData().GetMap()))
      {
        OnCascadeState();
      } else
      {
        OnCleanBoardState();
      }
    }

    private void OnResetState()
    {
      GameEvents.Instance.Notify<IResetBoard>(i => i.OnBoardResetCheck());
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
      GameEvents.Instance.Notify<IRemoveJewel>(i => i.OnRemoveJewel(jewel));
    }

    private void OnBonus(List<JewelID> jewels)
    {
      GameEvents.Instance.Notify<IGainJewelBonus>(i => i.OnGainBonusJewel(jewels));
    }
  }
}
