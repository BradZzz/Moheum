using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.UI.Jewel.Component;
using UnityEngine;

namespace Battle.UI.Jewel.Listener
{
  public class UiJewelClickListener : MonoBehaviour, IUiJewelClickListener
  {
    private IRuntimeJewel data;

    public void Execute(IRuntimeJewel jewelData)
    {
      data = jewelData;
    }

    void OnMouseDown()
    {
      Debug.Log("Clicked Data: ");
      Debug.Log(data);
      GameEvents.Instance.Notify<ISelectJewel>(i => i.OnSelect(data));
    }
  }
}
