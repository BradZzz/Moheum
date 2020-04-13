using Battle.Model.Game;
using Battle.Model.Game.Mechanics;
using Battle.Model.Player;
using Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Model.AI
{
  /// <summary>
  ///     This AiConfigs will always try to do damage.
  /// </summary>
  public class AiAggressive : AiBase
  {
    public AiAggressive(IPlayer player, IPrimitiveGame game) : base(player, game)
    {
    }

    public override SwapMechanics.RuntimeSwapData[] GetSwapMoves()
    {
      return GetAllSwapMoves();
    }

    protected SwapMechanics.RuntimeSwapData[] GetAllSwapMoves()
    {
      //var playerTeam = Player.Team.Members;
      //var enemyTeam = Enemy.Team.Members;
      //var allAttacks = new List<AttackMechanics.RuntimeAttackData>();

      //foreach (var agressor in playerTeam)
      //{
      //  var possibilities = new List<AttackMechanics.RuntimeAttackData>();

      //  foreach (var defender in enemyTeam)
      //  {
      //    if (Enemy.Team.HasTaunt && !defender.Attributes.HasTaunt)
      //      continue;

      //    var attack = new AttackMechanics.RuntimeAttackData()
      //    {
      //      Agressor = agressor,
      //      Blocker = defender
      //    };

      //    possibilities.Add(attack);
      //  }

      //  allAttacks.Add(possibilities.ToList().RandomItem());
      //}
      //return allAttacks.ToArray();
      return null;
    }
  }
}