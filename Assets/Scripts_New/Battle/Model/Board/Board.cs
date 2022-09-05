﻿using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Data;
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
      boardData = new BoardData(configuration);

      ProcessCascadeBoardMechanics = new CascadeBoardMechanics(this);
      ProcessEndGameBoardMechanics = new EndGameBoardMechanics(this);
      ProcessSelectBoardMechanics = new SelectBoardMechanics(this);
      ProcessStartGameBoardMechanics = new StartGameBoardMechanics(this);
      ProcessSwapBoardMechanics = new SwapBoardMechanics(this);
      UnselectBoardMechanics = new UnselectBoardMechanics(this);
      ActivateAbilityMechanics = new ActivateAbilityMechanics(this);
      InvokeActivateAbilityMechanics = new InvokeActivateAbilityMechanics(this);
      RemoveJewelBoardMechanics = new RemoveJewelBoardMechanics(this);
      ActivateItemAbilityMechanics = new ActivateItemAbilityMechanics(this);

      Mechanics.Add(ProcessCascadeBoardMechanics);
      Mechanics.Add(ProcessEndGameBoardMechanics);
      Mechanics.Add(ProcessSelectBoardMechanics);
      Mechanics.Add(ProcessStartGameBoardMechanics);
      Mechanics.Add(ProcessSwapBoardMechanics);
      Mechanics.Add(UnselectBoardMechanics);
      Mechanics.Add(ActivateAbilityMechanics);
      Mechanics.Add(InvokeActivateAbilityMechanics);
      Mechanics.Add(RemoveJewelBoardMechanics);
      Mechanics.Add(ActivateItemAbilityMechanics);
    }

    private Battle.Configurations.Configurations configuration { get; }
    private BoardData boardData;

    private List<BaseBoardMechanics> Mechanics { get; set; } = new List<BaseBoardMechanics>();
    private CascadeBoardMechanics ProcessCascadeBoardMechanics { get; }
    private EndGameBoardMechanics ProcessEndGameBoardMechanics { get; }
    private StartGameBoardMechanics ProcessStartGameBoardMechanics { get; }
    private SelectBoardMechanics ProcessSelectBoardMechanics { get; }
    private SwapBoardMechanics ProcessSwapBoardMechanics { get; }
    private UnselectBoardMechanics UnselectBoardMechanics { get; }
    private ActivateAbilityMechanics ActivateAbilityMechanics { get; }
    private InvokeActivateAbilityMechanics InvokeActivateAbilityMechanics { get; }
    private RemoveJewelBoardMechanics RemoveJewelBoardMechanics { get; }
    private ActivateItemAbilityMechanics ActivateItemAbilityMechanics { get; }

    public Func<IRuntimeJewel, bool> OnInvokeActionEffect { get; set; }
    public Action OnCleanAbility { get; set; }

    public BoardData GetBoardData()
    {
      return boardData;
    }

    public List<BaseBoardMechanics> GetMechanics()
    {
      return Mechanics;
    }

    public Action OnInvokeActionUIEffect { get; set; }
  }
}
