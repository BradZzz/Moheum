using System.Collections;
using System.Collections.Generic;
using Battle.Model.Player;
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

    void Start()
    {
      seat = transform.parent.GetComponent<IUiNavButtons>().Seat;
    }

    [SerializeField]
    private NavID navID;
    private Image image;
    private PlayerSeat seat;

    public NavID NavID => navID;
    public Image Image => image;
    public PlayerSeat Seat => seat;
  }
}
