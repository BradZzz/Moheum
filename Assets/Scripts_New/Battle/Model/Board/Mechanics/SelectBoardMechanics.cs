using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard;
using Battle.Model.RuntimeBoard.Controller;
using Patterns;
using UnityEngine;

namespace Battle.UI.RuntimeBoard.Mechanics
{
  public class SelectBoardMechanics : BaseBoardMechanics, IListener, ISelectJewel
  {
    public SelectBoardMechanics(IRuntimeBoard board) : base(board)
    {
      GameEvents.Instance.AddListener(this);
      GameBoard = board;
    }

    IRuntimeBoard GameBoard;

    public void OnSelect(IRuntimeJewel jewel)
    {
      // Check to see if an action is about to be run
      if (BoardController.Instance.IsWaitingForAction())
      {
        GameEvents.Instance.Notify<IInvokeActionBoard>(i => i.OnInvokeBoardActionCheck(jewel));
      }
      else
      {
        // Select jewel
        jewel.DoSelect();

        // Right here is where I need to look through all the jewels and see if two jewels are clicked
        GameEvents.Instance.Notify<ISelectedBoard>(i => i.OnBoardSelectedCheck());
      }
    }
  }
}
