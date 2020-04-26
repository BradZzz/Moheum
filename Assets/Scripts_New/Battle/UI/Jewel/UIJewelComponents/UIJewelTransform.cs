using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using Battle.UI.Jewel.UiJewelComponent;
using UnityEngine;

public class UIJewelTransform : IUiJewelTransform
{
  public UIJewelTransform(Transform transform, IJewelData data)
  {
    this.data = data;
    this.transform = transform;
    Execute();
  }

  Transform transform;
  IJewelData data;

  public Action<IJewelData> Execute()
  {
    if (data.JewelID == JewelID.wrath)
    {
      transform.localScale = new Vector3(0.3f,0.3f,0.3f);
    }
    return null;
  }
}
