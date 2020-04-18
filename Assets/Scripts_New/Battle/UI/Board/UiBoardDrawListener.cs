using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.UI.Board.Utils;
using Battle.UI.Utils;
using UnityEngine;

namespace Battle.UI.Board
{
  public class UiBoardDrawListener : UiListener, IBoardDrawJewel
  {
    void Awake()
    {
      BoardUtils = transform.parent.GetComponentInChildren<IUiPlayerBoardUtils>();
    }

    private IUiPlayerBoardUtils BoardUtils { get; set; }

    public void OnDraw(IRuntimeJewel jewel, Vector2 pos)
    {
      BoardUtils.Draw(jewel, pos);
    }
  }
}
