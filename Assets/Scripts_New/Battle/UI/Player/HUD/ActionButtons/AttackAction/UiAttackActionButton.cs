using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.Model.Game;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using TMPro;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiAttackActionButton : UiBaseActionButton
  {
    public List<IUiAttackCost> CostPanels;

    public override bool Populate(PlayerSeat seat, int pos)
    {
      CostPanels = new List<IUiAttackCost>();

      headerTxt = transform.Find("HeaderTxt").GetComponent<TextMeshProUGUI>();
      descTxt = transform.Find("DescTxt").GetComponent<TextMeshProUGUI>();

      IPlayer contPlayer = GameData.Instance.RuntimeGame.Players.Find(player => player.Seat == seat);
      IRoster pRoster = contPlayer.Roster;
      IRuntimeMoheData moheData = pRoster.CurrentMohe();

      //check to make sure the player has enough mohe for pos
      if (moheData.Abilities.Count <= pos)
        return false;

      //Populate headers and desc
      headerTxt.text = moheData.Abilities[pos].Name;
      descTxt.text = moheData.Abilities[pos].Desc;

      Transform costParent = transform.Find("Cost");

      foreach (Transform t in costParent)
      {
        Destroy(t.gameObject);
      }

      for (int i = 0; i < moheData.Abilities[pos].AbilityComponents.Count; i++)
      {
        IUiAttackCost cost = UiAtkActionCostPooler.Instance.Get(moheData.Abilities[pos].AbilityComponents[i]);
        cost.MBehaviour.transform.parent = costParent;
        cost.MBehaviour.transform.localScale = Vector3.one;
        CostPanels.Add(cost);
      }

      return true;
    }

    private TextMeshProUGUI headerTxt;
    private TextMeshProUGUI descTxt;

    public TextMeshProUGUI HeaderTxt => headerTxt;
    public TextMeshProUGUI DescTxt => descTxt;
  }
}
