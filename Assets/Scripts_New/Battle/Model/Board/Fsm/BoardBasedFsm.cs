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
    public BoardBasedFsm(IBoardController Handler, IRuntimeBoard Board) : base(Handler)
    {
      handler = Handler;
      board = Board;
      Initialize();
    }

    protected override void OnBeforeInitialize()
    {
      Logger.Log<BoardBasedFsm>("On Before Initialize: Create Game States");
      /*
       * Register States Here
       */
      //create states
      var cascade = new CascadeBoardState(this, board);
      var clean = new CleanBoardState(this, board);
      var evaluate = new EvaluateBoardState(this, board);
      var remove = new RemoveSelectedBoardState(this, board);
      var selected = new SelectedBoardState(this, board);
      var swap = new SwapBoardState(this, board);
      var action = new ActionBoardState(this, board);
      var preaction = new PreActionBoardState(this, board);
      var invalid = new InvalidBoardState(this, board);
      var reset = new ResetBoardState(this, board);

      //register all states
      RegisterState(cascade);
      RegisterState(clean);
      RegisterState(evaluate);
      RegisterState(remove);
      RegisterState(selected);
      RegisterState(swap);
      RegisterState(action);
      RegisterState(preaction);
      RegisterState(invalid);
      RegisterState(reset);
    }

    public new IBoardController Handler => handler;
    public IRuntimeBoard Board => board;

    private IBoardController handler;
    private IRuntimeBoard board;
  }
}
