using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public class UIJewelTransform : IUiJewelTransform
  {
    public UIJewelTransform(IUiJewelComponents parent, Transform transform)
    {
      parent.SetData += Execute;
      this.transform = transform;
    }

    Transform transform;

    public void Execute(IJewelData data)
    {
      if (data.JewelID == JewelID.wrath)
      {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
      }
    }
  }
}
