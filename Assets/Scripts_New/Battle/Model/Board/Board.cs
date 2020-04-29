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
    public Board(Battle.Configurations.Configurations configuration)
    {
      /*
       * Things the board needs to have:
       * # of Jewels
       * Jewel Dimensions (Board Configs)
       * Where Jewels are
       * Board Mechanics(Start Turn, End Turn)
       */

      this.configuration = configuration;
      jewelMap = new IRuntimeJewel[configuration.boardConfigs.width, configuration.boardConfigs.height];

      ProcessCascadeBoardMechanics = new CascadeBoardMechanics(this);
      ProcessEndGameBoardMechanics = new EndGameBoardMechanics(this);
      ProcessSelectBoardMechanics = new SelectBoardMechanics(this);
      ProcessStartGameBoardMechanics = new StartGameBoardMechanics(this);
      ProcessSwapBoardMechanics = new SwapBoardMechanics(this);

      Mechanics.Add(ProcessCascadeBoardMechanics);
      Mechanics.Add(ProcessEndGameBoardMechanics);
      Mechanics.Add(ProcessSelectBoardMechanics);
      Mechanics.Add(ProcessStartGameBoardMechanics);
      Mechanics.Add(ProcessSwapBoardMechanics);
    }

    private Battle.Configurations.Configurations configuration { get; }
    private IRuntimeJewel[,] jewelMap;

    private List<BaseBoardMechanics> Mechanics { get; set; } = new List<BaseBoardMechanics>();
    private CascadeBoardMechanics ProcessCascadeBoardMechanics { get; }
    private EndGameBoardMechanics ProcessEndGameBoardMechanics { get; }
    private StartGameBoardMechanics ProcessStartGameBoardMechanics { get; }
    private SelectBoardMechanics ProcessSelectBoardMechanics { get; }
    private SwapBoardMechanics ProcessSwapBoardMechanics { get; }

    public IRuntimeJewel[,] GetMap()
    {
      return jewelMap;
    }

    public List<BaseBoardMechanics> GetMechanics()
    {
      return Mechanics;
    }

    public void SetJewel(IRuntimeJewel jewel, int x, int y)
    {
      jewelMap[x, y] = jewel;
    }
  }
}
