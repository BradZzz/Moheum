using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiMoheHealth : UiListener, IUiMoheHealth, IPlayerUpdateRuntime
  {
    private void Awake()
    {
      UiPlayerHUD = transform.parent.GetComponentInParent<IUiPlayerHUD>();
      Image = GetComponent<Image>();
      //Refresh();
    }

    public void OnPlayerUpdateRuntime()
    {
      Debug.Log("UiMoheHealth OnPlayerUpdateRuntime!");
      //Refresh();
    }

    private void Update()
    {
      Refresh();
    }

    private void Refresh()
    {
      int currentHealth = UiPlayerHUD.PlayerController.Player.Roster.CurrentMohe().Health;
      int maxHealth = (int) UiPlayerHUD.PlayerController.Player.Roster.CurrentMohe().BaseMohe.Stats.health;
      float fill = currentHealth / (float)maxHealth;

      //Debug.Log("Refresh Health: " + currentHealth.ToString() + " : " + maxHealth.ToString() + " : " + fill.ToString());

      Image.fillAmount = fill;
    }

    private IUiPlayerHUD UiPlayerHUD;
    private Image Image;
  }
}
