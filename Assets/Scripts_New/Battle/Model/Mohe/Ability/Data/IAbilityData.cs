using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IAbilityData
  {
    AbilityID AbilityID { get; }
    string AbilityName { get; }
    string Description { get; }

    JewelID TriggerJewel { get; }
    List<BaseEffect> AfterEffects { get; }
  }
}
