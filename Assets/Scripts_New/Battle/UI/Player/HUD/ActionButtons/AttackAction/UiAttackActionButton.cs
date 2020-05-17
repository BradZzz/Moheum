using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiAttackActionButton : MonoBehaviour, IUiActionButton
  {
    public void Populate(int pos)
    {
      headerTxt = transform.Find("HeaderTxt").GetComponent<TextMeshProUGUI>();
      descTxt = transform.Find("DescTxt").GetComponent<TextMeshProUGUI>();
    }

    private TextMeshProUGUI headerTxt;
    private TextMeshProUGUI descTxt;

    public TextMeshProUGUI HeaderTxt => headerTxt;
    public TextMeshProUGUI DescTxt => descTxt;
  }
}
