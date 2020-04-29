using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public class UIJewelSprite : IUIJewelSprite
  {
    public UIJewelSprite (IUiJewelComponents parent)
    {
      parent.SetData += Execute;
      SprRend = parent.Renderer;
    }

    private SpriteRenderer SprRend;

    public void Execute(IRuntimeJewel jewelData)
    {
      Debug.Log("Action<IJewelData>");
      Debug.Log(jewelData);
      Debug.Log(SprRend);
      if (SprRend != null && jewelData != null)
      {
        Debug.Log(jewelData);
        Debug.Log(SprRend);
        SprRend.sprite = jewelData.Data.Artwork;
      }
    }
  }
}
