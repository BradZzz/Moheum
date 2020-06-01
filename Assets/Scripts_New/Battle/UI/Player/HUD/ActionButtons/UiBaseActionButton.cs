using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Player;
using Battle.UI.Player;
using Battle.UI.Utils;
using UnityEngine;

public abstract class UiBaseActionButton : UiListener, IUiActionButton
{
  void Awake()
  {
    monoBehaviour = this;
  }

  private MonoBehaviour monoBehaviour;

  public MonoBehaviour MBehaviour => monoBehaviour;

  public Action<bool> OnToggle { get; set; }

  public abstract bool Populate(PlayerSeat seat, int pos);
}
