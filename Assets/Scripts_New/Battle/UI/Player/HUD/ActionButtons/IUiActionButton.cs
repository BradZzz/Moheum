using System.Collections;
using System.Collections.Generic;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.UI.Player
{
  public interface IUiActionButton
  {
    MonoBehaviour MBehaviour { get; }

    bool Populate(PlayerSeat seat, int pos);
  }
}
