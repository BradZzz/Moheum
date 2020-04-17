using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.Player;
using Battle.UI.RuntimeBoard.Mechanics;
using Patterns;
using UnityEngine;

namespace Battle.Model.RuntimeBoard
{
  // Use IListener for the moodels. UIListener for the UI controllers
  public class Board : IRuntimeBoard
  {
    public Board()
    {
      /*
       * Things the board needs to have:
       * # of Jewels
       * Jewel Dimensions (Board Configs)
       * Where Jewels are
       * Board Mechanics(Start Turn, End Turn)
       */
      width = 10;
      height = 10;
      jewelMap = new IRuntimeJewel[width, height];

      ProcessCascadeBoardMechanics = new CascadeBoardMechanics(this);
      ProcessEndGameBoardMechanics = new EndGameBoardMechanics(this);
      ProcessStartGameBoardMechanics = new StartGameBoardMechanics(this);
      ProcessSwapBoardMechanics = new SwapBoardMechanics(this);

      Mechanics.Add(ProcessCascadeBoardMechanics);
      Mechanics.Add(ProcessEndGameBoardMechanics);
      Mechanics.Add(ProcessStartGameBoardMechanics);
      Mechanics.Add(ProcessSwapBoardMechanics);
    }

    private int height, width;
    private IRuntimeJewel[,] jewelMap;

    public List<BaseBoardMechanics> Mechanics { get; set; } = new List<BaseBoardMechanics>();
    private CascadeBoardMechanics ProcessCascadeBoardMechanics { get; }
    private EndGameBoardMechanics ProcessEndGameBoardMechanics { get; }
    private StartGameBoardMechanics ProcessStartGameBoardMechanics { get; }
    private SwapBoardMechanics ProcessSwapBoardMechanics { get; }
  }
}
