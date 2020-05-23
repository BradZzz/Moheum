using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiNavBClick : MonoBehaviour, IUiNavBClick
  {
    void Awake()
    {
      parent = GetComponent<IUiNavButton>();
    }

    public void OnMouseDown()
    {
      GameEvents.Instance.Notify<IPlayerNav>(i => i.OnPlayerNav(parent.Seat, parent.NavID));
    }

    private IUiNavButton parent;

    public IUiNavButton Parent => parent;
  }
}
