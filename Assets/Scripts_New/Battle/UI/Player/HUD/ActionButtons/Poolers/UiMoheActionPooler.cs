using System.Collections;
using System.Collections.Generic;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiMoheActionPooler : PrefabPooler<UiMoheActionPooler>
  {
    public IUiActionButton Get(PlayerSeat seat, int idx)
    {
      var obj = Get<IUiActionButton>();
      if (!obj.Populate(seat, idx))
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