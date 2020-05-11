using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IBaseAbiliity
  {
    string Name { get; }
    IAbilityEffect Effect { get; }
  }
}
