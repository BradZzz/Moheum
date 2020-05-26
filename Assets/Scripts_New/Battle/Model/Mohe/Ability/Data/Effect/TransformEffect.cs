using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Extensions;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  [CreateAssetMenu(menuName = "Effect/Transform")]
  public class TransformEffect : BaseEffect
  {
    public JewelID TransformJewel;

    public override bool Execute(IRuntimeJewel TriggerJewel)
    {
      // Get gameboard
      IRuntimeJewel[,] board = GameData.Instance.RuntimeGame.GameBoard.GetBoardData().GetMap();

      // Find all jewels of type
      List<IRuntimeJewel> matching = new List<IRuntimeJewel>();

      int width = board.GetLength(0);
      int height = board.GetLength(1);
      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height; y++)
        {
          if (board[x, y].Data.JewelID == Jewel || Jewel == JewelID.any)
          {
            matching.Add(board[x, y]);
          }
        }
      }

      // Pick out the right jewels in range
      // -1 values in MinAmt or MaxAmt will destroy all of type Jewel
      if (MinAmt != -1 && MaxAmt != -1)
      {
        int amt = Random.Range(MinAmt, MaxAmt);
        ListExtensions.Shuffle(matching);
        matching = matching.Take(amt).ToList();
      }

      // Destroy jewels in list
      foreach (IRuntimeJewel jwl in matching)
      {
        GameEvents.Instance.Notify<ITransformJewel>(i => i.OnTransformJewel(jwl, TransformJewel));
      }

      return true;
    }
  }
}
