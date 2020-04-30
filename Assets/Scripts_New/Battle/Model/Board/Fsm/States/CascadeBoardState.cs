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

    public override void OnEnterState()
    {
      base.OnEnterState();

      List<JewelData> jewels = JewelDatabase.Instance.GetFullList();
      IRuntimeJewel[,] jewelMap = boardData.GetMap();
      int width = jewelMap.GetLength(0);
      int height = jewelMap.GetLength(1);

      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height; y++)
        {
          OnCascadeJewel(new RuntimeJewel(jewels[Random.Range(0, jewels.Count)]), new Vector2(x, y));
        }
      }
    }

    /// <summary>
    ///     Dispatch start game event to the listeners.
    /// </summary>
    /// <param name="starterPlayer"></param>
    private void OnCascadeJewel(IRuntimeJewel jewel, Vector2 pos)
    {
      boardData.SetJewel(jewel, new Vector2(pos.x, pos.y));
      GameEvents.Instance.Notify<ICascadeJewel>(i => i.OnJewelFall(jewel, pos));
    }
  }
}
