using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Player;
using Battle.UI.Utils;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiActionButtons : UiListener, IUiActionButtons, IPlayerUpdateRuntime
  {
    void Awake()
    {
      actionButtons = new List<IUiActionButton>();
      playerHUD = GetComponentInParent<IUiPlayerHUD>();

      foreach (Transform t in transform)
      {
        Destroy(t.gameObject);
      }

      navButtons = transform.parent.GetComponentInChildren<IUiNavButtons>();
      currentNav = NavID.None;
      navButtons.OnNavigate += NavigateActions;
    }

    private void Start()
    {
      base.Start();
      Debug.Log("UiActionButtons Start");
      Debug.Log(navButtons.OnNavigate);
      NavigateActions(navButtons.Current);
    }

    private IUiNavButtons navButtons;
    private IUiPlayerHUD playerHUD;
    private List<IUiActionButton> actionButtons;
    private NavID currentNav;

    public IUiNavButtons NavButtons => navButtons;
    public IUiPlayerHUD PlayerHUD => playerHUD;
    public List<IUiActionButton> ActionButtons => actionButtons;

    public void OnPlayerUpdateRuntime()
    {
      NavigateActions(navButtons.Current);
    }

    public void NavigateActions(NavID nav)
    {
      if (nav == currentNav)
        return;

      currentNav = nav;
      PlayerSeat seat = playerHUD.Seat;
      // Return the current buttons to the pool
      int transformChildren = transform.childCount;
      for(int i = 0; i < transformChildren; i++)
      {
        Transform t = transform.GetChild(0);
        if (UiAtkActionPooler.Instance.CheckPooledObject(t.gameObject))
        {
          UiAtkActionPooler.Instance.ReleasePooledObject(t.gameObject);
        } else if (UiItemActionPooler.Instance.CheckPooledObject(t.gameObject))
        {
          UiItemActionPooler.Instance.ReleasePooledObject(t.gameObject);
        } else if (UiMoheActionPooler.Instance.CheckPooledObject(t.gameObject))
        {
          UiMoheActionPooler.Instance.ReleasePooledObject(t.gameObject);
        } else if (UiFleeActionPooler.Instance.CheckPooledObject(t.gameObject))
        {
          UiFleeActionPooler.Instance.ReleasePooledObject(t.gameObject);
        }
        else
        {
          Destroy(t.gameObject);
        }
      }

      for (int i = 0; i < 4; i++)
      {
        IUiActionButton action;
        switch (nav)
        {
          case NavID.Attack:
            action = UiAtkActionPooler.Instance.Get(seat, i);
            break;
          case NavID.Item:
            action = UiItemActionPooler.Instance.Get(seat, i);
            break;
          case NavID.Mohe:
            action = UiMoheActionPooler.Instance.Get(seat, i);
            break;
          case NavID.Run:
            action = UiFleeActionPooler.Instance.Get(seat, i);
            break;
          default:
            action = null;
            break;
        }
        if (action == null) {
          break;
        }
        action.MBehaviour.transform.SetParent(transform);
        action.MBehaviour.transform.localScale = Vector3.one;
        ActionButtons.Add(action);
      }
    }
  }
}
