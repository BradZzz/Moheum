using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Player;
using Battle.UI.Utils;
using UnityEngine;

public class UIOverrlayController : UiListener, IUIOverrlayController, IPreGameStart, IFinishGame
{
    private IUIEndGameOverlay uIEndGameOverlay;
    private IUIBaseOverlay uIBaseOverlay;

    void Awake()  
    {
      uIEndGameOverlay = GetComponentInChildren<IUIEndGameOverlay>();
      uIBaseOverlay = GetComponentInChildren<IUIBaseOverlay>();
    }

    public void OnPreGameStart(List<IPlayer> players)
    {
      Debug.Log("UIOverrlayController: OnPreGameStart");
      uIBaseOverlay.MonoBehaviour.gameObject.SetActive(false);
      uIEndGameOverlay.MonoBehaviour.gameObject.SetActive(false);
    }

    public void OnFinishGame(IPlayer winner)
    {
      Debug.Log("UIOverrlayController: OnEndGame");
      uIBaseOverlay.MonoBehaviour.gameObject.SetActive(true);
      uIEndGameOverlay.MonoBehaviour.gameObject.SetActive(true);
    }
}
