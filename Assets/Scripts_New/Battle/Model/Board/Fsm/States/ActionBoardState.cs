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
   * Called after an action is used
   */
  public class ActionBoardState : BaseBoardState
  {
    public ActionBoardState(BoardBasedFsm Fsm, IRuntimeBoard Board) : base(Fsm, Board)
    {
      board = Board;
    }

    private IRuntimeBoard board;

    public override void OnEnterState()
    {
      // Reset the used ability
      board.OnCleanAbility?.Invoke();

      // Update the UI
      GameEvents.Instance.Notify<IPlayerUpdateRuntime>(i => i.OnPlayerUpdateRuntime());

      // Unselect all the actions in UI
      GameEvents.Instance.Notify<IResetAtkActionButtons>(i => i.OnResetAtkActionButton());

      // An action has been performed, clean everything up
      GameEvents.Instance.Notify<ICascadeBoard>(i => i.OnBoardCascadeCheck());
    }
  }
}
