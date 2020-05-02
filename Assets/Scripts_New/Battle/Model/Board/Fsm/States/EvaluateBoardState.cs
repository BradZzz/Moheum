using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * A click has been made, and the next board state needs to be determined
   */

  public class EvaluateBoardState : BaseBoardState
  {
    public EvaluateBoardState(BoardBasedFsm Fsm, IBoardData BoardData) : base(Fsm, BoardData)
    {
      boardData = BoardData;
    }

    private IBoardData boardData;
    private List<IRuntimeJewel> toRemoveBuff = new List<IRuntimeJewel>();

    public override void OnEnterState()
    {
      base.OnEnterState();
      // Bring in board data
      IRuntimeJewel[,] jewelMap = boardData.GetMap();
      int width = jewelMap.GetLength(0);
      int height = jewelMap.GetLength(1);

      List<IRuntimeJewel> buffer = new List<IRuntimeJewel>();
      toRemoveBuff.Clear();

      // Look at all the rows and remove gems that are in the buffer more than 3
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          EvaluateBuffer(buffer, jewelMap[x, y]);
        }
        buffer.Clear();
      }

      // Look at all the columns and remove gems that are in the buffer more than 3
      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height; y++)
        {
          EvaluateBuffer(buffer, jewelMap[x, y]);
        }
        buffer.Clear();
      }
      Debug.Log("Evaluate Board State!");
      foreach (var item in toRemoveBuff)
      {
        OnRemove(item);
      }
    }

    private List<IRuntimeJewel> EvaluateBuffer(List<IRuntimeJewel> buffer, IRuntimeJewel nextJewel)
    {
      Debug.Log(buffer);
      Debug.Log(nextJewel);
      Debug.Log(nextJewel == null);
      Debug.Log(nextJewel != null);

      if (buffer.Count == 0 || nextJewel.Data.JewelID != buffer[0].Data.JewelID)
      {
        if (buffer.Count >= 3)
        {
          Debug.Log("Buffer full");
          foreach (IRuntimeJewel buff in buffer)
          {
            AddToRemoveBuff(buff);
          }
        }
        buffer.Clear();
      }
      if (nextJewel != null)
      {
        buffer.Add(nextJewel);
      }
      return buffer;
    }

    private void AddToRemoveBuff(IRuntimeJewel jewel)
    {
      toRemoveBuff.Add(jewel);
    }

    private void OnRemove(IRuntimeJewel jewel)
    {
      Debug.Log("Removing Jewels");
      GameEvents.Instance.Notify<IRemoveJewel>(i => i.OnRemoveJewel(jewel));
    }
  }
}
