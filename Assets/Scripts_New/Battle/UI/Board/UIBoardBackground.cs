using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.Model.Jewel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Controller;
using Battle.Model.RuntimeBoard.Data;
using Battle.UI.Jewel;
using Battle.UI.Player;
using Battle.UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Board
{
  public class UIBoardBackground : UiListener
  {
    [ColorUsageAttribute(true,true,0f,8f,0.125f,3f)]
    public Color inactiveColor;
    
    [ColorUsageAttribute(true,true,0f,8f,0.125f,3f)]
    public Color activeColor;

    private Image background;
    
    void Awake()
    {
      Debug.Log("UIBoardBackground");
      background = GetComponent<Image>();
      background.color = inactiveColor;
    }

    void Update()
    {
      if (BoardController.Instance.IsWaitingForAction())
      {
        background.color = activeColor;
      }
      else
      {
        background.color = inactiveColor;
      }
    }
  }
}
