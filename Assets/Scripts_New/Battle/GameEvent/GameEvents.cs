using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using Battle.Model.Jewel;
using Battle.Model.Player;
using Battle.UI.Player;
using Battle.Model.MoheModel;

namespace Battle.GameEvent
{
  public class GameEvents : Observer<GameEvents>
  {
      protected override void OnAwake()
      {
          Logger.Log<GameEvents>("Awake");
          Debug.Log("Awake");
      }

      private void Start()
      {
          Logger.Log<GameEvents>("Start");
          Debug.Log("Start");
      }
  }

  #region Game Events Declaration

  /// <summary>
  ///     Broadcast of the players right before the game start.
  /// </summary>
  public interface IPreGameStart : ISubject
  {
    void OnPreGameStart(List<IPlayer> players);
  }

  /// <summary>
  ///     Broadcast of the starter player to the Listeners.
  /// </summary>
  public interface IStartGame : ISubject
  {
    void OnStartGame(IPlayer starter);
  }

  /// <summary>
  ///     Broadcast of the winner after a game is finished to the Listeners.
  /// </summary>
  public interface IFinishGame : ISubject
  {
    void OnFinishGame(IPlayer winner);
  }

  /// <summary>
  ///     Broadcast of restart game.
  /// </summary>
  public interface IRestartGame : ISubject
  {
      void OnRestart();
  }

  /// <summary>
  ///     Broadcast of a player when it starts the turn to the Listeners.
  /// </summary>
  public interface IStartPlayerTurn : ISubject
  {
    void OnStartPlayerTurn(IPlayer player);
  }

  /// <summary>
  ///     Broadcast of a player when it finishes the turn to the Listeners.
  /// </summary>
  public interface IFinishPlayerTurn : ISubject
  {
      void OnFinishPlayerTurn(IPlayer player);
  }

  /// <summary>
  ///     Broadcast of the time to the Listeners.
  /// </summary>
  public interface IDoTick : ISubject
  {
    void OnTickTime(int time, IPlayer player);
  }

  /*
   * Jewel Events
   */
  public interface ISelectJewel : ISubject
  {
    void OnSelect(IRuntimeJewel jewel);
  }

  public interface IPostSelectJewel : ISubject
  {
    void OnPostSelect(IRuntimeJewel jewel);
  }

  public interface IRemoveJewel : ISubject
  {
    void OnRemoveJewel(IRuntimeJewel jewel);
  }

  public interface ITransformJewel : ISubject
  {
    void OnTransformJewel(IRuntimeJewel jewel, JewelID transformType);
  }

  public interface IPostTransformJewel : ISubject
  {
    void OnPostTransformJewel(IRuntimeJewel jewel);
  }

  public interface ICascadeJewel : ISubject
  {
    void OnJewelFall(IRuntimeJewel jewel);
  }

  public interface ISwapJewel : ISubject
  {
    void OnJewelSwap(IRuntimeJewel jewel, IRuntimeJewel jewel2);
  }

  public interface IPositionJewel : ISubject
  {
    void OnJewelPosition(IRuntimeJewel jewel, Vector3 from, Vector3 to);
  }

  /*
   * Board Mechanics
   */

  public interface IActionBoard : ISubject
  {
    void OnBoardActionCheck(PlayerSeat seat, IRuntimeAbility ability);
  }

  public interface IInvokeActionBoard : ISubject
  {
    void OnInvokeBoardActionCheck(IRuntimeJewel jewel);
  }

  public interface IUnselectAll : ISubject
  {
    void OnUnselectAll();
  }

  /*
   * Board States
   */
  public interface ICascadeBoard : ISubject
  {
    void OnBoardCascadeCheck();
  }

  public interface ICleanBoard : ISubject
  {
    void OnBoardCleanCheck();
  }

  public interface IEvaluateBoard : ISubject
  {
    void OnBoardEvaluateCheck();
  }

  public interface IPreActionBoard : ISubject
  {
    void OnPreActionCheck();
  }

  public interface IPostActionBoard : ISubject
  {
    void OnPostActionCheck();
  }

  public interface IRemoveSelectedBoard : ISubject
  {
    void OnBoardRemoveSelectedCheck();
  }

  public interface IResetBoard : ISubject
  {
    void OnBoardResetCheck();
  }

  public interface ISelectedBoard : ISubject
  {
    void OnBoardSelectedCheck();
  }

  public interface ISwapBoard : ISubject
  {
    void OnBoardSwapCheck();
  }

  /*
   * Mohe Mechanics
   */

  public interface IMoheSwap : ISubject
  {
    void OnMoheSwap(PlayerSeat seat, string moheInstanceID);
  }

  public interface IMoheTakeDamage : ISubject
  {
    void OnMoheTakeDamage(string moheInstanceID, int dmg);
  }

  public interface IMoheDeath : ISubject
  {
    void OnMoheDeath(string moheInstanceID);
  }

  public interface IMoheHeal : ISubject
  {
    void OnMoheHeal(string moheInstanceID, int heal);
  }

  /*
   * Player UI Nav
   */

  public interface IPlayerNav : ISubject
  {
    void OnPlayerNav(PlayerSeat seat, NavID nav);
  }

  public interface IPlayerUpdateRuntime : ISubject
  {
    void OnPlayerUpdateRuntime();
  }

  /*
   * Action Buttons
   */

  // Attack
  public interface ISelectAtkActionButton : ISubject
  {
    void OnSelectAtkActionButton(PlayerSeat seat, AbilityID id);
  }

  public interface IResetAtkActionButtons : ISubject
  {
    void OnResetAtkActionButton();
  }

  // Mohe
  public interface ISelectMoheActionButton : ISubject
  {
    void OnSelectMoheActionButton(PlayerSeat seat, string instanceId);
  }

  public interface IResetMoheActionButtons : ISubject
  {
    void OnResetMoheActionButton();
  }

  #endregion
}
