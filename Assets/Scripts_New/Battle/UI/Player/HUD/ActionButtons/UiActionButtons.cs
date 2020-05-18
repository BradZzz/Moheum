using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.UI.Player
{
  public class UiActionButtons : MonoBehaviour, IUiActionButtons
  {
    void Awake()
    {
      actionButtons = new List<IUiActionButton>();

      navButtons = transform.parent.GetComponentInChildren<IUiNavButtons>();
      navButtons.OnNavigate += NavigateActions;
    }

    void Start()
    {
      Debug.Log("UiActionButtons Start");
      Debug.Log(navButtons.OnNavigate);
      NavigateActions(navButtons.Current);
    }

    private IUiNavButtons navButtons;
    private List<IUiActionButton> actionButtons;

    public IUiNavButtons NavButtons => navButtons;
    public List<IUiActionButton> ActionButtons => actionButtons;

    public void NavigateActions(NavID nav)
    {
      // Clear the currrent buttons
      foreach (Transform t in transform)
      {
        Destroy(t.gameObject);
      }

      for (int i = 0; i < 4; i++)
      {
        IUiActionButton action;
        switch (nav)
        {
          case NavID.Attack:
            action = UiAtkActionPooler.Instance.Get(i);
            break;
          case NavID.Item:
            action = UiItemActionPooler.Instance.Get(i);
            break;
          case NavID.Mohe:
            action = UiMoheActionPooler.Instance.Get(i);
            break;
          case NavID.Run:
            action = UiFleeActionPooler.Instance.Get(i);
            break;
          default:
            action = UiAtkActionPooler.Instance.Get(i);
            break;
        }
        action.MBehaviour.transform.parent = transform;
        action.MBehaviour.transform.localScale = Vector3.one;
        ActionButtons.Add(action);
      }
    }
  }
}
