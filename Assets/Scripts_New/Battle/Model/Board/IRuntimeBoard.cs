using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using UnityEngine;

//namespace Battle.Model.RuntimeBoard
//{
public interface IRuntimeBoard
{
  IRuntimeJewel[,] GetMap();
}
//}
