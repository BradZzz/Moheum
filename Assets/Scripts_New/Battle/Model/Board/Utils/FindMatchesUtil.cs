using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Utils
{
  public class FindMatchesUtil
  {
    public static bool ContainsNullJewel(IRuntimeJewel[,] jewelMap)
    {
      int width = jewelMap.GetLength(0);
      int height = jewelMap.GetLength(1);

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (jewelMap[x, y] == null)
            return true;
          //GameObject UIJewel = GameObject.Find(jewelMap[x, y].JewelID);
          //if (UIJewel == null)
          //  return true;
          //Debug.Log(jewelMap[x, y].JewelID + ": " + x.ToString() + "|" + y.ToString() + " : " + jewelMap[x, y].Pos.ToString());
        }

      }
      return false;
    }

    public static List<SwapChoices> FindBestMatches(IRuntimeJewel[,] jewelMap, List<JewelID> prefJewels = null)
    {
      int width = jewelMap.GetLength(0);
      int height = jewelMap.GetLength(1);

      List<SwapChoices> options = new List<SwapChoices>();

      // Look at all the rows and remove gems that are in the buffer more than 3
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width - 1; x++)
        {
          IRuntimeJewel j1 = jewelMap[x, y];
          IRuntimeJewel j2 = jewelMap[x + 1, y];

          int matchcount = WillCauseMatchCount(jewelMap, j1, j2, prefJewels);
          if (matchcount > 0)
          {
            options.Add(new SwapChoices(j1, j2, matchcount));
          }
        }
      }

      // Look at all the columns and remove gems that are in the buffer more than 3
      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height - 1; y++)
        {
          IRuntimeJewel j1 = jewelMap[x, y];
          IRuntimeJewel j2 = jewelMap[x, y + 1];

          int matchcount = WillCauseMatchCount(jewelMap, j1, j2, prefJewels);
          if (matchcount > 0)
          {
            options.Add(new SwapChoices(j1, j2, matchcount));
          }
        }
      }
      return options;
    }

    public static bool WillCauseMatch(IRuntimeJewel[,] jewels, IRuntimeJewel swap1, IRuntimeJewel swap2)
    {
      jewels[(int)swap1.Pos.x, (int)swap1.Pos.y] = swap2;
      jewels[(int)swap2.Pos.x, (int)swap2.Pos.y] = swap1;
      bool foundMatches = FindMatchesUtil.FindMatches(jewels).Count > 0;
      jewels[(int)swap1.Pos.x, (int)swap1.Pos.y] = swap1;
      jewels[(int)swap2.Pos.x, (int)swap2.Pos.y] = swap2;
      return foundMatches;
    }

    public static int WillCauseMatchCount(IRuntimeJewel[,] jewels, IRuntimeJewel swap1, IRuntimeJewel swap2, List<JewelID> prefJewels = null)
    {
      jewels[(int)swap1.Pos.x, (int)swap1.Pos.y] = swap2;
      jewels[(int)swap2.Pos.x, (int)swap2.Pos.y] = swap1;
      int MatchCount = 0;
      List<IRuntimeJewel> foundMatches = FindMatchesUtil.FindMatches(jewels);
      if (prefJewels!= null)
      {
        List<JewelID> matchesPref = new List<JewelID>();
        foreach (IRuntimeJewel jwl in foundMatches)
        {
          if (prefJewels.Contains(jwl.Data.JewelID) && !matchesPref.Contains(jwl.Data.JewelID))
          {
            matchesPref.Add(jwl.Data.JewelID);
          }
        }
        MatchCount = foundMatches.Count + matchesPref.Count;
      } else
      {
        MatchCount = foundMatches.Count;
      }
      jewels[(int)swap1.Pos.x, (int)swap1.Pos.y] = swap1;
      jewels[(int)swap2.Pos.x, (int)swap2.Pos.y] = swap2;
      return MatchCount;
    }

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

    public static List<List<IRuntimeJewel>> FindMatchesBuffer(IRuntimeJewel[,] jewelMap)
    {
      int width = jewelMap.GetLength(0);
      int height = jewelMap.GetLength(1);

      List<IRuntimeJewel> buffer = new List<IRuntimeJewel>();
      List<List<IRuntimeJewel>> toRemoveBuff = new List<List<IRuntimeJewel>>();

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

    private static void EvaluateBuffer(List<IRuntimeJewel> buffer, List<List<IRuntimeJewel>> toRemoveBuffer, IRuntimeJewel nextJewel)
    {
      if (nextJewel == null || buffer.Count == 0 || nextJewel.Data.JewelID != buffer[0].Data.JewelID)
      {
        if (buffer.Count >= 3)
        {
          toRemoveBuffer.Add(new List<IRuntimeJewel>(buffer));
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
