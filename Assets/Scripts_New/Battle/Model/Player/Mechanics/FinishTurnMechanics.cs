using Battle.Controller;
using UnityEngine;

namespace Battle.Model.Player.Mechanics
{
  /// <summary>
  ///     Finish turn player mechanics.
  /// </summary>
  public class FinishTurnMechanics : BasePlayerMechanics
  {
    public FinishTurnMechanics(IPlayer Player) : base(Player)
    {
      player = Player;
    }

    private IPlayer player;

    public void FinishTurn()
    {
      Debug.Log("FinishTurn");
      GameController.Instance.GetPlayerController(player.Seat).PassTurn();
    }
  }
}