using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard;
using Patterns;
using UnityEngine;

namespace Battle.UI.RuntimeBoard.Mechanics
{
  public class RemoveJewelBoardMechanics : BaseBoardMechanics, IListener, IRemoveJewel
  {
    public RemoveJewelBoardMechanics(IRuntimeBoard Board) : base(Board)
    {
      GameEvents.Instance.AddListener(this);
      board = Board;
    }

    IRuntimeBoard board;

    public void OnRemoveJewel(IRuntimeJewel jewel)
    {
      board.GetBoardData().SetJewel(null, jewel.Pos);
    }
  }
}
