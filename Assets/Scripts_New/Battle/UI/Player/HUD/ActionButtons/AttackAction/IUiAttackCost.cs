using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using TMPro;
using UnityEngine;

namespace Battle.UI.Player
{
  public interface IUiAttackCost
  {
    JewelID ID { get; }
    TextMeshProUGUI TXT { get; }

    void Execute(int idx);
  }
}
