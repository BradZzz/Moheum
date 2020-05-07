using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard;
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
      // Look through all jewels
      IRuntimeJewel[,] jewels = GameBoard.GetBoardData().GetMap();

      for (int x = 0; x < jewels.GetLength(0); x++)
        for (int y = 0; y < jewels.GetLength(1); y++)
          if(jewels[x, y].JewelID == jewel.JewelID)
            jewels[x, y].DoSelect();

      // Send postclick event listener
      //GameEvents.Instance.Notify<IPostSelectJewel>(i => i.OnPostSelect(jewel));

      // Right here is where I need to look through all the jewels and see if two jewels are clicked
      GameEvents.Instance.Notify<ISwapBoard>(i => i.OnBoardSwapCheck());
    }
  }
}
