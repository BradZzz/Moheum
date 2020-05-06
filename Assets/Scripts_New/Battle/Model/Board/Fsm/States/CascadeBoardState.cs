using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * The board needs to be filled up with missing gems
   */

  public class CascadeBoardState : BaseBoardState
  {
    public CascadeBoardState(BoardBasedFsm Fsm, IBoardData BoardData) : base(Fsm, BoardData)
    {
      boardData = BoardData;
    }

    private IBoardData boardData;

    private int Count { get; set; }

    public override void OnEnterState()
    {
      base.OnEnterState();

      Debug.Log("CascadeBoardState");
      List<JewelData> jewels = JewelDatabase.Instance.GetFullList();
      IRuntimeJewel[,] jewelMap = boardData.GetMap();
      int width = jewelMap.GetLength(0);
      int height = jewelMap.GetLength(1);

      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height; y++)
        {
          Vector2 pos = new Vector2(x, y);
          IRuntimeJewel jewel = FindNextJewel(jewelMap, pos);
          if (jewel == null)
          {
            jewel = new RuntimeJewel(jewels[Random.Range(0, jewels.Count)], pos, "Jewel_" + Count++);
          } else
          {
            jewel.RotatePos(pos);
            jewelMap[(int)jewel.LastPos.x, (int)jewel.LastPos.y] = null;
          }
          // Place Jewel On Board
          SetJewelData(jewel, pos);
          // Update UI
          OnCascadeJewel(jewel);
          // Update temp buffer
          jewelMap[x, y] = jewel;
        }
      }
    }

    private IRuntimeJewel FindNextJewel(IRuntimeJewel[,] jewelMap, Vector2 currentPos)
    {
      int height = jewelMap.GetLength(1);
      int row = (int) currentPos.y;
      while (row < height - 1 && jewelMap[(int)currentPos.x, row] == null)
      {
        row++;
      }
      return jewelMap[(int)currentPos.x, row];
    }

    // Update
    private void SetJewelData(IRuntimeJewel jewel, Vector2 pos)
    {
      boardData.SetJewel(jewel, pos);
    }
    // Notify
    private void OnCascadeJewel(IRuntimeJewel jewel)
    {
      GameEvents.Instance.Notify<ICascadeJewel>(i => i.OnJewelFall(jewel));
    }
  }
}
