using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public class UIJewelTransform : IUiJewelTransform
  {
    public UIJewelTransform(IUiJewelComponents parent)
    {
      parent.SetData += Execute;
      this.transform = parent.transform;
    }

    Transform transform;

    public void Execute(IRuntimeJewel data)
    {
      if (data.Data.JewelID == JewelID.wrath)
      {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
      }
    }
  }
}
