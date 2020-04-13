using System;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.Game.Mechanics
{
  /// <summary>
  ///     Attack Logic Implementation
  /// </summary>
  public class SwapMechanics : BaseGameMechanics
  {
    //public struct UiAttackInfo
    //{
    //  public int Amount { get; set; }
    //  public bool IsLethal { get; set; }
    //  public int CurrentHealthTarget { get; set; }
    //  public int CurrentHealthSource { get; set; }
    //  public IDamager Target { get; set; }
    //  public IDamageable Source { get; set; }
    //}

    public struct RuntimeSwapData
    {
      public IRuntimeJewel Swapper { get; set; }
      public IRuntimeJewel Swappee { get; set; }
    }

    public SwapMechanics(IPrimitiveGame game) : base(game)
    {

    }

    /// <summary>
    ///     Execution of the damage logic.
    /// </summary>
    public void Execute(RuntimeSwapData data)
    {
      //if (!Game.IsTurnInProgress)
      //  return;

      //if (!Game.IsGameStarted)
      //  return;

      //if (Game.IsGameFinished)
      //  return;

      ////get attacker
      //var attackerPlayer = Game.TurnLogic.GetPlayer(data.Agressor.Attributes.Owner.Seat);

      //if (!Game.TurnLogic.IsMyTurn(attackerPlayer))
      //  return;

      //if (HandleTaunt(data))
      //  return;

      //HandleDamage(data);
      //HandleRetaliation(data);
    }

    bool HandleSwap(RuntimeSwapData data)
    {
      //var blocker = data.Blocker;
      //if (blocker.HasAegis)
      //{
      //  blocker.RemoveAegis();
      //  return true;
      //}

      return false;
    }
  }
}