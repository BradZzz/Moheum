using System.Collections.Generic;
using System.Linq;
using Battle.Model.Item;
using Patterns;
using UnityEngine;

namespace Battle.Model.Item
{
  public class ItemDatabase : Singleton<ItemDatabase>
  {
    private const string PathDataBase = "Battle/Item";
    private List<ItemData> Items { get; }

    public ItemDatabase()
    {
      Items = Resources.LoadAll<ItemData>(PathDataBase).ToList();
    }

    public ItemData Get(ItemID id)
    {
      return Items?.Find(item => item.ItemID == id);
    }

    public List<ItemData> GetFullList()
    {
      return Items;
    }
  }
}