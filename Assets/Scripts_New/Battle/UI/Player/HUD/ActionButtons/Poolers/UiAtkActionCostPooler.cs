using System.Collections;
using System.Collections.Generic;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiAtkActionCostPooler : PrefabPooler<UiAtkActionCostPooler>
  {
    public IUiAttackCost Get(PlayerSeat seat, IRuntimeAbility ability, int componentIdx)
    {
      var obj = Get<IUiAttackCost>();
      if (!obj.Populate(seat, ability, componentIdx))
      {
        //whoops...
        Instance.ReleasePooledObject(obj.MBehaviour.gameObject);
        return null;
      }
      return obj;
    }

    protected override void OnRelease(GameObject prefabModel)
    {
      base.OnRelease(prefabModel);
    }
  }
}