using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  [CreateAssetMenu(menuName = "Effect/Heal")]
  public class HealEffect : BaseEffect
  {
    public override bool Execute(IRuntimeJewel TriggerJewel)
    {
      PlayerSeat pSeat = EffectPlayer == PlayerEffectSeat.Active ? GameData.Instance.RuntimeGame.TurnLogic.CurrentPlayer.Seat : GameData.Instance.RuntimeGame.TurnLogic.NextPlayer.Seat;
      IRuntimeMoheData mohe = GameController.Instance.GetPlayerController(pSeat).Player.Roster.CurrentMohe();
      GameEvents.Instance.Notify<IMoheHeal>(i => i.OnMoheHeal( mohe.InstanceID, (int) Random.Range(MinAmt, MaxAmt)));

      return true;
    }
  }
}
