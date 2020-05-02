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
    class JewelQueue {
      public JewelQueue(IRuntimeJewel Jewel, Vector2 Pos)
      {
        jewel = Jewel;
        pos = Pos;
      }

      public IRuntimeJewel jewel;
      public Vector2 pos;
    }

    //--------------------------------------------------------------------------------------------------------------

    #region Fields

    private int Count { get; set; }

    [SerializeField]
    [Tooltip("World point where the deck is positioned")]
    private Transform deckPosition;

    private UiBoardPositioner BoardPos;

    private IUiBoard PlayerBoard { get; set; }

    private Queue<JewelQueue> JewelsToFall;

    private Coroutine JewelsFalling;

    private float JEWELFALLSPEED = .01f;
    private float JEWELFALLDELAY = .01f;

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
      PlayerBoard = transform.parent.GetComponentInChildren<IUiBoard>();
      BoardPos = new UiBoardPositioner();
      JewelsToFall = new Queue<JewelQueue>();
    }

    private void Start()
    {
    }

    //--------------------------------------------------------------------------------------------------------------

    public void Draw(IRuntimeJewel jewel)
    {
      Debug.Log("UiPlayerBoardUtils Draw");
      JewelsToFall.Enqueue(new JewelQueue(jewel, jewel.Pos));
      if (JewelsFalling == null)
      {
        JewelsFalling = StartCoroutine(CascadeJewelFromQueue());
      }
    }

    private IEnumerator CascadeJewelFromQueue()
    {
      JewelQueue jq = JewelsToFall.Dequeue();
      var uiJewel = UiJewelPool.Instance.Get(jq.jewel);
      IUiJewelComponents comp = uiJewel.MonoBehavior.GetComponent<IUiJewelComponents>();
      comp.UIRuntimeData.OnSetData(jq.jewel.Data);
      uiJewel.MonoBehavior.name = "Jewel_" + Count++;

      Vector2 boardPos = BoardPos.OffsetJewelByPosition(jq.pos);
      Vector3 uiPos = BoardPos.GetNextJewelPosition(boardPos, deckPosition.position);
      Vector3 topBoard = new Vector3(uiPos.x, BoardPos.GetBoardTopPos().y, uiPos.z);
      PlayerBoard.AddJewel(uiJewel);
      uiJewel.transform.position = topBoard;

      int count = 0;
      float diff = Math.Abs(uiPos.y - topBoard.y);
      while (uiJewel.transform.position != uiPos && count < diff)
      {
        uiJewel.transform.position = Vector3.Lerp(topBoard, uiPos, (float) count / diff);
        // If the diff is smaller, the jewels need to fall faster
        yield return new WaitForSeconds(JEWELFALLSPEED);
        count++;
      }
      uiJewel.transform.position = uiPos;
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

    private void OnDoneCascade()
    {
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