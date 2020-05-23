using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Data;
using Patterns;
using Patterns.StateMachine;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  public abstract class BaseBoardState : IState, IListener, IStartGame
  {
    protected BaseBoardState(BoardBasedFsm Fsm, IRuntimeBoard Board)
    {
      fsm = Fsm;
      board = Board;

      //Subscribe game events 
      GameEvents.Instance.AddListener(this);
      IsInitialized = true;
    }

    private BoardBasedFsm fsm;
    private IRuntimeBoard board;

    public bool IsInitialized { get; }

    public void OnClear()
    {
    }

    public virtual void OnEnterState()
    {
    }

    public void OnExitState()
    {
    }

    public void OnInitialize()
    {

    }

    public void OnNextState(IState next)
    {

    }

    protected virtual void OnNextState(BaseBoardState nextState)
    {
      fsm.PopState();
      fsm.PushState(nextState);
    }

    public void OnStartGame(IPlayer starter)
    {

    }

    public void OnUpdate()
    {

    }
  }
}
