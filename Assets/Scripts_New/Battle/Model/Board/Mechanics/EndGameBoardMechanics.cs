using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard;
using Battle.Model.RuntimeBoard.Controller;
using Battle.Model.RuntimeBoard.Fsm;
using Patterns;
using UnityEngine;

namespace Battle.UI.RuntimeBoard.Mechanics
{
  public class EndGameBoardMechanics : BaseBoardMechanics, IListener, IFinishGame
  {
    public EndGameBoardMechanics(IRuntimeBoard board) : base(board)
    {
      GameEvents.Instance.AddListener(this);
      board = Board;
    }

    public void OnFinishGame(IPlayer winner)
    {
      BoardController.Instance.OnInvalidateBoardState();
    }
  }
}
