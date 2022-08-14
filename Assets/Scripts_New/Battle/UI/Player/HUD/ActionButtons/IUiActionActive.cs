using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public interface IUiActionActive
  {
    Outline Outline { get; }
    ParticleSystem ParticleSystem { get; }
    bool Active { get; }

    void SetActive(bool active);
    void ActionEffectonUse();
  }
}
