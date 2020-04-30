using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Data
{
  public interface IBoardData
  {
    IRuntimeJewel[,] GetMap();
    void SetJewel(IRuntimeJewel jewel, Vector2 pos);
  }
}