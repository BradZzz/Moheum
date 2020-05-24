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
  ///     Base for all the Artificial Intelligence of the game.
  /// </summary>
  public abstract class AiBase
  {
    //----------------------------------------------------------------------------------------------------------

    #region Constructor

    protected AiBase(IPlayer player, IPrimitiveGame game)
    {
      Game = game;
      Player = player;
      Enemy = game.TurnLogic.GetOpponent(player);
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Properties

    protected IPrimitiveGame Game { get; }
    protected IPlayer Player { get; }
    protected IPlayer Enemy { get; }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    public abstract List<SwapChoices> GetSwapMoves(PlayerSeat seat);
    public abstract List<IRuntimeAbility> GetAbilityMoves(PlayerSeat seat);
    public abstract List<IRuntimeJewel> GetAbilityJewel(PlayerSeat seat, IRuntimeAbility ability);

    //----------------------------------------------------------------------------------------------------------
  }
}