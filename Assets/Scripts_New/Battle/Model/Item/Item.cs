using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.MoheModel;
using UnityEngine;

namespace Battle.Model.Item
{
  public class Item : IItem
  {
    public Item (IItemData Data)
    {
      data = Data;
    }

    public IItemData Data => data;
    private IItemData data;
  }
}
