using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Game;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiFleeActionButton : UiBaseActionButton, IUiFleeActionButton, IFleeSuccessful, IFleeFailure
  {
    private IUiActionActive actionOutline;
    private PlayerSeat Seat;

    public override bool Populate(PlayerSeat Seat, int pos)
    {
      if (pos > 0)
        return false;

      this.Seat = Seat;
      Outline outline = GetComponent<Outline>();
      ParticleSystem particleSystem = MBehaviour.GetComponentInChildren<ParticleSystem>();
      actionOutline = new UiActionActive(this, outline, particleSystem);
      
      return true;
    }

    public void OnClick()
    {
      // Todo: Event Listener, trying to escape and action
      Debug.Log("Trying to flee!");
      if (actionOutline.Active)
        GameEvents.Instance.Notify<ISelectFleeButton>(i => i.OnSelectFleeActionButton(Seat));
      OnToggle.Invoke(!actionOutline.Active);
    }
    
    IEnumerator FleeWait()
    {
      yield return new WaitForSeconds(2f);
      GameEvents.Instance.Notify<IFleeBattle>(i => i.OnFleeBattle(Seat));
    }

    public void OnFleeSuccess(PlayerSeat seat)
    {
      OnToggle.Invoke(false);
      AfterUseActionEffect.Invoke();
      StartCoroutine(FleeWait());
    }

    public void OnFleeFailure(PlayerSeat seat)
    {
      OnToggle.Invoke(false);
    }
  }
}
