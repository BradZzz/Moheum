using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Board.Utils
{
  public interface IUiPlayerBoardUtils
  {
    MonoBehaviour MBehaviour { get; }
    Transform DeckPosition { get; }
    float JEWELFALLDELAY { get; }
    IUiBoard PlayerBoard { get; }

    void CascadeJewelBoard(IRuntimeJewel jewel);
    void SwapJewelBoard(IRuntimeJewel jewel);

    //void Discard(IRuntimeJewel jewel);
    //void PlayCard(IRuntimeJewel jewel);
  }
}

