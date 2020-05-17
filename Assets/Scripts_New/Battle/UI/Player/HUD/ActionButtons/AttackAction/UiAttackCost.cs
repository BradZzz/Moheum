using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Model.Jewel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiAttackCost : MonoBehaviour, IUiAttackCost
  {
    void Awake()
    {
      txt = GetComponent<TextMeshProUGUI>();
      img = GetComponent<Image>();
    }

    private JewelID id;
    private TextMeshProUGUI txt;
    private Image img;

    public JewelID ID => id;
    public TextMeshProUGUI TXT => txt;

    public void Execute(int idx)
    {
      id = JewelID.envy;
      List<JewelData> jewels = JewelDatabase.Instance.GetFullList();
      img.sprite = jewels.First(jewel => jewel.JewelID == id).Artwork;
      txt.text = "2 / 3";
    }
  }
}
