using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using TMPro;
using UnityEngine;

namespace Battle.UI.Player
{
  public interface IUiAttackCost
  {
    MonoBehaviour MBehaviour { get; }

    JewelID ID { get; }
    TextMeshProUGUI TXT { get; }

    bool Populate(IRuntimeAbilityComponent idx);
  }
}
