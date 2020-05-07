using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.UI.Board.Utils;
using Battle.UI.Utils;
using UnityEngine;

namespace Battle.UI.Board
{
  public class UiBoardDrawListener : UiListener, ICascadeJewel, IRepositionJewel
  {
    void Awake()
    {
      Debug.Log("UiBoardDrawListener Awake");
      BoardUtils = transform.parent.GetComponentInChildren<IUiPlayerBoardUtils>();
    }

    private IUiPlayerBoardUtils BoardUtils { get; set; }

    public void OnJewelFall(IRuntimeJewel jewel)
    {
      BoardUtils.CascadeJewelBoard(jewel);
    }

    public void OnJewelReposition(IRuntimeJewel jewel)
    {
      BoardUtils.SwapJewelBoard(jewel);
    }
  }
}
