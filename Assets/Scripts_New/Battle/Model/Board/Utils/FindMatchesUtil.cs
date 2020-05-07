using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Utils
{
  public class FindMatchesUtil
  {
    public static List<IRuntimeJewel> FindMatches(IRuntimeJewel[,] jewelMap)
    {
      int width = jewelMap.GetLength(0);
      int height = jewelMap.GetLength(1);

      List<IRuntimeJewel> buffer = new List<IRuntimeJewel>();
      List<IRuntimeJewel> toRemoveBuff = new List<IRuntimeJewel>();

      // Look at all the rows and remove gems that are in the buffer more than 3
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          EvaluateBuffer(buffer, toRemoveBuff, jewelMap[x, y]);
        }
        EvaluateBuffer(buffer, toRemoveBuff, null);
        buffer.Clear();
      }

      // Look at all the columns and remove gems that are in the buffer more than 3
      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height; y++)
        {
          EvaluateBuffer(buffer, toRemoveBuff, jewelMap[x, y]);
        }
        EvaluateBuffer(buffer, toRemoveBuff, null);
        buffer.Clear();
      }
      return toRemoveBuff;
    }

    private static void EvaluateBuffer(List<IRuntimeJewel> buffer, List<IRuntimeJewel> toRemoveBuffer, IRuntimeJewel nextJewel)
    {
      if (nextJewel == null || buffer.Count == 0 || nextJewel.Data.JewelID != buffer[0].Data.JewelID)
      {
        if (buffer.Count >= 3)
        {
          foreach (IRuntimeJewel buff in buffer)
          {
            toRemoveBuffer.Add(buff);
          }
        }
        buffer.Clear();
      }
      if (nextJewel != null)
      {
        buffer.Add(nextJewel);
      }
    }
  }
}
