using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IAbility : IBaseAbiliity
  {
    List<IAbilityComponent> AbilityComponents { get; }
  }
}
