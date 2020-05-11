using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IAbilityComponent
  {
    JewelID JewelType { get; }
    int Needs { get; }
  }
}
