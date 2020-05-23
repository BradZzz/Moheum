using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.UI.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiAttackCost : UiListener, IUiAttackCost
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
    private PlayerSeat seat;
    private IRuntimeAbility ability;
    private int componentIdx;

    public JewelID ID => id;
    public TextMeshProUGUI TXT => txt;
    public MonoBehaviour MBehaviour => monoBehaviour;
    public PlayerSeat Seat => seat;
    public IRuntimeAbility Ability => ability;
    public int ComponentIdx => componentIdx;

    // Listen to Ability charge update

    // Refresh UI on call

    public bool Populate(PlayerSeat Seat, IRuntimeAbility Ability, int ComponentIdx)
    {
      seat = Seat;
      ability = Ability;
      componentIdx = ComponentIdx;

      return Refresh();
    }

    public void OnPlayerUpdateRuntime()
    {
      Refresh();
    }

    bool Refresh()
    {
      if (Ability == null) return false;

      IRuntimeAbilityComponent comp = ability.AbilityComponents[componentIdx];
      id = comp.JewelType;
      List<JewelData> jewels = JewelDatabase.Instance.GetFullList();
      img.sprite = jewels.First(jewel => jewel.JewelID == id).Artwork;
      txt.text = comp.Has.ToString() + " / " + comp.Needs.ToString();
      return true;
    }
  }
}
