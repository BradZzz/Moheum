using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.MoheModel;
using Patterns;
using UnityEngine;

namespace Battle.Model.Item
{
  public interface IItemData
  {
    ItemID ItemID { get; }
    string Name { get; }
    string Description { get; }
    Sprite Artwork { get; }
    List<ItemData.ItemAbilityEffects> Abilities { get; }
  }
}