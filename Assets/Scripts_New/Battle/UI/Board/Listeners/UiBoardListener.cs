using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.UI.Board.Utils;
using Battle.UI.Utils;
using UnityEngine;

namespace Battle.UI.Board
{
  public class UiBoardListener : UiListener, ICascadeJewel, ISwapJewel
  {
    void Awake()
    {
      Debug.Log("UiBoardDrawListener Awake");
      BoardUtils = transform.parent.GetComponentInChildren<IUiPlayerBoardUtils>();
    }

    private IUiPlayerBoardUtils BoardUtils { get; set; }

    public void OnJewelFall(IRuntimeJewel jewel)
    {
      Debug.Log("ICascadeJewel");
      BoardUtils.CascadeJewelBoard(jewel);
    }

    public void OnJewelSwap(IRuntimeJewel jewel, IRuntimeJewel jewel2)
    {
      BoardUtils.SwapJewelBoard(jewel, jewel2);
    }
  }
}
