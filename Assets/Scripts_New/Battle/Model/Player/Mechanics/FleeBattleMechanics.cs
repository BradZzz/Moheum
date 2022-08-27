using Battle.Controller;
using Battle.Controller.TurnControllers;
using Battle.GameEvent;
using Battle.Model.Player;
using Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battle.Model.Player.Mechanics
{
  /// <summary>
  ///     Finish turn player mechanics.
  /// </summary>
  public class FleeBattleMechanics : BasePlayerMechanics, IListener, ISelectFleeButton, IFleeSuccessful, IFleeFailure, IFleeBattle, IStartPlayerTurn
  {
    public FleeBattleMechanics(IPlayer Player) : base(Player)
    {
      GameEvents.Instance.AddListener(this);
      player = Player;
    }

    private IPlayer player;
    private bool waitForResponse;

    public void OnSelectFleeActionButton(PlayerSeat seat)
    {
      Debug.Log("OnSelectFleeActionButton");
      if (GameController.Instance.FleeingBattle)
        return;
      
      GameController.Instance.FleeBattle();
      if (Random.Range(0, 2) > 0)
      {
        GameEvents.Instance.Notify<IFleeSuccessful>(i => i.OnFleeSuccess(seat));
      }
      else
      {
        GameEvents.Instance.Notify<IFleeFailure>(i => i.OnFleeFailure(seat));
      }
    }

    public void OnFleeSuccess(PlayerSeat seat)
    {
      Debug.Log("Flee Success");
    }

    public void OnFleeFailure(PlayerSeat seat)
    {
      Debug.Log("Flee Failure");
      // O no! the player didn't escape. swap players and let the enemy attack again
      player.SwapTurn();
    }

    public void OnFleeBattle(PlayerSeat seat)
    {
      IPlayer winner = GameController.Instance.GetPlayerController(PlayerSeat.Left == seat ? PlayerSeat.Right : PlayerSeat.Left).Player;
      GameEvents.Instance.Notify<IFinishGame>(i => i.OnFinishGame(winner));
    }

    public void OnStartPlayerTurn(IPlayer player)
    {
      GameController.Instance.ContinueBattle();
    }
  }
}