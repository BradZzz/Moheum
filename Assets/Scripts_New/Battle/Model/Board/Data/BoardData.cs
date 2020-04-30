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
      return jewelMap;
    }

    public void SetJewel(IRuntimeJewel jewel, Vector2 pos)
    {
      jewelMap[(int)pos.x, (int)pos.y] = jewel;
    }
  }
}
