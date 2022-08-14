using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiActionActive : IUiActionActive
  {
    private Outline outline;
    private ParticleSystem particleSystem;
    private UiBaseActionButton parent;

    public Outline Outline => outline;

    public ParticleSystem ParticleSystem => particleSystem;
    public bool Active => Outline.enabled;

    public UiActionActive(UiBaseActionButton Parent, Outline Outline, ParticleSystem ParticleSystem)
    {
      parent = Parent;
      parent.OnToggle += SetActive;
      parent.AfterUseActionEffect += ActionEffectonUse;
      outline = Outline;
      particleSystem = ParticleSystem;
      particleSystem.Stop();
      outline.enabled = false;
    }

    public void SetActive(bool active)
    {
      outline.enabled = active;
    }

    IEnumerator ActionUsed()
    {
      particleSystem.Play();
      yield return new WaitForSeconds(2f);
      particleSystem.Stop();
    }

    public void ActionEffectonUse()
    {
      parent.MBehaviour.StartCoroutine(ActionUsed());
    }
  }
}
