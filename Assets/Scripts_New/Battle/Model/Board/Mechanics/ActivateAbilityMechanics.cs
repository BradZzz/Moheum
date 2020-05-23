using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard;
using Patterns;
using UnityEngine;

namespace Battle.UI.RuntimeBoard.Mechanics
{
  public class ActivateAbilityMechanics : BaseBoardMechanics, IListener, IActionBoard
  {
    private BaseEffect effect;

    public ActivateAbilityMechanics(IRuntimeBoard Board) : base(Board)
    {
      GameEvents.Instance.AddListener(this);
      board = Board;
    }

    IRuntimeBoard board;

    public void OnBoardActionCheck(PlayerSeat seat, IRuntimeAbility ability)
    {
      board.OnInvokeActionEffect = null;
      board.OnCleanAbility = null;
      if (ability.Ability.AfterEffect != null)
      {
        board.OnInvokeActionEffect += ability.Ability.AfterEffect.Execute;
        board.OnCleanAbility += ability.ResetAbility;
        NotifyAction();
      }
      else
      {
        NotifyEvaluate();
      }
    }

    void NotifyAction()
    {
      Debug.Log("IPreActionBoard");
      GameEvents.Instance.Notify<IPreActionBoard>(i => i.OnPreActionCheck());
    }

    void NotifyEvaluate()
    {
      Debug.Log("IEvaluateBoard");
      GameEvents.Instance.Notify<IEvaluateBoard>(i => i.OnBoardEvaluateCheck());
    }
  }
}
