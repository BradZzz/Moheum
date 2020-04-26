﻿using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Board.Utils
{
  public class UiBoardPositioner : IUiBoardPositioner
  {
    private static float OFFSET_WIDTH = .5f;
    private static float OFFSET_HEIGHT = .5f;

    private static float MARGIN_WIDTH = .9f;
    private static float MARGIN_HEIGHT = .9f;

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

    public Vector2 GetNextJewelPosition(Vector2 pos, Vector2 center)
    {
      Vector2 jewelPos = new Vector3(center.x + OFFSET_WIDTH + pos.x * MARGIN_WIDTH, center.y + OFFSET_HEIGHT + pos.y * MARGIN_HEIGHT);
      return jewelPos;
    }
  }
}