using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Patterns;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public class UIJewelPosition : IUIJewelPosition
  {
    private float JEWELFALLSPEED = .01f;

    public UIJewelPosition(IUiJewelComponents Parent)
    {
      Parent.UIRuntimeData.OnSetData += Execute;
      parent = Parent;
      transform = Parent.transform;
    }

    IRuntimeJewel jewel;
    Transform transform;
    IUiJewelComponents parent;

    public void Execute(IRuntimeJewel jewelData)
    {
      jewel = jewelData;
    }

    public void OnJewelPosition(IRuntimeJewel Jewel, Vector3 from, Vector3 to)
    {
      if (jewel.JewelID == Jewel.JewelID)
      {
        Debug.Log("OnJewelPosition");
        Debug.Log(jewel.JewelID);
        Debug.Log(jewel.Pos.ToString());
        Debug.Log(jewel.LastPos.ToString());
        Debug.Log(from.ToString());
        Debug.Log(to.ToString());
        Debug.Log("Transform Before: " + transform.position.ToString());
        parent.MonoBehavior.StartCoroutine(CascadeJewelFromPosition(from, to));
      }
    }

    private IEnumerator CascadeJewelFromPosition(Vector3 from, Vector3 to)
    {
      //transform.position = from;

      int count = 0;
      float diff = Math.Abs(to.y - from.y);
      while (transform.position != to && count < diff)
      {
        transform.position = Vector3.Lerp(from, to, (float)count / diff);
        // If the diff is smaller, the jewels need to fall faster
        yield return new WaitForSeconds(JEWELFALLSPEED);
        count++;
      }
      transform.position = to;
      Debug.Log("Transform After: " + transform.position.ToString());
    }
  }
}
