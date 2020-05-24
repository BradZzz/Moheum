using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Player;
using Patterns;
using UnityEngine;

namespace Battle.Model.MoheModel.Mechanics
{
  public class DamageMoheMechanic : BaseMoheMechanics, IListener, IMoheTakeDamage
  {
    public DamageMoheMechanic(IRuntimeMoheData Mohe) : base(Mohe)
    {
      GameEvents.Instance.AddListener(this);
      mohe = Mohe;
    }

    private IRuntimeMoheData mohe;

    public void Execute(int dmg)
    {
      mohe.Health -= dmg;
      if (mohe.Health < 0)
      {
        mohe.Health = 0;
      }
    }

    public void OnMoheTakeDamage(string moheInstanceID, int dmg)
    {
      if (moheInstanceID == mohe.InstanceID)
      {
        Debug.Log("Mohe damaged: " + moheInstanceID + " : " + mohe.BaseMohe.Data.MoheID);
        Execute(dmg);
        //Update UI
        GameEvents.Instance.Notify<IPlayerUpdateRuntime>(i => i.OnPlayerUpdateRuntime());
      }
    }
  }
}
