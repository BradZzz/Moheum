using System.Collections;
using System.Collections.Generic;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiAtkActionCostPooler : PrefabPooler<UiAtkActionCostPooler>
  {
    public IUiAttackCost Get(IRuntimeAbilityComponent runtimeAbility)
    {
      var obj = Get<IUiAttackCost>();
      if (!obj.Populate(runtimeAbility))
        return null;
      return obj;
    }

    protected override void OnRelease(GameObject prefabModel)
    {
      base.OnRelease(prefabModel);
    }
  }
}