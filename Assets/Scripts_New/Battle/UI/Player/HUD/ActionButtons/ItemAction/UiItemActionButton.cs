using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Game;
using Battle.Model.Item;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiItemActionButton : UiBaseActionButton, IUiItemActionButton
  {
    private IUiActionActive actionOutline;
    private PlayerSeat Seat;
    private IPlayer uiPlayer;
    private IRuntimeItemData item;

    public override bool Populate(PlayerSeat Seat, int pos)
    {
      uiPlayer = GameData.Instance.RuntimeGame.Players.Find(player => player.Seat == Seat);
      
      if (pos < 0 || pos >= uiPlayer.Inventory.InventoryList.Count)
        return false;
      
      this.Seat = Seat;
      Outline outline = GetComponent<Outline>();
      ParticleSystem particleSystem = MBehaviour.GetComponentInChildren<ParticleSystem>();
      actionOutline = new UiActionActive(this, outline, particleSystem);
      item = uiPlayer.Inventory.InventoryList[pos];
      return true;
    }

    public void OnClick()
    {
      Debug.Log("Item clicked");
      OnToggle.Invoke(!actionOutline.Active);
    }
  }
}
