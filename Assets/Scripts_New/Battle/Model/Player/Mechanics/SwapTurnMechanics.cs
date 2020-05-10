using UnityEngine;

namespace Battle.Model.Player.Mechanics
{
  /// <summary>
  ///     Start turn player mechanics.
  /// </summary>
  public class SwapTurnMechanics : BasePlayerMechanics
  {
    public SwapTurnMechanics(IPlayer Player) : base(Player)
    {
      player = Player;
    }

    private IPlayer player;

    public void SwappedOnTurn()
    {
      player.HasSwapped = true;
    }
  }
}