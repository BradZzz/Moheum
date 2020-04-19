using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.UI.Jewel.UiJewelParameters
{
  public class UIJewelComponentUtility
  {
    public static void Format(UIJewelComponent jewelComp, UiJewelParameters param)
    {
      jewelComp.MonoBehavior.transform.localScale *= param.JewelScale;
    }
  }
}
