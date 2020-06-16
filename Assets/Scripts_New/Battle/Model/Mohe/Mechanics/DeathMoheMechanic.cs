using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Controller;
using Patterns;
using UnityEngine;

namespace Battle.Model.MoheModel.Mechanics
{
  public class DeathMoheMechanic : BaseMoheMechanics, IListener, IMoheDeath
  {
    public DeathMoheMechanic(IRuntimeMoheData Mohe) : base(Mohe)
    {
      GameEvents.Instance.AddListener(this);
      mohe = Mohe;
    }

    private IRuntimeMoheData mohe;

    public void Execute()
    {
      if (mohe.MoheDead())
      {
        // Find the player that the mohe belongs to
        IRoster roster = GameController.Instance.GetPlayerController(mohe.PlayerSeat).Player.Roster;

        // If the player has no more mohe, end the game
        if (roster.AllVanquished)
        {
          Debug.Log("Move game state ended");
        } else
        {
          // If the player has more mohe, swap in an available mohe for the destroyed one
          Debug.Log("More Mohe! Continue game");
          roster.AutoRoster();
        }
      }
    }

    private void Notify()
    {
      GameEvents.Instance.Notify<IPlayerUpdateRuntime>(i => i.OnPlayerUpdateRuntime());
    }

    public void OnMoheDeath(string moheInstanceID)
    {
      if (moheInstanceID == mohe.InstanceID)
      {
        Execute();
        Notify();
      }
    }
  }
}
