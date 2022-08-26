using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public class UIJewelBoxCollider : IUIJewelBoxCollider
  {
    public UIJewelBoxCollider(IUiJewelComponents parent)
    {
      parent.UIRuntimeData.OnSetData += Execute;
      this.collider = parent.BoxCollider;
    }

    BoxCollider2D collider;

    public void Execute(IRuntimeJewel data)
    {
      /*if (data.Data.JewelID == JewelID.wrath)
      {
        collider.size = new Vector3(2.5f, 2.5f, 0);
      }*/
    }
  }
}
