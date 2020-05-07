using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard;
using Patterns;
using UnityEngine;

namespace Battle.UI.RuntimeBoard.Mechanics
{
  public class UnselectBoardMechanics : BaseBoardMechanics, IListener, IUnselectAll
  {
    public UnselectBoardMechanics(IRuntimeBoard board) : base(board)
    {
      GameEvents.Instance.AddListener(this);
      GameBoard = board;
    }

    IRuntimeBoard GameBoard;

    public void OnUnselectAll()
    {
      IRuntimeJewel[,] jewels = GameBoard.GetBoardData().GetMap();
      for (int x = 0; x < jewels.GetLength(0); x++)
        for (int y = 0; y < jewels.GetLength(1); y++)
          jewels[x, y].DoUnselect();
    }
  }
}
