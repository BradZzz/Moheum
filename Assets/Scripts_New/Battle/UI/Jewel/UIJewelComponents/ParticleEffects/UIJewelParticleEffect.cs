using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.UI.Utils;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  [RequireComponent(typeof(ParticleSystem))]
  public class UIJewelParticleEffect : UiListener, IUIJewelParticleEffect
  {
    private void Awake()
    {
      _particleSystem = GetComponent<ParticleSystem>();
      _particleSystem.Stop();
    }

    IRuntimeJewel jewel;
    private ParticleSystem _particleSystem;

    public void ExecuteData(IRuntimeJewel jewelData)
    {
      jewel = jewelData;
    }
    
    IEnumerator DeathParticleEffect()
    {
      _particleSystem.Play();
      yield return new WaitForSeconds(.5f);
      _particleSystem.Stop();
      GameEvents.Instance.Notify<IRemoveJewel>(i => i.OnRemoveJewel(jewel));
    }

    public void ExecuteParticleEffects(IRuntimeJewel jewelData)
    {
      if (jewelData == jewel)
      {
        StartCoroutine(DeathParticleEffect());
      }
    }
  }
}
