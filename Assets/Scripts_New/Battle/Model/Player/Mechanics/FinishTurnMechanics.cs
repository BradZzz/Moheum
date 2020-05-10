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
      //var isDiscard = Player.Configurations.Amount.LibraryPlayer.isDiscardableHands;
      //if (isDiscard)
      //  DiscardAll();

      //var members = Player.Team.Members.ToArray();
      ////finish turn for all members
      //foreach (var mem in members)
      //  mem.FinishPlayerTurn();
      Debug.Log("FinishTurn");
      GameController.Instance.GetPlayerController(player.Seat).PassTurn();
    }

    //private void DiscardAll()
    //{
    //  var quant = Player.Hand.Size;
    //  for (var i = 0; i < quant; i++)
    //    Player.Discard(Player.Hand.Units[0]);
    //}
  }
}