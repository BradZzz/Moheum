using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.MoheModel.ExpTypes;
using Battle.Model.MoheModel.Mechanics;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Model.Item
{
  public class RuntimeItemData : IRuntimeItemData
  {
    /*
     * Health
     * Progress w/ abilities
     */
    public RuntimeItemData(IItem Item, int Quantity, PlayerSeat Seat)
    {
        item = Item;
        quantity = Quantity;
        playerSeat = Seat;
    }

    public IItem Item => item;
    private IItem item;

    public int Quantity => quantity;
    private int quantity;

    public PlayerSeat PlayerSeat => playerSeat;
    private PlayerSeat playerSeat;
    
    public bool UsableItem()
    {
      return quantity > 0;
    }

    public bool UseItem()
    {
      if (UsableItem())
      {
        quantity--;
        return true;
      }
      return false;
    }
  }
}
