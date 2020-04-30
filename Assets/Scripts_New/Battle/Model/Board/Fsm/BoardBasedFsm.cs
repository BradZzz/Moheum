using System.Collections;
using System.Collections.Generic;
using Battle.Model.RuntimeBoard.Controller;
using Battle.Model.RuntimeBoard.Data;
using Patterns.StateMachine;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  public class BoardBasedFsm : BaseStateMachine
  {
    public BoardBasedFsm(IBoardController Handler, IBoardData BoardData) : base(Handler)
    {
      handler = Handler;
      boardData = BoardData;
      Initialize();
    }

    protected override void OnBeforeInitialize()
    {
      Logger.Log<BoardBasedFsm>("On Before Initialize: Create Game States");
      /*
       * Register States Here
       */
      //create states
      var cascade = new CascadeBoardState(this, boardData);
      var clean = new CleanBoardState(this, boardData);
      var evaluate = new EvaluateBoardState(this, boardData);
      var remove = new RemoveMatchesBoardState(this, boardData);
      var selected = new SelectedBoardState(this, boardData);
      var swap = new SwapBoardState(this, boardData);

      //register all states
      RegisterState(cascade);
      RegisterState(clean);
      RegisterState(evaluate);
      RegisterState(remove);
      RegisterState(selected);
      RegisterState(swap);
    }

    public new IBoardController Handler => handler;
    public IBoardData BoardData => boardData;

    private IBoardController handler;
    private IBoardData boardData;
  }
}
