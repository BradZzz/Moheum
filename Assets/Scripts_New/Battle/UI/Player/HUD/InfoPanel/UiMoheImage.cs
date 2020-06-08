using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiMoheImage : UiListener, IUiMoheImage, IPlayerUpdateRuntime
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
      Image.sprite = UiPlayerHUD.PlayerController.Player.Roster.CurrentMohe().BaseMohe.Data.Artwork;
    }

    private IUiPlayerHUD UiPlayerHUD;
    private Image Image;
  }
}
