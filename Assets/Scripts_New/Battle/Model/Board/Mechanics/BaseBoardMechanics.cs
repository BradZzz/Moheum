using System.Collections;
using System.Collections.Generic;
using Battle.Model.RuntimeBoard;
using UnityEngine;

namespace Battle.UI.RuntimeBoard.Mechanics
{
  public class BaseBoardMechanics : MonoBehaviour
  {
    /// <summary>
    ///     Player reference.
    /// </summary>
    protected IRuntimeBoard Board { get; }


    protected BaseBoardMechanics(IRuntimeBoard board)
    {
      Board = board;
    }
  }
}
