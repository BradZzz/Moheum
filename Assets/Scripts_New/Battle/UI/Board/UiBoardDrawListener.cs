using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.UI.Board.Utils;
using Battle.UI.Utils;
using UnityEngine;

namespace Battle.UI.Board
{
  public class UiBoardDrawListener : UiListener, ICascadeJewel
  {
    void Awake()
    {
      Debug.Log("UiBoardDrawListener Awake");
      BoardUtils = transform.parent.GetComponentInChildren<IUiPlayerBoardUtils>();
    }

    private IUiPlayerBoardUtils BoardUtils { get; set; }

    //public void OnDraw(IRuntimeJewel jewel, Vector2 pos)
    //{
    //  Debug.Log("UiBoardDrawListener OnDraw");
    //  //BoardUtils.Draw(jewel, pos);
    //}

    public void OnJewelFall(IRuntimeJewel jewel)
    {
      Debug.Log("UiBoardDrawListener OnJewelFall");
      BoardUtils.Draw(jewel);
    }
  }
}
