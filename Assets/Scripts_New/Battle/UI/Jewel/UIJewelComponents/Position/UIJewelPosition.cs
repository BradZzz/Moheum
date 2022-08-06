using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
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
      /*
       * Here is where we need to query the board map holder and make sure that
       * this gem is the gem that is supposed to be in the position
       */
      if (jewel.JewelID == Jewel.JewelID)
      {
        IRuntimeJewel[,] board = GameData.Instance.RuntimeGame.GameBoard.GetBoardData().GetMap();
        IRuntimeJewel boardJewel = board[(int)jewel.Pos.x, (int)jewel.Pos.y];
        if (boardJewel.JewelID == jewel.JewelID)
        {
          // The board and the cascader are in sync, move jewel to correct position
          parent.MonoBehavior.StartCoroutine(CascadeJewelFromPosition(from, to));
        } else
        {
          // The board and the cascader are out of sync, and this jewel should have been deleted
          parent.OnRemove(jewel);
        }
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
      // Need to recenter here if not perfect
      //Debug.Log("jewel pos: " + jewel.Pos.ToString());
      //Debug.Log("jewel id: " + jewel.JewelID.ToString());
      //Debug.Log("To: " + to.ToString());
    }
  }
}
