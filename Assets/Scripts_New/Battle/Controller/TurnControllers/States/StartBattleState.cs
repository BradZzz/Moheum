using System.Collections;
using Battle.GameEvent;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Controller.TurnControllers.States
{
  public class StartBattleState : BaseBattleState, IStartGame
  {
    //----------------------------------------------------------------------------------------------------------

    #region Constructor

    public StartBattleState(TurnBasedFsm fsm, IGameData gameData, Battle.Configurations.Configurations configurations)
      : base(fsm,gameData, configurations)
    {
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Operations

    public override void OnEnterState()
    {
      base.OnEnterState();
      //schedule pre game
      Fsm.Handler.MonoBehaviour.StartCoroutine(PreGameRoutine());

      //schedule start game
      Fsm.Handler.MonoBehaviour.StartCoroutine(StartGameRoutine());
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Game Events

    void IStartGame.OnStartGame(IPlayer starter)
    {
      var nextState = Fsm.GetPlayerController(starter);
      //Debug.Log("OnStartGame nextState: " + nextState.ToString());
      Fsm.Handler.MonoBehaviour.StartCoroutine(NextStateRoutine(nextState));
    }

    private IEnumerator NextStateRoutine(BaseBattleState nextState)
    {
      yield return new WaitForSeconds(Configurations.FirstPlayer);
      OnNextState(nextState);
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Coroutines

    private IEnumerator PreGameRoutine()
    {
      yield return new WaitForSeconds(Configurations.PreGameEvent);
      GameData.RuntimeGame.PreStartGame();
    }

    private IEnumerator StartGameRoutine()
    {
      var time = Configurations.PreGameEvent + Configurations.StartGameEvent;
      yield return new WaitForSeconds(time);
      GameData.RuntimeGame.StartGame();
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------
  }
}