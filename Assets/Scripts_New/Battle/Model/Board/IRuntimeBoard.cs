using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.UI.RuntimeBoard.Mechanics;
using UnityEngine;

namespace Battle.Model.RuntimeBoard
{
  public interface IRuntimeBoard
  {
    IRuntimeJewel[,] GetMap();
    void SetJewel(IRuntimeJewel jewel, int x, int y);
    List<BaseBoardMechanics> GetMechanics();
  }
}
