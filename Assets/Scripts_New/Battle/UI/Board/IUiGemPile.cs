using System;
using System.Collections;
using System.Collections.Generic;
using Battle.UI.Jewel;
using UnityEngine;

namespace Battle.UI.Board
{
  public interface IUiGemPile
  {
    //Action<IUiJewel[]> OnPileChanged { get; set; }
    void AddJewel(IUiJewel uiJewel);
    //void RemoveJewel(IUiJewel uiJewel);
    //void Restart();
  }
}
