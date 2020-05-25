using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard;
using Extensions;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  [CreateAssetMenu(menuName = "Effect/Destroy")]
  public class DestroyEffect : BaseEffect
  {
    public override bool Execute(IRuntimeJewel TriggerJewel)
    {
      Debug.Log("Execute Destroy Effect!");

      // Destroy between MinAmt and MaxAmt of random Jewel
      // -1 values in MinAmt or MaxAmt will destroy all of type Jewel

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
      foreach(IRuntimeJewel jwl in matching)
      {
        //GameData.Instance.RuntimeGame.GameBoard.GetBoardData().SetJewel(null, jwl.Pos);
        GameEvents.Instance.Notify<IRemoveJewel>(i => i.OnRemoveJewel(jwl));
      }

      // Notify Evaluate
      //GameEvents.Instance.Notify<ICascadeBoard>(i => i.OnBoardCascadeCheck());

      return true;
    }
  }
}
