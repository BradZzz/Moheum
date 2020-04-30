using System.Collections;
using System.Collections.Generic;
using Patterns.StateMachine;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Controller
{
  public interface IBoardController : IStateMachineHandler
  {
    MonoBehaviour MonoBehaviour { get; }
  }
}
