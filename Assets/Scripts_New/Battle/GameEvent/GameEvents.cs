using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using Battle.Model.Jewel;
using Battle.Model.Player;

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

  public interface IBoardDrawJewel : ISubject
  {
    void OnDraw(IRuntimeJewel jewel, Vector2 pos);
  }

  public interface ISelectJewel : ISubject
  {
    void OnSelect(IRuntimeJewel jewel);
  }

  public interface IUnselectAll : ISubject
  {
    void OnUnselectAll ();
  }

  /*
   * Update the UI with the data from the jewel that was just selected
   */
  public interface IPostSelectJewel : ISubject
  {
    void OnPostSelect(IRuntimeJewel jewel);
  }

  public interface ICascadeJewel : ISubject
  {
    void OnJewelFall(IRuntimeJewel jewel, Vector2 pos);
  }

  /*
   * Board Checks for next state
   */
  public interface IEvaluateBoard : ISubject
  {
    void OnBoardEvaluateCheck();
  }

  ///// <summary>
  /////     Broadcast of Jewel Swap to Listeners
  ///// </summary>
  //public interface IOnSwapJewels : ISubject
  //{
  //    void IOnSwapJewels(IRuntimeJewel jewel1, IRuntimeJewel jewel2);
  //}

  ///// <summary>
  /////     Broadcast of Jewel Click to Listeners
  ///// </summary>
  //public interface IOnClickJewel : ISubject
  //{
  //    void IOnClickJewel(IRuntimeJewel jewel);
  //}

  ///// <summary>
  /////     Broadcast of Jewel Destroy to Listeners
  ///// </summary>
  //public interface IOnDestroyJewel : ISubject
  //{
  //    void IOnDestroyJewel(IRuntimeJewel jewel);
  //}

  #endregion
}
