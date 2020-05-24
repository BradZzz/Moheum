using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using UnityEngine;

namespace Battle.Model.MoheModel.Mechanics
{
  public class HealMoheMechanic : BaseMoheMechanics, IMoheHeal
  {
    public HealMoheMechanic(IRuntimeMoheData Mohe) : base(Mohe)
    {
      mohe = Mohe;
    }

    private IRuntimeMoheData mohe;

    public void Execute(int heal)
    {
      mohe.Health += heal;
      if (mohe.Health > mohe.BaseMohe.Stats.health)
      {
        mohe.Health = (int) mohe.BaseMohe.Stats.health;
      }
    }

    public void OnMoheHeal(string moheInstanceID, int heal)
    {
      if (moheInstanceID == mohe.InstanceID)
      {
        Execute(heal);
        //Update UI
        GameEvents.Instance.Notify<IPlayerUpdateRuntime>(i => i.OnPlayerUpdateRuntime());
      }
    }
  }
}
