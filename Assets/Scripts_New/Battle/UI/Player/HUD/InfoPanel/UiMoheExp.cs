using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiMoheExp : UiListener, IUiMoheExp, IPlayerUpdateRuntime
  {
    // Start is called before the first frame update
    void Awake()
    {
      UiPlayerHUD = transform.parent.GetComponentInParent<IUiPlayerHUD>();
      Image = GetComponent<Image>();
      //Refresh();
    }

    public void OnPlayerUpdateRuntime()
    {
      Refresh();
    }

    //private void Update()
    //{
    //  Refresh();
    //}

    private void Refresh()
    {
      int currentExp = UiPlayerHUD.PlayerController.Player.Roster.CurrentMohe().Exp;
      int thisLevel = (int)UiPlayerHUD.PlayerController.Player.Roster.CurrentMohe().BaseExpType.IntFromLastLevel(currentExp);
      int nextLevel = (int)UiPlayerHUD.PlayerController.Player.Roster.CurrentMohe().BaseExpType.IntToNextLevel(currentExp);
      float fill = thisLevel / (float)nextLevel;

      //Debug.Log("Refresh Health: " + currentHealth.ToString() + " : " + maxHealth.ToString() + " : " + fill.ToString());

      Image.fillAmount = fill;
    }

    private IUiPlayerHUD UiPlayerHUD;
    private Image Image;
  }
}
