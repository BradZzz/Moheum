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
      board.OnInvokeActionUIEffect = null;
      board.OnCleanAbility = null;
      bool skipNotifyBoard = true;
      if (ability != null && ability.Ability.AfterEffects != null)
      {
        foreach (BaseEffect effect in ability.Ability.AfterEffects)
        {
          if (effect.RequiresPlayerClick)
            skipNotifyBoard = false;
          
          board.OnInvokeActionEffect += effect.Execute;
        }
        board.OnInvokeActionUIEffect += () => { GameEvents.Instance.Notify<IUseAtkActionButton>(i => i.OnUseAtkActionButton(seat,ability.Ability.AbilityID)); };
        board.OnCleanAbility += ability.ResetAbility;
        
        // None of the effects require a gem click, so skip that step
        if (skipNotifyBoard)
        {
          GameEvents.Instance.Notify<IInvokeActionBoard>(i => i.OnInvokeBoardActionCheck(null));
        }
        else
        {
          // a gem click is needed. notify the board to lock and wait for input
          NotifyBoard();
        }
      }
      else
      {
        NotifyEvaluate();
      }
    }

    void NotifyBoard()
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
