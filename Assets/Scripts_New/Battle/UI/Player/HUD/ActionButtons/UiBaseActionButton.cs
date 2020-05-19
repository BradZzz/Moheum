using System.Collections;
using System.Collections.Generic;
using Battle.Model.Player;
using Battle.UI.Player;
using UnityEngine;

public abstract class UiBaseActionButton : MonoBehaviour, IUiActionButton
{
  void Awake()
  {
    monoBehaviour = this;
  }

  private MonoBehaviour monoBehaviour;

  public MonoBehaviour MBehaviour => monoBehaviour;

  public abstract bool Populate(PlayerSeat seat, int pos);
}
