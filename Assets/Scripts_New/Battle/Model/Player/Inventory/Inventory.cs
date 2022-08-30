using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Item;
using Battle.Model.MoheModel;
using UnityEngine;

namespace Battle.Model.Player
{
  public class Inventory : IInventory
  {
    public Inventory(List<IRuntimeItemData> InventoryList)
    {
      inventoryList = InventoryList;
    }

    public List<IRuntimeItemData> InventoryList => inventoryList;
    private List<IRuntimeItemData> inventoryList;
  }
}
