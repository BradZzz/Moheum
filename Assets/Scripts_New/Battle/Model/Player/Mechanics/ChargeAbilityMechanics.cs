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
  public class ChargeAbilityMechanics : BasePlayerMechanics, IListener, IRemoveJewel
  {
    public ChargeAbilityMechanics(IPlayer Player) : base(Player)
    {
      GameEvents.Instance.AddListener(this);
      player = Player;
    }

    private IPlayer player;

    public void OnRemoveJewel(IRuntimeJewel jewel)
    {
      if (GameController.Instance.GetPlayerController(player.Seat).IsMyTurn)
      {
        ChargeAbility(jewel.Data.JewelID);
        Notify();
      }
    }

    public void ChargeAbility(JewelID jewel)
    {
      player.Roster.CurrentMohe().PopulateAbilities(jewel, 1);
    }

    public void Notify()
    {
      GameEvents.Instance.Notify<IPlayerUpdateRuntime>(i => i.OnPlayerUpdateRuntime());
    }
  }
}