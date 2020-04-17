using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using Battle.Model.Player;
using Battle.UI.Jewel;
using Battle.UI.Player;
using UnityEngine;

namespace Battle.UI.Board
{
  public class UiBoard : UIGemPile, IUiBoard
  {
    protected override void Awake()
    {
      base.Awake();
      //Controller = GetComponent<IUiPlayer>();

      //Draw Jewels here

      //TargetResolver = transform.parent.GetComponentInChildren<ITargetResolver>();
      //PlayerTeams = transform.parent.parent.GetComponentsInChildren<IUiPlayerTeam>();
      //foreach (var team in PlayerTeams)
      //{
      //  team.OnPileChanged += (characters, capitain) => EnableCards();
      //  team.OnCharacterSelected += (charac) => { if (charac.IsUser) DisableCards(); };
      //}
      //TargetResolver.OnTargetsResolve += (card) => EnableCards();
    }

    private IUiPlayer Controller { get; set; }

    // Add actions here whenever these actions happen to cascade
    public Action<IUiJewel> OnJewelPlayed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action<IUiJewel> OnJewelSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action<IUiJewel> OnJewelDiscarded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    List<IUiJewel> IUiBoard.Jewels => throw new NotImplementedException();

    Action<IUiJewel> IUiBoard.OnJewelPlayed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Action<IUiJewel> IUiBoard.OnJewelSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Action<IUiJewel> IUiBoard.OnJewelDiscarded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void DisableJewels()
    {
      throw new NotImplementedException();
    }

    public void DiscardJewel(IUiJewel uiCard)
    {
      throw new NotImplementedException();
    }

    public void EnableJewels()
    {
      throw new NotImplementedException();
    }

    public IUiJewel GetJewel(IRuntimeJewel card)
    {
      throw new NotImplementedException();
    }

    public void PlayJewel(IUiJewel uiCard)
    {
      throw new NotImplementedException();
    }

    public void PlaySelected()
    {
      throw new NotImplementedException();
    }

    public void SelectJewel(IUiJewel uiCard)
    {
      throw new NotImplementedException();
    }

    public void Unselect()
    {
      throw new NotImplementedException();
    }

    public void UnselectJewel(IUiJewel uiCard)
    {
      throw new NotImplementedException();
    }

    void IUiBoard.PlaySelected()
    {
      throw new NotImplementedException();
    }

    void IUiBoard.Unselect()
    {
      throw new NotImplementedException();
    }

    void IUiBoard.PlayJewel(IUiJewel uiCard)
    {
      throw new NotImplementedException();
    }

    void IUiBoard.SelectJewel(IUiJewel uiCard)
    {
      throw new NotImplementedException();
    }

    void IUiBoard.DiscardJewel(IUiJewel uiCard)
    {
      throw new NotImplementedException();
    }

    void IUiBoard.UnselectJewel(IUiJewel uiCard)
    {
      throw new NotImplementedException();
    }

    void IUiBoard.EnableJewels()
    {
      throw new NotImplementedException();
    }

    void IUiBoard.DisableJewels()
    {
      throw new NotImplementedException();
    }

    IUiJewel IUiBoard.GetJewel(IRuntimeJewel card)
    {
      throw new NotImplementedException();
    }
  }
}
