using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel.Mechanics
{
  public class HealMoheMechanic : BaseMoheMechanics
  {
    public HealMoheMechanic(IRuntimeMoheData Mohe) : base(Mohe)
    {
      mohe = Mohe;
    }

    private IRuntimeMoheData mohe;
  }
}
