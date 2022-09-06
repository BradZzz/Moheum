using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Game;
using Battle.Model.Game.Mechanics;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Controller;
using Battle.Model.RuntimeBoard.Utils;
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
    public AiAggressive(IPlayer Player, IPrimitiveGame Game) : base(Player, Game)
    {
      player = Player;
      game = Game;
    }

    private IPlayer player;
    private IPrimitiveGame game;

    private IPlayer Player => player;
    private IPrimitiveGame Game => game;

    public override List<SwapChoices> GetSwapMoves(PlayerSeat seat)
    {
      List<JewelID> prefJewels = new List<JewelID>() { JewelID.wrath };

      // Look through all of the current mohe's abilities
      IRuntimeMoheData mohe = GameController.Instance.GetPlayerController(seat).Player.Roster.CurrentMohe();

      // If there are any abilities that are uncharged, add to abilities buffer
      foreach (var abl in mohe.Abilities)
      {
        foreach (var comp in abl.AbilityComponents)
        {
          if (comp.Has < comp.Needs && !prefJewels.Contains(comp.JewelType))
          {
            prefJewels.Add(comp.JewelType);
          }
        }
      }

      // look for matches
      List<SwapChoices> matchesBuff = FindMatchesUtil.FindBestMatches(game.GameBoard.GetBoardData().GetMap(), prefJewels);

      return matchesBuff.OrderByDescending(m => m.matches).ToList();
    }

    public override List<IRuntimeAbility> GetAbilityMoves(PlayerSeat seat)
    {
      List<IRuntimeAbility> abilityChoices = new List<IRuntimeAbility>();

      // Look through all of the current mohe's abilities
      IRuntimeMoheData mohe = GameController.Instance.GetPlayerController(seat).Player.Roster.CurrentMohe();

      // If there are any abilities that are uncharged, add to abilities buffer
      foreach (var abl in mohe.Abilities)
      {
        if (abl.AbilityCharged() && GetAbilityJewel(seat, abl).Count() > 0)
        {
          abilityChoices.Add(abl);
        }
      }
      return abilityChoices;
    }

    public override List<IRuntimeJewel> GetAbilityJewel(PlayerSeat seat, IRuntimeAbility ability)
    {
      List<IRuntimeJewel> abilityJewels = new List<IRuntimeJewel>();
      IRuntimeJewel[,] jwlMap = game.GameBoard.GetBoardData().GetMap();
      int width = jwlMap.GetLength(0);
      int height = jwlMap.GetLength(1);

      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height; y++)
        {
          List<JewelID> effectJewels = new List<JewelID>();
          foreach (AbilityData.AbilityCostData abilityCost in ability.Ability.abilityCost)
          {
            effectJewels.Add(abilityCost.jewel);
          }
          
          //Which gem the computer prioritizes swapping
          // Gems that activate abilities + any wrath gems
          if (effectJewels.Contains(jwlMap[x,y].Data.JewelID) || effectJewels.Contains(JewelID.any) || jwlMap[x,y].Data.JewelID == JewelID.wrath)
            abilityJewels.Add(jwlMap[x, y]);
        }
      }

      return abilityJewels;
    }
  }
}