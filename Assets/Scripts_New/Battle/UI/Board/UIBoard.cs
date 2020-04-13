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
  public class UIBoard : UiListener, IUiBoard, IRestartGame, IStartGame
  {
    public void OnRestart()
    {
      throw new System.NotImplementedException();
    }

    protected virtual void Awake()
    {
      //initialize register
      Jewels = new List<IUiJewel>();

      Restart();
    }

    /// <summary>
    ///     List with all jewels.
    /// </summary>
    public List<IUiJewel> Jewels { get; private set; }
    public Action<IUiJewel> OnJewelPlayed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action<IUiJewel> OnJewelSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action<IUiJewel> OnJewelDiscarded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    /// <summary>
    ///     Clear all the jewels.
    /// </summary>
    [Button]
    public virtual void Restart()
    {
      var childJewels = GetComponentsInChildren<IUiJewel>();
      foreach (var uiCJewel in childJewels)
        UiJewelPool.Instance.ReleasePooledObject(uiCJewel.gameObject);

      Jewels.Clear();
    }

    public void SelectJewel(IUiJewel jewel)
    {
      throw new System.NotImplementedException();
    }

    public void SwapSelected()
    {
      throw new NotImplementedException();
    }

    public void Unselect()
    {
      throw new NotImplementedException();
    }

    public void UnselectJewel(IUiJewel jewel)
    {
      throw new NotImplementedException();
    }

    public void EnableJewels()
    {
      throw new NotImplementedException();
    }

    public void DisableJewels()
    {
      throw new NotImplementedException();
    }

    public IUiJewel GetJewel(IRuntimeJewel jewel)
    {
      throw new NotImplementedException();
    }

    public void OnStartGame(IPlayer starter)
    {
      Debug.Log("Populate Jewels Here!");
    }
  }
}
