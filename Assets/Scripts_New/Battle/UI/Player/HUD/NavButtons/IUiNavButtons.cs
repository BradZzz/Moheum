using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.UI.Player
{
  public interface IUiNavButtons : IPlayerNav
  {
    NavID Current { get; }
    List<IUiNavButton> Buttons { get; }
    PlayerSeat Seat { get; }

    Action<NavID> OnNavigate { get; set; }
  }
}
