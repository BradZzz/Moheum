using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public interface IUIJewelBoxCollider
  {
    void Execute(IRuntimeJewel jewelData);
  }
}
