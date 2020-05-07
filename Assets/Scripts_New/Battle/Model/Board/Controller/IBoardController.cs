using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Patterns.StateMachine;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Controller
{
  public interface IBoardController : IStateMachineHandler, IStartGame, ICascadeBoard,
    ICleanBoard, IEvaluateBoard, IRemoveSelectedBoard, ISelectedBoard, ISwapBoard
  {
    MonoBehaviour MonoBehaviour { get; }
  }
}
