using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public class UIJewelSprite : IUIJewelSprite
  {
    public UIJewelSprite (IUiJewelComponents parent, SpriteRenderer sprRend)
    {
      parent.SetData += Execute;
      SprRend = sprRend;
    }

    private SpriteRenderer SprRend;

    public void Execute(IJewelData jewelData)
    {
      Debug.Log("Action<IJewelData>");
      Debug.Log(jewelData);
      Debug.Log(SprRend);
      if (SprRend != null && jewelData != null)
      {
        Debug.Log(jewelData);
        Debug.Log(SprRend);
        SprRend.sprite = jewelData.Artwork;
      }
    }
  }
}
