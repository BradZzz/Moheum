using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiMoheActionButton : UiBaseActionButton
  {
    private TextMeshProUGUI headerTxt;
    private TextMeshProUGUI lvlTxt;

    private Image moheImage;

    private IUiActionActive actionOutline;
    private PlayerSeat seat;

    public override bool Populate(PlayerSeat Seat, int pos)
    {
      Outline outty = GetComponent<Outline>();
      actionOutline = new UiActionActive(this, outty);

      seat = Seat;

      IPlayer contPlayer = GameData.Instance.RuntimeGame.Players.Find(player => player.Seat == seat);
      IRoster pRoster = contPlayer.Roster;

      if (pRoster.MoheRoster.Count <= pos)
        return false;

      IRuntimeMoheData moheData = pRoster.MoheRoster[pos];

      headerTxt = transform.Find("HeaderTxt").GetComponent<TextMeshProUGUI>();
      lvlTxt = transform.Find("LvlTxt").GetComponent<TextMeshProUGUI>();

      transform.Find("HealthBarImg").GetComponent<UiHealthBarFiller>().Populate(moheData);
      transform.Find("ExpBarImg").GetComponent<UiExpBarFiller>().Populate(moheData);
      moheImage = transform.Find("MohePortrait").GetComponent<Image>();

      headerTxt.text = moheData.BaseMohe.Data.Name;
      lvlTxt.text = moheData.BaseExpType.CalculateLevel(moheData.Exp).ToString();

      moheImage.sprite = moheData.BaseMohe.Data.Artwork;

      return true;
    }
  }
}
