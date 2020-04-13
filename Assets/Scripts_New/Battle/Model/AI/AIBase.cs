using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Model.Game;
using Battle.Model.Game.Mechanics;
using Battle.Model.Player;
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

    public abstract SwapMechanics.RuntimeSwapData[] GetSwapMoves();


    //----------------------------------------------------------------------------------------------------------
  }
}