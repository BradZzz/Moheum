using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.UI.Utils;
using TMPro;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiMoheName : UiListener, IUiMoheName, IPlayerUpdateRuntime
  {
    // Start is called before the first frame update
    void Awake()
    {
      UiPlayerHUD = transform.parent.GetComponentInParent<IUiPlayerHUD>();
      TXT = GetComponent<TextMeshProUGUI>();
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
      Debug.Log("UiMoheName: " + UiPlayerHUD.PlayerController.Player.Roster.CurrentMohe().BaseMohe.Data.Name);
      TXT.text = UiPlayerHUD.PlayerController.Player.Roster.CurrentMohe().BaseMohe.Data.Name;
    }

    private IUiPlayerHUD UiPlayerHUD;
    private TextMeshProUGUI TXT;
  }
}
