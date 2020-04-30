using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard.Data;
using Battle.UI.Jewel;
using Battle.UI.Utils;
using Patterns.StateMachine;
using UnityEngine;

namespace Battle.UI.Board
{
  public interface IUiBoard : IUiGemPile
  {
    //IBoardData GetBoardData();

    //List<IUiJewel> Jewels { get; }

    //Action<IUiJewel> OnJewelPlayed { get; set; }
    //Action<IUiJewel> OnJewelSelected { get; set; }
    //Action<IUiJewel> OnJewelDiscarded { get; set; }

    //void PlaySelected();
    //void Unselect();
    //void PlayJewel(IUiJewel uiCard);
    //void SelectJewel(IUiJewel uiCard);
    //void DiscardJewel(IUiJewel uiCard);
    //void UnselectJewel(IUiJewel uiCard);
    //void EnableJewels();
    //void DisableJewels();
    //IUiJewel GetJewel(IRuntimeJewel card);
  }
}