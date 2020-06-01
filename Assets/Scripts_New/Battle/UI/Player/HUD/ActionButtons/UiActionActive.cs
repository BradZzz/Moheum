using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiActionActive : IUiActionActive
  {
    private Outline outline;

    public Outline Outline => outline;
    public bool Active => Outline.enabled;

    public UiActionActive(UiBaseActionButton parent, Outline Outline)
    {
      outline = Outline;
      outline.enabled = false;
      parent.OnToggle += SetActive;
    }

    public void SetActive(bool active)
    {
      outline.enabled = active;
    }
  }
}
