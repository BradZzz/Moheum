using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.MoheModel.ExpTypes;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Model.Item
{
  public interface IRuntimeItemData
  {
    IItem Item { get; }
    int Quantity { get;  }
    PlayerSeat PlayerSeat { get; }
    
    bool UsableItem();
  }
}
