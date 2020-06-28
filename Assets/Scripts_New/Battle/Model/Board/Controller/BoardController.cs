using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Fsm;
using Patterns;
using Patterns.StateMachine;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Controller
{
  public class BoardController : SingletonMB<BoardController>, IBoardController, IListener
  {
    /// <summary>
    ///     State machine that holds the board logic.
    /// </summary>
    private BoardBasedFsm BoardBasedLogic { get; set; }

    public MonoBehaviour MonoBehaviour => this;

    public string Name => gameObject.name;

    void Awake()
    {
      base.Awake();
      GameEvents.Instance.AddListener(this);
    }

    //private void Start()
    //{
    //  // Get config and board from gameobject
    //  BoardBasedLogic = new BoardBasedFsm(this, GameData.Instance.RuntimeGame.GameBoard.GetBoardData());
    //  OnTurnStart();
    //}

    public void OnStartGame(IPlayer starter)
    {
      Debug.Log("OnStartGame");
      BoardBasedLogic = new BoardBasedFsm(this, GameData.Instance.RuntimeGame.GameBoard);
      OnBoardCascadeCheck();
      //OnTurnStart();
    }

    public void OnTurnStart()
    {
      /*
       * CleanBoardState
       * SelectedBoardState
       * SwapBoardState
       * EvaluateBoardState
       * RemoveMatchesBoardState
       * CasecadeBoardState
       * CleanBoardState
       * ActionBoardState
       */
      //OnBoardCascadeCheck();
    }

    public IState CurrentState => BoardBasedLogic.PeekState();

    public bool CanManipulate()
    {
      return BoardBasedLogic.IsCurrent<CleanBoardState>();
    }

    public bool CanClickJewel()
    {
      return CanManipulate() || IsWaitingForAction();
    }

    public bool IsWaitingForAction()
    {
      return BoardBasedLogic.IsCurrent<PreActionBoardState>();
    }

    public void OnBoardCascadeCheck()
    {
      Debug.Log("CascadeBoardState");
      BoardBasedLogic.PopState();
      BoardBasedLogic.PushState<CascadeBoardState>();
    }

    public void OnBoardCleanCheck()
    {
      Debug.Log("CleanBoardState");
      BoardBasedLogic.PopState();
      BoardBasedLogic.PushState<CleanBoardState>();
    }

    public void OnBoardEvaluateCheck()
    {
      Debug.Log("EvaluateBoardState");
      BoardBasedLogic.PopState();
      BoardBasedLogic.PushState<EvaluateBoardState>();
    }

    public void OnBoardRemoveSelectedCheck()
    {
      Debug.Log("RemoveSelectedBoardState");
      BoardBasedLogic.PopState();
      BoardBasedLogic.PushState<RemoveSelectedBoardState>();
    }

    public void OnBoardSelectedCheck()
    {
      Debug.Log("SelectedBoardState");
      BoardBasedLogic.PopState();
      BoardBasedLogic.PushState<SelectedBoardState>();
    }

    public void OnBoardSwapCheck()
    {
      Debug.Log("SwapBoardState");
      BoardBasedLogic.PopState();
      BoardBasedLogic.PushState<SwapBoardState>();
    }

    public void OnPreActionCheck()
    {
      Debug.Log("PreActionBoardState");
      BoardBasedLogic.PopState();
      BoardBasedLogic.PushState<PreActionBoardState>();
    }

    public void OnPostActionCheck()
    {
      Debug.Log("ActionBoardState");
      BoardBasedLogic.PopState();
      BoardBasedLogic.PushState<ActionBoardState>();
    }

    public void OnBoardResetCheck()
    {
      BoardBasedLogic.PopState();
      BoardBasedLogic.PushState<ResetBoardState>();
    }
  }
}
