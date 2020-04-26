using System;
using System.Collections;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Extensions;
using System.Linq;
using Battle.Model.Jewel;
using Battle.UI.Jewel;

namespace Battle.UI.Board.Utils
{
  public class UiPlayerBoardUtils : MonoBehaviour, IUiPlayerBoardUtils
  {
    //--------------------------------------------------------------------------------------------------------------

    #region Fields

    private int Count { get; set; }

    [SerializeField]
    [Tooltip("World point where the deck is positioned")]
    private Transform deckPosition;

    private UiBoardPositioner BoardPos;

    private IUiBoard PlayerBoard { get; set; }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
      PlayerBoard = transform.parent.GetComponentInChildren<IUiBoard>();
      BoardPos = new UiBoardPositioner();
    }

    private void Start()
    {
      //Draw(null);
    }

    //--------------------------------------------------------------------------------------------------------------

    public void Draw(IRuntimeJewel jewel, Vector2 pos)
    {
      Debug.Log("Draw");
      //Debug.Log(UiJewelPool.Instance);
      var uiJewel = UiJewelPool.Instance.Get(jewel);
      Debug.Log("UiPlayerBoardUtils uiJewel");
      //Debug.Log(uiJewel);
      UIJewelComponent comp = uiJewel.MonoBehavior.GetComponent<UIJewelComponent>();
      comp.UIRuntimeData.OnSetData(jewel.Data);
      //JewelData jd = uiJewel.MonoBehavior.AddComponent<JewelData>() as JewelData;
      uiJewel.MonoBehavior.name = "Jewel_" + Count++;
      //Offset jewel position from center
      Vector3 uiPos = BoardPos.GetNextJewelPosition(pos, deckPosition.position);
      uiJewel.transform.position = uiPos;
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