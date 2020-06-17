using System.Collections.Generic;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Patterns;
using UnityEngine;

namespace Battle.Model.Player.Mechanics
{
  /// <summary>
  ///     Start turn player mechanics.
  /// </summary>
  public class GainJewelBonusMechanics : BasePlayerMechanics, IListener, IGainJewelBonus
  {
    public GainJewelBonusMechanics(IPlayer Player) : base(Player)
    {
      GameEvents.Instance.AddListener(this);
      player = Player;
    }

    private IPlayer player;
    private JewelID[] BONUS_IDS = new JewelID[] { JewelID.envy, JewelID.wrath, JewelID.greed, JewelID.gluttony, JewelID.pride, JewelID.lust, JewelID.sloth };

    public void ChargeAbilities(int amt)
    {
      foreach (var jewel in BONUS_IDS)
      {
        player.Roster.CurrentMohe().PopulateAbilities(jewel, amt);
      }
    }

    public void Notify()
    {
      GameEvents.Instance.Notify<IPlayerUpdateRuntime>(i => i.OnPlayerUpdateRuntime());
    }

    public void OnGainBonusJewel(List<JewelID> jewels)
    {
      Debug.Log("OnGainBonusJewel");
      if (GameData.Instance.RuntimeGame.IsTurnInProgress && GameController.Instance.GetPlayerController(player.Seat).IsMyTurn)
      {
        ChargeAbilities(jewels.Count == 4 ? 1 : 3);
        Notify();
      }
    }
  }
}