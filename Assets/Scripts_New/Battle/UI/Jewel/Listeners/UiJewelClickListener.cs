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
    //// Start is called before the first frame update
    void Awake()
    {
      Parent = GetComponent<IUiJewel>();
      Parent.SetData += Execute;
    }

    private IUiJewel Parent;
    private IRuntimeJewel data;

    public void Execute(IRuntimeJewel jewelData)
    {
      data = jewelData;
    }

    void OnMouseDown()
    {
      Debug.Log("Click");
      Debug.Log(data.Data.Name);
      GameEvents.Instance.Notify<ISelectJewel>(i => i.OnSelect(data));
    }
  }
}
