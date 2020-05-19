using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiAttackCost : MonoBehaviour, IUiAttackCost
  {
    void Awake()
    {
      monoBehaviour = this;
      txt = GetComponentInChildren<TextMeshProUGUI>();
      img = transform.Find("Jewel").GetComponent<Image>();
    }

    private JewelID id;
    private TextMeshProUGUI txt;
    private Image img;
    private MonoBehaviour monoBehaviour;

    public JewelID ID => id;
    public TextMeshProUGUI TXT => txt;
    public MonoBehaviour MBehaviour => monoBehaviour;

    public bool Populate(IRuntimeAbilityComponent ablComp)
    {
      id = ablComp.JewelType;
      List<JewelData> jewels = JewelDatabase.Instance.GetFullList();
      img.sprite = jewels.First(jewel => jewel.JewelID == id).Artwork;
      txt.text = ablComp.Has.ToString() + " / " + ablComp.Needs.ToString();
      return true;
    }
  }
}
