using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiNavButton : MonoBehaviour, IUiNavButton
  {
    void Awake()
    {
      image = GetComponent<Image>();
    }

    [SerializeField]
    private NavID navID;

    private Image image;

    public NavID NavID => navID;
    public Image Image => image;
  }
}
