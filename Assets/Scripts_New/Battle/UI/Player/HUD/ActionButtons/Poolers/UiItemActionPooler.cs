﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiItemActionPooler : PrefabPooler<UiItemActionPooler>
  {
    public IUiActionButton Get(int idx)
    {
      var obj = Get<IUiActionButton>();
      obj.Populate(idx);
      return obj;
    }

    protected override void OnRelease(GameObject prefabModel)
    {
      base.OnRelease(prefabModel);
    }
  }
}