using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using Battle.UI.Jewel;
using UnityEngine;

namespace Battle.UI.Board
{
  public interface IUiBoard
  {
    List<IUiJewel> Jewels { get; }

    Action<IUiJewel> OnJewelPlayed { get; set; }
    Action<IUiJewel> OnJewelSelected { get; set; }
    Action<IUiJewel> OnJewelDiscarded { get; set; }

    void SwapSelected();
    void Unselect();
    void UnselectJewel(IUiJewel jewel);
    void SelectJewel(IUiJewel jewel);
    void EnableJewels();
    void DisableJewels();

    IUiJewel GetJewel(IRuntimeJewel jewel);
  }
}