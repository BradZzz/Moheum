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
  [CreateAssetMenu(menuName = "Effect/Pop")]
  public class PopEffect : BaseEffect
  {
    public override bool Execute(IRuntimeJewel TriggerJewel)
    {
      Debug.Log("Execute Pop Effect!");
      if (TriggerJewel.Data.JewelID != Jewel)
        return false;

      // Find all jewels of type
      List<IRuntimeJewel> matching = new List<IRuntimeJewel>();

      // Make sure the jewel clicked is the correct jewel for the effect

      // Pop in a radius between min and max
      int radius = Random.Range(MinAmt, MaxAmt);

      // Get gameboard
      IRuntimeJewel[,] board = GameData.Instance.RuntimeGame.GameBoard.GetBoardData().GetMap();

      int width = board.GetLength(0);
      int height = board.GetLength(1);
      for (int x = (int) TriggerJewel.Pos.x - radius; x < (int)TriggerJewel.Pos.x + radius; x++)
      {
        for (int y = (int)TriggerJewel.Pos.y + radius; y < (int)TriggerJewel.Pos.y + radius; y++)
        {
          if (x >= 0 && x < width && y >= 0 && y < height)
          {
            matching.Add(board[x, y]);
          }
        }
      }

      // Destroy jewels in list
      foreach (IRuntimeJewel jwl in matching)
      {
        //GameData.Instance.RuntimeGame.GameBoard.GetBoardData().SetJewel(null, jwl.Pos);
        GameEvents.Instance.Notify<IRemoveJewel>(i => i.OnRemoveJewel(jwl));
      }

      return true;
    }
  }
}
