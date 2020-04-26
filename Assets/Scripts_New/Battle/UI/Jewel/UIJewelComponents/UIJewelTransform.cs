using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using Battle.UI.Jewel.UiJewelComponent;
using UnityEngine;

namespace Battle.UI.Jewel.UiJewelComponent
{
  public class UIJewelTransform : IUiJewelTransform
  {
    public UIJewelTransform(UIJewelComponent parent, Transform transform)
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
