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
      parent.UIRuntimeData.OnSetData += Execute;
      SprRend = parent.Renderer;
    }

    private SpriteRenderer SprRend;

    public void Execute(IRuntimeJewel jewelData)
    {
      if (SprRend != null && jewelData != null)
      {
        SprRend.sprite = jewelData.Data.Artwork;
      }
    }
  }
}
