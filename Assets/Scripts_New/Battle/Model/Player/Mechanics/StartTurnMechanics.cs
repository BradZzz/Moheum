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
      Debug.Log("StartTurn");
      player.HasSwapped = false;
    }
  }
}