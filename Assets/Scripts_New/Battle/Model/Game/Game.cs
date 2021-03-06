﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Game.Mechanics;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard;
using Battle.Model.RuntimeBoard.Controller;
using Battle.Model.RuntimeBoard.Utils;
using Battle.Model.TurnLogic;
using UnityEngine;

namespace Battle.Model.Game
{
  /// <summary>
  ///     Simple concrete Game Implementation.
  ///     TODO: Consider to break this class down into small partial classes.
  /// </summary>
  public class Game : IPrimitiveGame
  {
    //----------------------------------------------------------------------------------------------------------

    #region Constructor

    public Game(List<IPlayer> players, IRuntimeBoard GameBoard, Battle.Configurations.Configurations configurations)
    {
      Configurations = configurations;
      TurnLogic = new Battle.Model.TurnLogic.TurnLogic(players);
      ProcessPreStartGame = new PreStartGameMechanics(this);
      ProcessStartGame = new StartGameMechanics(this);
      ProcessStartPlayerTurn = new StartPlayerTurnMechanics(this);
      ProcessFinishPlayerTurn = new FinishPlayerTurnMechanics(this);
      ProcessTick = new TickTimeMechanics(this);
      ProcessSwap = new SwapMechanics(this);
      ProcessFinishGame = new FinishGameMechanics(this);
      gameBoard = GameBoard;

      AddMechanic(ProcessPreStartGame);
      AddMechanic(ProcessStartGame);
      AddMechanic(ProcessStartPlayerTurn);
      AddMechanic(ProcessFinishPlayerTurn);
      AddMechanic(ProcessTick);
      AddMechanic(ProcessSwap);
      AddMechanic(ProcessFinishGame);
      Logger.Log<Game>("Game Created", "blue");
      Debug.Log("Game Created!");
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Properties

    public List<IPlayer> Players => TurnLogic.Players;
    public bool IsGameStarted { get; set; }
    public bool IsGameFinished { get; set; }
    public bool IsTurnInProgress { get; set; }
    public int TurnTime { get; set; }
    public int TotalTime { get; set; }
    public IRuntimeBoard GameBoard => gameBoard;
    public Battle.Configurations.Configurations Configurations { get; }

    #region Processes

    public List<BaseGameMechanics> Mechanics { get; set; } = new List<BaseGameMechanics>();
    private IRuntimeBoard gameBoard;
    public ITurnLogic TurnLogic { get; set; }
    private PreStartGameMechanics ProcessPreStartGame { get; }
    private StartGameMechanics ProcessStartGame { get; }
    private TickTimeMechanics ProcessTick { get; }
    private StartPlayerTurnMechanics ProcessStartPlayerTurn { get; }
    private FinishPlayerTurnMechanics ProcessFinishPlayerTurn { get; }
    private SwapMechanics ProcessSwap { get; }
    private FinishGameMechanics ProcessFinishGame { get; set; }

    #endregion

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Execution

    public void PreStartGame()
    {
      ProcessPreStartGame.Execute();
    }

    public void StartGame()
    {
      ProcessStartGame.Execute();
    }

    public void StartCurrentPlayerTurn()
    {
      ProcessStartPlayerTurn.Execute();
    }

    public void FinishCurrentPlayerTurn()
    {
      ProcessFinishPlayerTurn.Execute();
    }

    public void EndGame()
    {
      ProcessFinishGame.Execute();
    }

    public void Tick()
    {
      ProcessTick.Execute();
    }

    //public void Swap(SwapMechanics.RuntimeSwapData data)
    //{
    //  ProcessSwap.Execute(data);
    //}

    //public IEnumerator ExecuteAiAction(PlayerSeat seat)
    //{
    //  yield return new WaitForSeconds(0.5f);
    //}

    //public IEnumerator ExecuteAiSwap(PlayerSeat seat)
    //{
    //  List<JewelID> prefJewels = new List<JewelID>() { JewelID.wrath };

    //  // Look through all of the current mohe's abilities
    //  IRuntimeMoheData mohe = GameController.Instance.GetPlayerController(seat).Player.Roster.CurrentMohe();

    //  // If there are any abilities that are uncharged, add to abilities buffer
    //  foreach (var abl in mohe.Abilities)
    //  {
    //    foreach (var comp in abl.AbilityComponents)
    //    {
    //      if (comp.Has < comp.Needs && !prefJewels.Contains(comp.JewelType))
    //      {
    //        prefJewels.Add(comp.JewelType);
    //      }
    //    }
    //  }

    //  // look for matches
    //  List<SwapChoices> matchesBuff = FindMatchesUtil.FindBestMatches(GameBoard.GetBoardData().GetMap(), prefJewels);

    //  matchesBuff = matchesBuff.OrderByDescending(m => m.matches).ToList();
    //  //foreach (var match in matchesBuff)
    //  //{
    //  //  Debug.Log("matchesBuff: " + match.jewel1.Pos.ToString() + ":" + match.jewel2.Pos.ToString() + " = " + match.matches.ToString());
    //  //}

    //  if (matchesBuff.Count > 0)
    //  {
    //    Debug.Log("Swapping: " + matchesBuff[0].jewel1.Pos.ToString() + "<=>" + matchesBuff[0].jewel2.Pos.ToString());

    //    // click first gem
    //    GameEvents.Instance.Notify<ISelectJewel>(i => i.OnSelect(matchesBuff[0].jewel1));
    //    Debug.Log("Click: " + matchesBuff[0].jewel1.Pos.ToString());

    //    yield return new WaitForSeconds(0.5f);
    //    while (!BoardController.Instance.CanManipulate()) { }

    //    // click second gem
    //    GameEvents.Instance.Notify<ISelectJewel>(i => i.OnSelect(matchesBuff[0].jewel2));
    //    Debug.Log("Click: " + matchesBuff[0].jewel2.Pos.ToString());
    //  }
    //  else
    //  {
    //    Debug.Log("Nothing in buffer. Reshuffle the board!");
    //    GameEvents.Instance.Notify<IResetBoard>(i => i.OnBoardResetCheck());
    //  }
    //}

    #endregion


    void AddMechanic(BaseGameMechanics mechanic)
    {
      Mechanics.Add(mechanic);
    }

    //----------------------------------------------------------------------------------------------------------
  }
}