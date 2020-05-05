using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public interface IUIJewelPosition : IPositionJewel
  {
    void Execute(IRuntimeJewel jewelData);
  }
}
