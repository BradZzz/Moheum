using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.MoheModel;
using Battle.UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiHealthBarFiller : UiListener, IUiBarFiller, IPlayerUpdateRuntime
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
      int currentHealth = mohe.Health;
      int maxHealth = (int)mohe.BaseMohe.Stats.health;
      float fill = currentHealth / (float)maxHealth;

      //Debug.Log("Refresh Health: " + currentHealth.ToString() + " : " + maxHealth.ToString() + " : " + fill.ToString());

      Image.fillAmount = fill;
    }

    private Image Image;
  }
}
