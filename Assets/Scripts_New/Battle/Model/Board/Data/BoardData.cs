using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Data
{
  public class BoardData : IBoardData
  {
    public BoardData(Battle.Configurations.Configurations configuration)
    {
      jewelMap = new IRuntimeJewel[configuration.boardConfigs.width, configuration.boardConfigs.height];
    }

    private IRuntimeJewel[,] jewelMap;

    public IRuntimeJewel[,] GetMap()
    {
      IRuntimeJewel[,] jewels = new IRuntimeJewel[jewelMap.GetLength(0), jewelMap.GetLength(1)];
      for (int x = 0; x < jewelMap.GetLength(0); x++)
        for (int y = 0; y < jewelMap.GetLength(1); y++)
          jewels[x, y] = jewelMap[x, y];
      return jewels;
    }

    public void PrettyJewelMap()
    {
      string buff = "";
      for (int x = 0; x < jewelMap.GetLength(0); x++)
      {
        buff = "";
        for (int y = 0; y < jewelMap.GetLength(1); y++)
        {
          buff += ((int) jewelMap[x, y].Data.JewelID).ToString();
        }
        Debug.Log(buff);
      }
    }

    public void SetJewel(IRuntimeJewel jewel, Vector2 pos)
    {
      jewelMap[(int)pos.x, (int)pos.y] = jewel;
    }
  }
}
