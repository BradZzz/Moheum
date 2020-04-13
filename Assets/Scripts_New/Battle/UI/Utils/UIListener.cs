using Battle.GameEvent;
using Patterns;
using UnityEngine;

namespace Battle.UI.Utils
{
  public class UiListener : MonoBehaviour, IListener
  {
    protected virtual void Start()
    {
      //subscribe
      if (GameEvents.Instance)
        GameEvents.Instance.AddListener(this);
    }

    protected virtual void OnDestroy()
    {
      //unsubscribe
      if (GameEvents.Instance)
        GameEvents.Instance.RemoveListener(this);
    }
  }
}