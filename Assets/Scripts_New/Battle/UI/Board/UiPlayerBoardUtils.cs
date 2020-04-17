using System;
using System.Collections;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Extensions;
using System.Linq;
using Battle.Model.Jewel;

namespace Battle.UI.Board
{
  public class UiPlayerBoardUtils : MonoBehaviour, IUiPlayerBoardUtils
  {
    //--------------------------------------------------------------------------------------------------------------

    #region Fields

    private int Count { get; set; }

    [SerializeField]
    [Tooltip("World point where the deck is positioned")]
    private Transform deckPosition;

    private IUiBoard PlayerBoard { get; set; }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
      PlayerBoard = transform.parent.GetComponentInChildren<IUiBoard>();
    }

    private void Start()
    {
      //Draw(null);
    }

    //--------------------------------------------------------------------------------------------------------------

    public void Draw(IRuntimeJewel jewel)
    {
      Debug.Log("Draw");
      Debug.Log(UiJewelPool.Instance);
      var uiJewel = UiJewelPool.Instance.Get(jewel);
      uiJewel.MonoBehavior.name = "Jewel_" + Count;
      uiJewel.transform.position = deckPosition.position;
      Count++;
      PlayerBoard.AddJewel(uiJewel);
    }

    public void Discard(IRuntimeJewel jewel)
    {
      var uiJewel = PlayerBoard.GetJewel(jewel);
      PlayerBoard.DiscardJewel(uiJewel);
    }

    public void PlayCard(IRuntimeJewel jewel)
    {
      var uiJewel = PlayerBoard.GetJewel(jewel);
      PlayerBoard.PlayJewel(uiJewel);
    }

    //--------------------------------------------------------------------------------------------------------------

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape)) Restart();
    }

    public void Restart()
    {
      SceneManager.LoadScene(0);
    }

    //--------------------------------------------------------------------------------------------------------------
  }
}