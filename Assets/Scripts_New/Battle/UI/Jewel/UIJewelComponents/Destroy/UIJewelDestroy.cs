using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public class UIJewelDestroy : IUIJewelDestroy
  {
    public UIJewelDestroy(IUiJewelComponents Parent)
    {
      Parent.UIRuntimeData.OnSetData += ExecuteData;
      Parent.OnRemove += ExecuteDestroy;
      parent = Parent;
    }

    IRuntimeJewel jewel;
    IUiJewelComponents parent;

    public void ExecuteData(IRuntimeJewel jewelData)
    {
      jewel = jewelData;
    }

    public void ExecuteDestroy(IRuntimeJewel jewelData)
    {
      if (jewelData == jewel)
      {
        Object.Destroy(parent.gameObject);
      }
    }
  }
}
