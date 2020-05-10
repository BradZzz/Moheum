using UnityEngine;

namespace Battle.Model.Player.Mechanics
{
  /// <summary>
  ///     Start turn player mechanics.
  /// </summary>
  public class StartTurnMechanics : BasePlayerMechanics
  {
    public StartTurnMechanics(IPlayer Player) : base(Player)
    {
      player = Player;
    }

    private IPlayer player;

    public void StartTurn()
    {
      //  var quant = Player.Configurations.Amount.LibraryPlayer.drawAmountByTurn;

      //  //draw cards
      //  for (int i = 0; i < quant; i++)
      //    Player.Draw();

      //  //replanish mana
      //  Player.ReplenishMana();

      //  var members = Player.Team.Members.ToArray();
      //  //start turn for all members
      //  foreach (var mem in members)
      //    mem.StartPlayerTurn();
      Debug.Log("StartTurn");
      player.HasSwapped = false;
    }
  }
}