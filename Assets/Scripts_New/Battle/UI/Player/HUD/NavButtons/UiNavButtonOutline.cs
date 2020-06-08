using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Player;
using Battle.UI.Player;
using Battle.UI.Utils;
using UnityEngine;
using UnityEngine.UI;

public class UiNavButtonOutline : UiListener, IUiNavButtonOutline, IPlayerNav
{
    void Awake()
    {
      parent = GetComponent<IUiNavButton>();
      outline = GetComponent<Outline>();
      outline.enabled = false;
    }

    public void OnPlayerNav(PlayerSeat seat, NavID nav)
    {
      if (seat != parent.Seat)
        return;

      if (nav == parent.NavID) {
        outline.enabled = true;
      } else {
        outline.enabled = false;
      }
    }

    private Outline outline;

    private IUiNavButton parent;
    public IUiNavButton Parent => parent;
}
