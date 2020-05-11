using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IRuntimeMoheData
  {
    int Health { get; }
    IMohe BaseMohe { get; }
    List<IRuntimeAbility> Abilities { get; }

    bool MoheDead();
  }
}
