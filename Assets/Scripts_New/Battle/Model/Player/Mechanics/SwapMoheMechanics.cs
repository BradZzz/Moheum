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
  public class SwapMoheMechanics : BasePlayerMechanics, IListener, IMoheSwap
  {
    public SwapMoheMechanics(IPlayer Player) : base(Player)
    {
      GameEvents.Instance.AddListener(this);
      player = Player;
    }

    private IPlayer player;

    public void OnMoheSwap(PlayerSeat seat, string moheInstanceID)
    {
      IPlayer contPlayer = GameData.Instance.RuntimeGame.Players.Find(player => player.Seat == seat);
      IRoster pRoster = contPlayer.Roster;
      for (int i = 0; i < pRoster.MoheRoster.Count; i++)
      {
        if (moheInstanceID == pRoster.MoheRoster[i].InstanceID)
        {
          pRoster.SetRoster(i);
          // End turn since mohe was swapped
          player.SwapTurn();
        }
      }
      // notify ui
      Notify();

      SetBoardState();
    }

    private void Notify()
    {
      // Update player ui
      GameEvents.Instance.Notify<IPlayerUpdateRuntime>(i => i.OnPlayerUpdateRuntime());
      // Unselect all action buttons
      GameEvents.Instance.Notify<IResetMoheActionButtons>(i => i.OnResetMoheActionButton());
    }

    private void SetBoardState()
    {
      GameEvents.Instance.Notify<ICleanBoard>(i => i.OnBoardCleanCheck());
    }
  }
}