using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Board
{
  public interface IUiPlayerBoardUtils
  {
    void Draw(IRuntimeJewel jewel);
    void Discard(IRuntimeJewel jewel);
    void PlayCard(IRuntimeJewel jewel);
  }
}

