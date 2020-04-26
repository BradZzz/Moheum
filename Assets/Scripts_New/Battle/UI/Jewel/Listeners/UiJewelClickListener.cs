using System.Collections;
using System.Collections.Generic;
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
      Parent = GetComponent<IUiJewelComponents>();
      Parent.SetData += Execute;
    }

    private IUiJewelComponents Parent;
    private IJewelData data;

    public void Execute(IJewelData jewelData)
    {
      data = jewelData;
    }

    void OnMouseDown()
    {
      Debug.Log("Click");
      Debug.Log(data.Name);
    }
  }
}
