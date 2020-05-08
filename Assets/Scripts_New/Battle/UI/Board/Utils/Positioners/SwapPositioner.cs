using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.UI.Jewel.Component;
using UnityEngine;

namespace Battle.UI.Board.Utils
{
  public class SwapPositioner
  {
    public SwapPositioner(IUiPlayerBoardUtils Utils)
    {
      utils = Utils;
      JewelsToPosition = new Queue<IRuntimeJewel>();
      BoardPos = new UiBoardPositioner();
    }

    private IUiPlayerBoardUtils utils;
    private Coroutine JewelsFalling;
    private Queue<IRuntimeJewel> JewelsToPosition;
    private UiBoardPositioner BoardPos;

    public void StartReposition(IRuntimeJewel jewel, IRuntimeJewel jewel2)
    {
      utils.MBehaviour.StartCoroutine(PositionJewelFromQueue(jewel, jewel2));
    }

    private IEnumerator PositionJewelFromQueue(IRuntimeJewel jq, IRuntimeJewel jq2)
    {
      Vector2 BoardTo1 = BoardPos.OffsetJewelByPosition(jq.Pos);
      Vector2 BoardFrom1 = BoardPos.OffsetJewelByPosition(jq.LastPos);

      Vector3 to1 = BoardPos.GetNextJewelPosition(BoardTo1, utils.DeckPosition.position);
      Vector3 from1 = BoardPos.GetNextJewelPosition(BoardFrom1, utils.DeckPosition.position);

      GameEvents.Instance.Notify<IPositionJewel>(i => i.OnJewelPosition(jq, from1, to1));

      Vector2 BoardTo2 = BoardPos.OffsetJewelByPosition(jq2.Pos);
      Vector2 BoardFrom2 = BoardPos.OffsetJewelByPosition(jq2.LastPos);

      Vector3 to2 = BoardPos.GetNextJewelPosition(BoardTo2, utils.DeckPosition.position);
      Vector3 from2 = BoardPos.GetNextJewelPosition(BoardFrom2, utils.DeckPosition.position);

      GameEvents.Instance.Notify<IPositionJewel>(i => i.OnJewelPosition(jq2, from2, to2));

      yield return new WaitForSeconds(utils.JEWELPOSITIONDELAY);

      OnDonePosition();

      //if (JewelsToPosition.Count > 0)
      //{
      //  JewelsFalling = utils.MBehaviour.StartCoroutine(PositionJewelFromQueue());
      //}
      //else
      //{
      //  OnDonePosition();
      //}

      //JewelsFalling = utils.MBehaviour.StartCoroutine(CascadeJewelFromQueue());

      //IRuntimeJewel jq = JewelsToPosition.Dequeue();

      //Vector2 BoardTo = BoardPos.OffsetJewelByPosition(jq.Pos);
      //Vector2 BoardFrom = BoardPos.OffsetJewelByPosition(jq.LastPos);

      //Vector3 to = BoardPos.GetNextJewelPosition(BoardTo, utils.DeckPosition.position);
      //Vector3 from = jq.IsNew() ? new Vector3(to.x, BoardPos.GetBoardTopPos().y, to.z)
      //  : new Vector3(to.x, BoardPos.GetNextJewelPosition(BoardFrom, utils.DeckPosition.position).y, to.z);

      //if (jq.IsNew())
      //{
      //  var uiJewel = UiJewelPool.Instance.Get(jq);
      //  IUiJewelComponents comp = uiJewel.MonoBehavior.GetComponent<IUiJewelComponents>();
      //  comp.UIRuntimeData.OnSetData(jq);
      //  uiJewel.MonoBehavior.name = jq.JewelID;
      //  uiJewel.transform.position = from;
      //  utils.PlayerBoard.AddJewel(uiJewel);
      //}

      //yield return new WaitForSeconds(utils.JEWELFALLDELAY);

      //if (jq.IsNew() || jq.LastPos.y != jq.Pos.y)
      //{
      //  OnNotifyPositionChange(jq, from, to);
      //}

      //if (JewelsToPosition.Count > 0)
      //{
      //  JewelsFalling = utils.MBehaviour.StartCoroutine(CascadeJewelFromQueue());
      //}
      //else
      //{
      //  OnDoneCascade();
      //}
    }

    //private void OnNotifyPositionChange(IRuntimeJewel jewel, Vector3 from, Vector3 to)
    //{
    //  GameEvents.Instance.Notify<IPositionJewel>(i => i.OnJewelPosition(jewel, from, to));
    //}

    private void OnDonePosition()
    {
      JewelsFalling = null;
      GameEvents.Instance.Notify<IRemoveSelectedBoard>(i => i.OnBoardRemoveSelectedCheck());
    }
  }
}
