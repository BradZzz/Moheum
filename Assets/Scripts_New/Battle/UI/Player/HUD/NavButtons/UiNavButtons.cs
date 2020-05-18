using System;
using System.Collections;
using System.Collections.Generic;
using Battle.UI.Utils;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiNavButtons : UiListener, IUiNavButtons
  {
    void Awake()
    {
      current = NavID.Attack;
      buttons = new List<IUiNavButton>();
      for (int i = 0; i< transform.childCount; i++)
      {
        buttons.Add(transform.GetChild(i).GetComponent<IUiNavButton>());
      }
    }

    private List<IUiNavButton> buttons;
    private NavID current;

    public List<IUiNavButton> Buttons => buttons;
    public NavID Current => current;
    public Action<NavID> OnNavigate { get; set; }

    public void OnPlayerNav(NavID nav)
    {
      current = nav;
      Debug.Log(OnNavigate);
      Debug.Log(nav);
      if (OnNavigate != null)
        OnNavigate.Invoke(Current);
    }
  }
}
