using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.UI.Jewel.Component;
using UnityEngine;

namespace Battle.UI.Board.Utils
{
  public class CascadePositoner
  {
    public CascadePositoner(IUiPlayerBoardUtils Utils)
    {
      utils = Utils;
      JewelsToFall = new Queue<IRuntimeJewel>();
      BoardPos = new UiBoardPositioner();
    }

    private IUiPlayerBoardUtils utils;
    private Coroutine JewelsFalling;
    private Queue<IRuntimeJewel> JewelsToFall;
    private UiBoardPositioner BoardPos;

    public void StartReposition(IRuntimeJewel jewel)
    {
      JewelsToFall.Enqueue(jewel);
      if (JewelsFalling == null)
      {
        JewelsFalling = utils.MBehaviour.StartCoroutine(CascadeJewelFromQueue());
      }
    }

    private IEnumerator CascadeJewelFromQueue()
    {
      IRuntimeJewel jq = JewelsToFall.Dequeue();

      Vector2 BoardTo = BoardPos.OffsetJewelByPosition(jq.Pos);
      Vector2 BoardFrom = BoardPos.OffsetJewelByPosition(jq.LastPos);

      Vector3 to = BoardPos.GetNextJewelPosition(BoardTo, utils.DeckPosition.position);
      Vector3 from = jq.IsNew() ? new Vector3(to.x, BoardPos.GetBoardTopPos().y, to.z)
        : new Vector3(to.x, BoardPos.GetNextJewelPosition(BoardFrom, utils.DeckPosition.position).y, to.z);

      if (jq.IsNew())
      {
        var uiJewel = UiJewelPool.Instance.Get(jq);
        IUiJewelComponents comp = uiJewel.MonoBehavior.GetComponent<IUiJewelComponents>();
        comp.UIRuntimeData.OnSetData(jq);
        uiJewel.MonoBehavior.name = jq.JewelID;
        uiJewel.transform.position = from;
        utils.PlayerBoard.AddJewel(uiJewel);
      }

      yield return new WaitForSeconds(utils.JEWELFALLDELAY);

      if (jq.IsNew() || jq.LastPos.y != jq.Pos.y)
      {
        OnNotifyPositionChange(jq, from, to);
      }

      if (JewelsToFall.Count > 0)
      {
        JewelsFalling = utils.MBehaviour.StartCoroutine(CascadeJewelFromQueue());
      }
      else
      {
        yield return new WaitForSeconds(.2f);
        CheckEndCascade();
      }
    }

    private void CheckEndCascade()
    {
      if (JewelsToFall.Count > 0)
      {
        JewelsFalling = utils.MBehaviour.StartCoroutine(CascadeJewelFromQueue());
      }
      else
      {
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
  }
}
