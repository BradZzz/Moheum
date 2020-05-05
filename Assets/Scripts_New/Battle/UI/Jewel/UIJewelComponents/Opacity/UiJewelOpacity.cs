using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public class UiJewelOpacity : IUiJewelOpacity
  {
    // Link back to the jewel info and set darker when clicked
    public UiJewelOpacity(IUiJewel JewelComponent)
    {
      JewelComponent.UIRuntimeData.OnSetData += Execute;
      JewelComponent.OnPostSelect += OnPostSelect;
      renderer = JewelComponent.Renderer;
    }

    private IRuntimeJewel jewel;
    private SpriteRenderer renderer;

    public IRuntimeJewel Jewel => jewel;
    public SpriteRenderer Renderer => renderer;

    public void Execute(IRuntimeJewel jewelData)
    {
      jewel = jewelData;
    }

    public void OnPostSelect(IRuntimeJewel jewel)
    {
      if (jewel == Jewel)
      {
        Debug.Log("OnSelect UiJewelOpacity");
        if (jewel.IsSelected) {
          renderer.color = new Color(.8f,.8f,.8f,1);
        } else {
          renderer.color = new Color(1, 1, 1, 1);
        }
      }
    }
  }
}
