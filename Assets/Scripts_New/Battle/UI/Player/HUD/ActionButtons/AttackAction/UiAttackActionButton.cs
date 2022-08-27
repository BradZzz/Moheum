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
  public class UiAttackActionButton : UiBaseActionButton, IUiAtkActionButton
  {
    private List<IUiAttackCost> CostPanels;

    private TextMeshProUGUI headerTxt;
    private TextMeshProUGUI descTxt;

    private PlayerSeat seat;
    private IRuntimeAbility ability;

    private IUiActionActive actionOutline;

    //public TextMeshProUGUI HeaderTxt => headerTxt;

    private IPlayer contPlayer;

    public override bool Populate(PlayerSeat Seat, int pos)
    {
      Outline outline = GetComponent<Outline>();
      ParticleSystem particleSystem = MBehaviour.GetComponentInChildren<ParticleSystem>();
      actionOutline = new UiActionActive(this, outline, particleSystem);

      seat = Seat;

      CostPanels = new List<IUiAttackCost>();

      headerTxt = transform.Find("HeaderTxt").GetComponent<TextMeshProUGUI>();
      descTxt = transform.Find("DescTxt").GetComponent<TextMeshProUGUI>();

      contPlayer = GameData.Instance.RuntimeGame.Players.Find(player => player.Seat == seat);
      IRoster pRoster = contPlayer.Roster;
      IRuntimeMoheData moheData = pRoster.CurrentMohe();

      //check to make sure the player has enough mohe for pos
      if (moheData.Abilities.Count <= pos)
        return false;

      //Populate headers and desc
      ability = moheData.Abilities[pos];
      headerTxt.text = ability.Ability.AbilityName;
      descTxt.text = ability.Ability.Description;

      Transform costParent = transform.Find("Cost");

      int transformChildren = costParent.childCount;
      for(int i = 0; i < transformChildren; i++) 
      {
        Transform costChild = costParent.GetChild(0);
        if (UiAtkActionCostPooler.Instance.CheckPooledObject(costChild.gameObject))
        {
          UiAtkActionCostPooler.Instance.ReleasePooledObject(costChild.gameObject);
        }
        else
        {
          Destroy(costChild.gameObject);
        }
      }

      for (int i = 0; i < ability.AbilityComponents.Count; i++)
      {
        IUiAttackCost cost = UiAtkActionCostPooler.Instance.Get(seat, ability, i);
        cost.MBehaviour.transform.SetParent(costParent);
        cost.MBehaviour.transform.localScale = Vector3.one;
        CostPanels.Add(cost);
      }

      return true;
    }

    public void OnClick()
    {
      if (ability != null && ability.AbilityCharged() && GameData.Instance.RuntimeGame.TurnLogic.IsMyTurn(contPlayer))
      {
        GameEvents.Instance.Notify<ISelectAtkActionButton>(i => i.OnSelectAtkActionButton(seat, ability.Ability.AbilityID));
      }
    }

    public void OnSelectAtkActionButton(PlayerSeat Seat, AbilityID Id)
    {
      if (Seat == seat && ability != null && Id == ability.Ability.AbilityID)
      {
        OnToggle.Invoke(!actionOutline.Active);
        GameEvents.Instance.Notify<IActionBoard>(i => i.OnBoardActionCheck(seat, actionOutline.Active ? ability : null));
      } else
      {
        OnToggle.Invoke(false);
      }
    }

    public void OnResetAtkActionButton()
    {
      OnToggle.Invoke(false);
    }

    public void OnUseAtkActionButton(PlayerSeat Seat, AbilityID Id)
    {
      if (Seat == seat && ability != null && Id == ability.Ability.AbilityID)
      {
        AfterUseActionEffect.Invoke();
      }
    }
  }
}
