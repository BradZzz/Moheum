using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Board.Utils
{
  public class UiBoardPositioner : IUiBoardPositioner
  {
    private static int MARGIN_WIDTH = 1;
    private static int MARGIN_HEIGHT = 1;

    public int GetMarginWidth()
    {
      return MARGIN_WIDTH;
    }

    public int GetMarginHeight()
    {
      return MARGIN_HEIGHT;
    }

    public Vector2 GetNextJewelPosition(Vector2 pos, Vector2 center)
    {
      Vector2 jewelPos = new Vector3(center.x + pos.x * MARGIN_WIDTH, center.y + pos.y * MARGIN_HEIGHT);
      return jewelPos;
    }
  }
}
