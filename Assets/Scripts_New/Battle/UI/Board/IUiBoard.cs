using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard.Data;
using Battle.UI.Jewel;
using Battle.UI.Utils;
using Patterns.StateMachine;
using UnityEngine;

namespace Battle.UI.Board
{
  public interface IUiBoard : IUiGemPile
  {
    Transform Transform { get; }
  }
}