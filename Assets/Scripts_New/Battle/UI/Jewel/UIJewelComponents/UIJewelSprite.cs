using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.UiJewelComponent
{
  public class UIJewelSprite : IUIJewelSprite
  {
    public UIJewelSprite (SpriteRenderer sprRend, IJewelData jewelData)
    {
      SprRend = sprRend;
      JewelData = jewelData;
      Execute();
    }

    private SpriteRenderer SprRend;
    private IJewelData JewelData;

    public Action<IJewelData> Execute()
    {
      Debug.Log("Action<IJewelData>");
      Debug.Log(JewelData);
      Debug.Log(SprRend);
      if (SprRend != null && JewelData != null)
      {
        Debug.Log(JewelData);
        Debug.Log(SprRend);
        SprRend.sprite = JewelData.Artwork;
      }
      return null;
    }
  }
}
