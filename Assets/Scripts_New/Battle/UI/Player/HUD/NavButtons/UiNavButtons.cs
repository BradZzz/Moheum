using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Player;
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
      seat = transform.parent.GetComponent<IUiPlayerHUD>().Seat;
      Debug.Log("Awake: " + Seat);
    }

    private List<IUiNavButton> buttons;
    private NavID current;
    private PlayerSeat seat;

    public List<IUiNavButton> Buttons => buttons;
    public NavID Current => current;
    public PlayerSeat Seat => seat;

    public Action<NavID> OnNavigate { get; set; }

    public void OnPlayerNav(PlayerSeat Seat, NavID Nav)
    {
      if (Seat == seat)
      {
        Debug.Log("OnPlayerNav: " + Seat);
        current = Nav;
        OnNavigate?.Invoke(Current);
      }
    }
  }
}
