using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel.Mechanics
{
  public class BaseMoheMechanics
  {
    protected IRuntimeMoheData Mohe { get; }

    protected BaseMoheMechanics(IRuntimeMoheData mohe)
    {
      Mohe = mohe;
    }
  }
}
