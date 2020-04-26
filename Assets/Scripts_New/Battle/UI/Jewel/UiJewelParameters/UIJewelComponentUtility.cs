using System.Collections;
using System.Collections.Generic;
using Battle.UI.Jewel.Component;
using UnityEngine;

namespace Battle.UI.Jewel.UiJewelParameters
{
  public class UIJewelComponentUtility
  {
    public static void Format(IUiJewelComponents jewelComp, UiJewelParameters param)
    {
      jewelComp.MonoBehavior.transform.localScale *= param.JewelScale;
    }
  }
}
