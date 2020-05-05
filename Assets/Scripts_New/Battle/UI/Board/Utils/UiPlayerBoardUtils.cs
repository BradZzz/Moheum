using System;
using System.Collections;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Extensions;
using System.Linq;
using Battle.Model.Jewel;
using Battle.UI.Jewel;
using Battle.UI.Jewel.Component;
using System.Collections.Generic;
using Battle.GameEvent;

namespace Battle.UI.Board.Utils
{
  /*
   * Handles the jewel falling code
   */
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

    private Queue<IRuntimeJewel> JewelsToFall;

    private Coroutine JewelsFalling;

    private float JEWELFALLDELAY = .01f;

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
      PlayerBoard = transform.parent.GetComponentInChildren<IUiBoard>();
      BoardPos = new UiBoardPositioner();
      JewelsToFall = new Queue<IRuntimeJewel>();
    }

    private void Start()
    {
    }

    //--------------------------------------------------------------------------------------------------------------

    public void Draw(IRuntimeJewel jewel)
    {
      Debug.Log("UiPlayerBoardUtils Draw");
      JewelsToFall.Enqueue(jewel);
      if (JewelsFalling == null)
      {
        JewelsFalling = StartCoroutine(CascadeJewelFromQueue());
      }
    }

    private IEnumerator CascadeJewelFromQueue()
    {
      IRuntimeJewel jq = JewelsToFall.Dequeue();

      if (jq.IsNew())
      {
        var uiJewel = UiJewelPool.Instance.Get(jq);
        IUiJewelComponents comp = uiJewel.MonoBehavior.GetComponent<IUiJewelComponents>();
        comp.UIRuntimeData.OnSetData(jq.Data);
        uiJewel.MonoBehavior.name = "Jewel_" + Count++;

        PlayerBoard.AddJewel(uiJewel);
      }

      Vector2 boardPos = BoardPos.OffsetJewelByPosition(jq.Pos);
      Vector3 to = BoardPos.GetNextJewelPosition(boardPos, deckPosition.position);
      Vector3 from = new Vector3(to.x, jq.IsNew() ? BoardPos.GetBoardTopPos().y : jq.LastPos.y, to.z);

      if (jq.IsNew() || jq.LastPos.y != jq.Pos.y)
      {
        OnNotifyPositionChange(jq, from, to);
      }

      yield return new WaitForSeconds(JEWELFALLDELAY);

      if (JewelsToFall.Count > 0)
      {
        JewelsFalling = StartCoroutine(CascadeJewelFromQueue());
      } else
      {
        // Send a signal to the board fsm to move to evaluateBoardState
        OnDoneCascade();
      }
    }

    private void OnNotifyPositionChange(IRuntimeJewel jewel, Vector3 from, Vector3 to)
    {
      GameEvents.Instance.Notify<IPositionJewel>(i => i.OnJewelPosition(jewel, from, to));
    }

    private void OnDoneCascade()
    {
      JewelsFalling = null;
      GameEvents.Instance.Notify<IEvaluateBoard>(i => i.OnBoardEvaluateCheck());
    }

    //public void Discard(IRuntimeJewel jewel)
    //{
    //  var uiJewel = PlayerBoard.GetJewel(jewel);
    //  PlayerBoard.DiscardJewel(uiJewel);
    //}

    //public void PlayCard(IRuntimeJewel jewel)
    //{
    //  var uiJewel = PlayerBoard.GetBoardData().GetJewel(jewel);
    //  PlayerBoard.PlayJewel(uiJewel);
    //}

    //--------------------------------------------------------------------------------------------------------------

    //private void Update()
    //{
    //  if (Input.GetKeyDown(KeyCode.Escape)) Restart();
    //}

    //public void Restart()
    //{
    //  SceneManager.LoadScene(0);
    //}

    //--------------------------------------------------------------------------------------------------------------
  }
}