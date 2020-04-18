using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Board.Utils
{
  public interface IUiPlayerBoardUtils
  {
    void Draw(IRuntimeJewel jewel, Vector2 pos);
    void Discard(IRuntimeJewel jewel);
    void PlayCard(IRuntimeJewel jewel);
  }
}

