using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Model.Game;
using Battle.Model.Game.Mechanics;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Utils;
using Extensions;
using UnityEngine;

namespace Battle.Model.AI
{
  /// <summary>
  ///     This class holds ai submodules that interact with
  ///     a game and player to accomplish its own goals
  /// </summary>
  public class AIModule
  {
    //----------------------------------------------------------------------------------------------------------

    #region Constructor

    public AIModule(IPlayer player, IPrimitiveGame game)
    {
      //add all submodules
      subModules.Add(AiArchetype.Aggressive, GetAi(AiArchetype.Aggressive, player, game));

      //define current ai randomly
      CurrentAi = subModules.Keys.ToList().RandomItem();
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Factory

    /// <summary>
    ///     Small factory to create sub ai modules.
    /// </summary>
    /// <param name="archetype"></param>
    /// <param name="player"></param>
    /// <param name="game"></param>
    /// <returns></returns>
    private static AiBase GetAi(AiArchetype archetype, IPlayer player, IPrimitiveGame game)
    {
      switch (archetype)
      {
        case AiArchetype.Aggressive: return new AiAggressive(player, game);
        default:
          throw new ArgumentOutOfRangeException(nameof(archetype), archetype, null);
      }
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Properties and Fields

    /// <summary>
    ///     Register with all the AiConfigs submodules.
    /// </summary>
    private readonly Dictionary<AiArchetype, AiBase> subModules = new Dictionary<AiArchetype, AiBase>();

    /// <summary>
    ///     AiConfigs that is current operating.
    /// </summary>
    private AiArchetype CurrentAi { get; set; }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Operations

    /// <summary>
    ///     Returns the best move according to the current ai submodule.
    /// </summary>
    /// <returns></returns>
    public List<SwapChoices> GetBestMove(PlayerSeat seat)
    {
      if (!subModules.ContainsKey(CurrentAi))
        throw new ArgumentOutOfRangeException(
            CurrentAi + " is not registered as a valid archetype in this module.");

      return subModules[CurrentAi].GetSwapMoves(seat);
    }

    /// <summary>
    ///     Returns the best ability according to the current ai submodule.
    /// </summary>
    /// <returns></returns>
    public List<IRuntimeAbility> GetBestAbility(PlayerSeat seat)
    {
      if (!subModules.ContainsKey(CurrentAi))
        throw new ArgumentOutOfRangeException(
            CurrentAi + " is not registered as a valid archetype in this module.");

      return subModules[CurrentAi].GetAbilityMoves(seat);
    }

    /// <summary>
    ///     Returns the best ability according to the current ai submodule.
    /// </summary>
    /// <returns></returns>
    public List<IRuntimeJewel> GetAbilityJewels(PlayerSeat seat, IRuntimeAbility ability)
    {
      if (!subModules.ContainsKey(CurrentAi))
        throw new ArgumentOutOfRangeException(
            CurrentAi + " is not registered as a valid archetype in this module.");

      return subModules[CurrentAi].GetAbilityJewel(seat, ability);
    }

    /// <summary>
    ///     Change the current archetype.
    /// </summary>
    /// <param name="archetype"></param>
    public void SwapAiToArchetype(AiArchetype archetype)
    {
      CurrentAi = archetype;
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------
  }
}
