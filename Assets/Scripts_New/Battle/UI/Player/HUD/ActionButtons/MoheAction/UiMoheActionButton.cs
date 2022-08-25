using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiMoheActionButton : UiBaseActionButton, ISelectMoheActionButton, IResetMoheActionButtons
  {
    private TextMeshProUGUI headerTxt;
    private TextMeshProUGUI lvlTxt;

    private Image moheImage;

    private IUiActionActive actionOutline;
    private PlayerSeat seat;
    private IRuntimeMoheData moheData;
    private IPlayer contPlayer;
    private bool waitingForSwap;

    public override bool Populate(PlayerSeat Seat, int pos)
    {
      Outline outline = GetComponent<Outline>();
      ParticleSystem particleSystem = MBehaviour.GetComponentInChildren<ParticleSystem>();
      actionOutline = new UiActionActive(this, outline, particleSystem);

      seat = Seat;

      contPlayer = GameData.Instance.RuntimeGame.Players.Find(player => player.Seat == seat);
      IRoster pRoster = contPlayer.Roster;

      if (pRoster.MoheRoster.Count <= pos)
        return false;

      moheData = pRoster.MoheRoster[pos];

      headerTxt = transform.Find("HeaderTxt").GetComponent<TextMeshProUGUI>();
      lvlTxt = transform.Find("LvlTxt").GetComponent<TextMeshProUGUI>();

      transform.Find("HealthBarImg").GetComponent<UiHealthBarFiller>().Populate(moheData);
      transform.Find("ExpBarImg").GetComponent<UiExpBarFiller>().Populate(moheData);
      moheImage = transform.Find("MohePortrait").GetComponent<Image>();

      headerTxt.text = moheData.BaseMohe.Data.Name;
      lvlTxt.text = moheData.BaseExpType.CalculateLevel(moheData.Exp).ToString();

      moheImage.sprite = moheData.BaseMohe.Data.Artwork;

      OnToggle += OnSetSwap;

      return true;
    }

    public void OnClick()
    {
      if (moheData != null && !moheData.MoheDead() && GameData.Instance.RuntimeGame.TurnLogic.IsMyTurn(contPlayer) && BoardController.Instance.CanClickJewel())
      {
        GameEvents.Instance.Notify<ISelectMoheActionButton>(i => i.OnSelectMoheActionButton(seat, moheData.InstanceID));
      }
    }

    public void OnSelectMoheActionButton(PlayerSeat Seat, string instanceId)
    {
      if (Seat == seat && moheData!= null && moheData.InstanceID == instanceId)
      {
        if (!waitingForSwap)
        {
          OnToggle.Invoke(true);
        } else
        {
          OnToggle.Invoke(false);
          GameEvents.Instance.Notify<IMoheSwap>(i => i.OnMoheSwap(seat, moheData.InstanceID));
        }
      }
      else
      {
        OnToggle.Invoke(false);
      }
    }

    public void OnResetMoheActionButton()
    {
      OnToggle.Invoke(false);
    }

    public void OnSetSwap(bool swap)
    {
      waitingForSwap = swap;
    }
  }
}
