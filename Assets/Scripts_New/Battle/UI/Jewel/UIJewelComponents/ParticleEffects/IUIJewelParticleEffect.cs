using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public interface IUIJewelParticleEffect
  {
    void ExecuteData(IRuntimeJewel jewelData);
    void ExecuteParticleEffects(IRuntimeJewel jewelData);
  }
}
