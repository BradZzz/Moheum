using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel.ExpTypes
{
  public interface IBaseExpType
  {
    int CalculateLevel(int exp);
    int IntFromLastLevel(int exp);
    int IntToNextLevel(int exp);
  }
}
