using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.UI.Player
{
  public interface IUiActionButton
  {
    MonoBehaviour MBehaviour { get; }

    void Populate(int pos);
  }
}
