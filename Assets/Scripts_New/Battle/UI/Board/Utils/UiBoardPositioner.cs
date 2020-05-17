using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Board.Utils
{
  public class UiBoardPositioner : IUiBoardPositioner
  {
    private static float OFFSET_WIDTH = .5f;
    private static float OFFSET_HEIGHT = .5f;

    private static float MARGIN_WIDTH = .84f;
    private static float MARGIN_HEIGHT = .84f;

    public float GetOffsetWidth()
    {
      return OFFSET_WIDTH;
    }

    public float GetOffsetHeight()
    {
      return OFFSET_HEIGHT;
    }

    public float GetMarginWidth()
    {
      return MARGIN_WIDTH;
    }

    public float GetMarginHeight()
    {
      return MARGIN_HEIGHT;
    }

    public Vector2 GetBoardTopPos()
    {
      Vector2 jewelPos = new Vector2(0, 5);
      return jewelPos;
    }

    public Vector2 OffsetJewelByPosition(Vector2 pos)
    {
      IRuntimeJewel[,] jewelMap = GameData.Instance.RuntimeGame.GameBoard.GetBoardData().GetMap();
      int width = jewelMap.GetLength(0);
      int height = jewelMap.GetLength(1);

      Vector2 middle = new Vector2((int)width / 2, (int)height / 2);
      return new Vector2(pos.x - middle.x, pos.y - middle.y);
    }

    public Vector2 GetNextJewelPosition(Vector2 pos, Vector2 center)
    {
      Vector2 jewelPos = new Vector3(center.x + OFFSET_WIDTH + pos.x * MARGIN_WIDTH, center.y + OFFSET_HEIGHT + pos.y * MARGIN_HEIGHT);
      return jewelPos;
    }
  }
}
