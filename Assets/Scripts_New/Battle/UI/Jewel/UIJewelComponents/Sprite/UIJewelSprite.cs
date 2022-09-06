using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public class UIJewelSprite : IUIJewelSprite
  {
    public UIJewelSprite (IUiJewelComponents Parent)
    {
      Parent.UIRuntimeData.OnAfterSetData += Execute;
      SprRend = Parent.Renderer;
      parent = Parent;
    }

    private SpriteRenderer SprRend;
    private IUiJewelComponents parent;

    public void Execute(IRuntimeJewel jewelData)
    {
      if (SprRend != null && jewelData != null)
      {
        SprRend.sprite = jewelData.Data.Artwork;
      }
    }
  }
}
