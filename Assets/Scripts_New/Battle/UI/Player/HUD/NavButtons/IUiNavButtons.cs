using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using UnityEngine;

namespace Battle.UI.Player
{
  public interface IUiNavButtons : IPlayerNav
  {
    NavID Current { get; }
    List<IUiNavButton> Buttons { get; }
  }
}
