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
  public class UiItemActionButton : UiBaseActionButton, IUiItemActionButton, IUseItemActionButton, IOnCleanItemAbility, IPlayerUpdateRuntime
  {
    private IUiActionActive actionOutline;
    private PlayerSeat Seat;
    private IPlayer uiPlayer;
    private IRuntimeItemData item;

    private Image image;
    private TextMeshProUGUI name;
    private TextMeshProUGUI description;
    private TextMeshProUGUI quantity;

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
      
      image = transform.Find("ItemPortrait").GetComponent<Image>();
      name = transform.Find("HeaderTxt").GetComponent<TextMeshProUGUI>();
      description = transform.Find("DescTxt").GetComponent<TextMeshProUGUI>();
      quantity = transform.Find("QuantityTxt").GetComponent<TextMeshProUGUI>();

      OnPlayerUpdateRuntime();
      return true;
    }

    public void OnClick()
    {
      Debug.Log("Item clicked");
      if (actionOutline.Active)
      {
        GameEvents.Instance.Notify<ISelectItemButton>(i => i.OnSelectItemActionButton(item, Seat));
      }
      else
      {
        OnToggle.Invoke(true);
      }
    }

    public void OnUseItemActionButton(IRuntimeItemData Item, PlayerSeat Seat)
    {
      Debug.Log("OnUseItemActionButton");
      if (item.InstanceID == Item.InstanceID && Seat == this.Seat)
      {
        item.UseItem();
        AfterUseActionEffect();
        GameEvents.Instance.Notify<IPlayerUpdateRuntime>(i => i.OnPlayerUpdateRuntime());
      }
    }

    public void OnCleanItemActionButton(IRuntimeItemData Item, PlayerSeat Seat)
    {
      Debug.Log("OnCleanItemActionButton");
      if (item.InstanceID == Item.InstanceID && Seat == this.Seat)
      {
        OnToggle.Invoke(false);
      }
    }

    public void OnPlayerUpdateRuntime()
    {
      image.sprite = item.Item.Data.Artwork;
      name.text = item.Item.Data.Name;
      description.text = item.Item.Data.Description;
      quantity.text = $"x{item.Quantity.ToString()}";
    }
  }
}
