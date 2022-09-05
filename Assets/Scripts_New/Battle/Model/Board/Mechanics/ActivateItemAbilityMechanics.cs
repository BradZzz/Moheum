using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Item;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard;
using Patterns;
using UnityEngine;

namespace Battle.UI.RuntimeBoard.Mechanics
{
  public class ActivateItemAbilityMechanics : BaseBoardMechanics, IListener, ISelectItemButton
  {
    private BaseEffect effect;

    public ActivateItemAbilityMechanics(IRuntimeBoard Board) : base(Board)
    {
      GameEvents.Instance.AddListener(this);
      board = Board;
    }

    IRuntimeBoard board;
    
    public void OnSelectItemActionButton(IRuntimeItemData item, PlayerSeat seat)
    {
      board.OnInvokeActionEffect = null;
      board.OnInvokeActionUIEffect = null;
      board.OnCleanAbility = null;
      
      // check the item as a whole for validation
      if (item.UsableItem())
      {
        bool skipNotifyBoard = true;
        foreach (ItemData.ItemAbilityEffects abilityEffect in item.Item.Data.Abilities)
        {
          if (abilityEffect.ability != null && abilityEffect.ability.AfterEffect != null)
          {
            if (abilityEffect.ability.AfterEffect.RequiresPlayerClick)
              skipNotifyBoard = false;
            
            board.OnInvokeActionEffect += abilityEffect.ability.AfterEffect.Execute;
            board.OnInvokeActionUIEffect += () =>{ GameEvents.Instance.Notify<IUseItemActionButton>(i => i.OnUseItemActionButton(item,seat)); };
            board.OnCleanAbility += () => { GameEvents.Instance.Notify<IOnCleanItemAbility>(i => i.OnCleanItemActionButton(item,seat)); };
          }
        }
        // Check to see if there is something that the player needs to click on
        // If there is, go here
        if (skipNotifyBoard)
        {
          ActivateItemWithoutNotifyBoard();
        }
        else
        {
          NotifyBoard();
        }
      }
      else
      {
        Debug.Log("Item is not usable");
        GameEvents.Instance.Notify<IOnCleanItemAbility>(i => i.OnCleanItemActionButton(item,seat));
        NotifyEvaluate();
      }
    }

    void NotifyBoard()
    {
      Debug.Log("IPreActionBoard");
      GameEvents.Instance.Notify<IPreActionBoard>(i => i.OnPreActionCheck());
    }
    
    void ActivateItemWithoutNotifyBoard()
    {
      Debug.Log("IPreActionBoard");
      GameEvents.Instance.Notify<IInvokeActionBoard>(i => i.OnInvokeBoardActionCheck(null));
    }

    void NotifyEvaluate()
    {
      Debug.Log("IEvaluateBoard");
      GameEvents.Instance.Notify<IEvaluateBoard>(i => i.OnBoardEvaluateCheck());
    }
  }
}
