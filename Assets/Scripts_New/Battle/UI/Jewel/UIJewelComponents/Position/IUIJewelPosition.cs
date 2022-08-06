using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public interface IUIJewelPosition
  {
    void Execute(IRuntimeJewel jewelData);
    void OnJewelPosition(IRuntimeJewel Jewel, Vector3 from, Vector3 to);
  }
}
