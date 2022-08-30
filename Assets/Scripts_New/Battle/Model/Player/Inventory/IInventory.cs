using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Item;
using Battle.Model.MoheModel;
using Patterns;
using UnityEngine;

namespace Battle.Model.Player
{
  public interface IInventory
  {
    List<IRuntimeItemData> InventoryList { get; }
  }
}