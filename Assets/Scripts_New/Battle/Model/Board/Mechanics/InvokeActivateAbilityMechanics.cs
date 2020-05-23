using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.RuntimeBoard;
using Patterns;
using UnityEngine;

namespace Battle.UI.RuntimeBoard.Mechanics
{
  public class InvokeActivateAbilityMechanics : BaseBoardMechanics, IListener, IInvokeActionBoard
  {
    private BaseEffect effect;

    public InvokeActivateAbilityMechanics(IRuntimeBoard Board) : base(Board)
    {
      GameEvents.Instance.AddListener(this);
      board = Board;
    }

    IRuntimeBoard board;

    public void OnInvokeBoardActionCheck(IRuntimeJewel jewel)
    {
      if (board.OnInvokeActionEffect != null)
      {
        if (board.OnInvokeActionEffect.Invoke(jewel))
        {
          Notify();
        }
      }
    }

    void Notify()
    {
      GameEvents.Instance.Notify<IPostActionBoard>(i => i.OnPostActionCheck());
    }
  }
}
