using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.Player;
using Battle.UI.Jewel;
using Battle.UI.Utils;
using UnityEngine;

namespace Battle.UI.Board
{
  //------------------------------------------------------------------------------------------------------------------

  /// <summary>
  ///     Pile of jewels. Add or Remove jewels and be notified when changes happen.
  /// </summary>
  public abstract class UIGemPile : UiListener, IUiGemPile, IRestartGame, IFinishGame
  {
    //--------------------------------------------------------------------------------------------------------------

    #region Unitycallbacks

    protected virtual void Awake()
    {
      //initialize register
      Jewels = new List<IUiJewel>();

      Restart();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Properties

    /// <summary>
    ///     List with all cards.
    /// </summary>
    public List<IUiJewel> Jewels { get; private set; }

    public Action<IUiJewel[]> OnPileChanged { get; set; } = (jewel) => { };


    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Operations

    /// <summary>
    ///     Add a card to the pile.
    /// </summary>
    /// <param name="card"></param>
    public virtual void AddJewel(IUiJewel jewel)
    {
      if (jewel == null)
        throw new ArgumentNullException("Null is not a valid argument.");

      Jewels.Add(jewel);
      jewel.transform.SetParent(transform);
      //Debug.Log("AddJewel");
      //jewel.Renderer.sprite = jewel.Data.Artwork;
      jewel.Initialize();
      NotifyPileChange();
      jewel.Draw();
    }


    /// <summary>
    ///     Remove a card from the pile.
    /// </summary>
    /// <param name="card"></param>
    public virtual void RemoveJewel(IUiJewel jewel)
    {
      if (jewel == null)
        throw new ArgumentNullException("Null is not a valid argument.");

      Jewels.Remove(jewel);

      NotifyPileChange();
    }

    /// <summary>
    ///     Clear all the pile.
    /// </summary>
    [Button]
    public virtual void Restart()
    {
      var childJewels = GetComponentsInChildren<IUiJewel>();
      foreach (var uiJewelPile in childJewels)
        UiJewelPool.Instance.ReleasePooledObject(uiJewelPile.gameObject);

      Jewels.Clear();
    }

    /// <summary>
    ///     Notify all listeners of this pile that some change has been made.
    /// </summary>
    [Button]
    public void NotifyPileChange()
    {
      OnPileChanged?.Invoke(Jewels.ToArray());
    }

    public void OnRestart()
    {
      Restart();
    }

    public void OnFinishGame(IPlayer winner)
    {
      Restart();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------
  }
}