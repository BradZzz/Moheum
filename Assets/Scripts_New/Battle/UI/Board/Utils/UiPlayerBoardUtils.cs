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

    private IUiBoard playerBoard { get; set; }

    private Queue<IRuntimeJewel> JewelsToFall;

    private Coroutine JewelsFalling;

    private MonoBehaviour mBehaviour;
    private float jEWELFALLDELAY = .01f;

    public Transform DeckPosition => deckPosition;
    public MonoBehaviour MBehaviour => mBehaviour;
    public IUiBoard PlayerBoard => playerBoard;
    public float JEWELFALLDELAY => jEWELFALLDELAY;

    private CascadePositoner cascadePos;
    private SwapPositioner swapPos;

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
      playerBoard = transform.parent.GetComponentInChildren<IUiBoard>();
      BoardPos = new UiBoardPositioner();
      JewelsToFall = new Queue<IRuntimeJewel>();
      mBehaviour = this;

      cascadePos = new CascadePositoner(this);
      swapPos = new SwapPositioner(this);
    }

    private void Start()
    {
    }

    //--------------------------------------------------------------------------------------------------------------
    public void CascadeJewelBoard(IRuntimeJewel jewel)
    {
      cascadePos.StartReposition(jewel);
    }

    public void SwapJewelBoard(IRuntimeJewel jewel)
    {
      swapPos.StartReposition(jewel);
    }



    //public void Draw(IRuntimeJewel jewel)
    //{
    //  Debug.Log("UiPlayerBoardUtils Draw");
    //  JewelsToFall.Enqueue(jewel);
    //  if (JewelsFalling == null)
    //  {
    //    JewelsFalling = StartCoroutine(CascadeJewelFromQueue());
    //  }
    //}

    //private IEnumerator CascadeJewelFromQueue()
    //{
    //  IRuntimeJewel jq = JewelsToFall.Dequeue();

    //  Vector2 BoardTo = BoardPos.OffsetJewelByPosition(jq.Pos);
    //  Vector2 BoardFrom = BoardPos.OffsetJewelByPosition(jq.LastPos);

    //  Vector3 to = BoardPos.GetNextJewelPosition(BoardTo, deckPosition.position);
    //  Vector3 from = jq.IsNew() ? new Vector3(to.x, BoardPos.GetBoardTopPos().y, to.z)
    //    : new Vector3(to.x, BoardPos.GetNextJewelPosition(BoardFrom, deckPosition.position).y, to.z);

    //  if (jq.IsNew())
    //  {
    //    var uiJewel = UiJewelPool.Instance.Get(jq);
    //    IUiJewelComponents comp = uiJewel.MonoBehavior.GetComponent<IUiJewelComponents>();
    //    comp.UIRuntimeData.OnSetData(jq);
    //    uiJewel.MonoBehavior.name = jq.JewelID;
    //    uiJewel.transform.position = from;
    //    PlayerBoard.AddJewel(uiJewel);
    //  }

    //  yield return new WaitForSeconds(JEWELFALLDELAY);

    //  if (jq.IsNew() || jq.LastPos.y != jq.Pos.y)
    //  {
    //    OnNotifyPositionChange(jq, from, to);
    //  }

    //  if (JewelsToFall.Count > 0)
    //  {fdeck
    //    JewelsFalling = StartCoroutine(CascadeJewelFromQueue());
    //  } else
    //  {
    //    OnDoneCascade();
    //  }
    //}

    //private void OnNotifyPositionChange(IRuntimeJewel jewel, Vector3 from, Vector3 to)
    //{
    //  GameEvents.Instance.Notify<IPositionJewel>(i => i.OnJewelPosition(jewel, from, to));
    //}

    //private void OnDoneCascade()
    //{
    //  JewelsFalling = null;
    //  GameEvents.Instance.Notify<IEvaluateBoard>(i => i.OnBoardEvaluateCheck());
    //}
  }
}