using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.UI.Board.Utils
{
  public interface IUiBoardPositioner
  {
    float GetMarginWidth();
    float GetMarginHeight();
    float GetOffsetWidth();
    float GetOffsetHeight();
  }
}
