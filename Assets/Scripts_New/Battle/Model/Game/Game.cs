using System.Collections;
using System.Collections.Generic;
using Battle.Model.Game.Mechanics;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard;
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
    public IRuntimeBoard gameBoard { get; set; }
    public Battle.Configurations.Configurations Configurations { get; }

    #region Processes

    public List<BaseGameMechanics> Mechanics { get; set; } = new List<BaseGameMechanics>();
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

    public void Tick()
    {
      ProcessTick.Execute();
    }

    public void Swap(SwapMechanics.RuntimeSwapData data)
    {
      ProcessSwap.Execute(data);
    }

    public IEnumerator ExecuteAiTurn(PlayerSeat seat)
    {
      //bool GetSpace()
      //{
      //  Debug.Log("Press C");
      //  return Input.GetKeyDown(KeyCode.C);
      //}
      //var player = TurnLogic.GetPlayer(seat);
      //var team = player.Team;
      //var size = team.Size;

      //for (var i = 0; i < size; i++)
      //{
      //  var member = team.Members[i];

      //  if (Configurations.PlayerTurn.DebugAiTurn)
      //  {
      //    if (i > 0)
      //    {
      //      yield return new WaitUntil(GetSpace);
      //    }
      //    yield return new WaitForSeconds(0.5f);
      //  }

      //  member.ExecuteTurn();
      //}

      // AI Turn here!
      yield return new WaitForSeconds(0.5f);
    }

    #endregion


    void AddMechanic(BaseGameMechanics mechanic)
    {
      Mechanics.Add(mechanic);
    }

    //----------------------------------------------------------------------------------------------------------
  }
}