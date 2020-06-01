using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.MoheModel;
using Battle.UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiExpBarFiller : UiListener, IUiBarFiller, IPlayerUpdateRuntime
  {
    private void Awake()
    {
      Image = GetComponent<Image>();
    }

    private IRuntimeMoheData mohe;

    public void Populate(IRuntimeMoheData Mohe)
    {
      mohe = Mohe;
      Refresh();
    }

    public void OnPlayerUpdateRuntime()
    {
      if (mohe != null)
        Refresh();
    }

    private void Update()
    {
      //Refresh();
    }

    private void Refresh()
    {
      int currentExp = mohe.Exp;
      int lastLvl = (int)mohe.BaseExpType.IntFromLastLevel(currentExp);
      int nxtLvl = (int)mohe.BaseExpType.IntToNextLevel(currentExp);
      float fill = (currentExp - lastLvl) / ((float)nxtLvl - lastLvl);

      Image.fillAmount = fill;
    }

    private Image Image;
  }
}
