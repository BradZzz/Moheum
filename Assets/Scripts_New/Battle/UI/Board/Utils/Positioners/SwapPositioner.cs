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

      jq.DoUnselect();
      jq2.DoUnselect();

      yield return new WaitForSeconds(utils.JEWELPOSITIONUNSELECTDELAY);

      OnDonePosition();
    }

    //private void OnNotifyPositionChange(IRuntimeJewel jewel, Vector3 from, Vector3 to)
    //{
    //  GameEvents.Instance.Notify<IPositionJewel>(i => i.OnJewelPosition(jewel, from, to));
    //}

    private void OnDonePosition()
    {
      JewelsFalling = null;
      GameEvents.Instance.Notify<IEvaluateBoard>(i => i.OnBoardEvaluateCheck());
    }
  }
}
