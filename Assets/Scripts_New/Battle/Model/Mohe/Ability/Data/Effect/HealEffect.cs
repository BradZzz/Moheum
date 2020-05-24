using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  [CreateAssetMenu(menuName = "Effect/Heal")]
  public class HealEffect : BaseEffect
  {
    public override bool Execute(IRuntimeJewel TriggerJewel)
    {
      //heal
      IRuntimeMoheData mohe = GameController.Instance.GetPlayerController(EffectPlayer).Player.Roster.CurrentMohe();
      GameEvents.Instance.Notify<IMoheHeal>(i => i.OnMoheHeal( mohe.InstanceID, (int) Random.Range(MinAmt, MaxAmt)));

      return true;
    }
  }
}
