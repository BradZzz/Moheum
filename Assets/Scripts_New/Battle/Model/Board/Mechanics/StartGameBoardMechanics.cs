using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard;
using Patterns;
using UnityEngine;

namespace Battle.UI.RuntimeBoard.Mechanics
{
  public class StartGameBoardMechanics : BaseBoardMechanics, IListener, IStartGame
  {
    public StartGameBoardMechanics(IRuntimeBoard board) : base(board)
    {
      GameEvents.Instance.AddListener(this);
      runtimeboard = board;
    }

    private IRuntimeBoard runtimeboard;

    public void OnStartGame(IPlayer starter)
    {
      Execute();
    }

    /// <summary>
    ///     Execution of start game
    /// </summary>
    public void Execute()
    {
      //IRuntimeJewel[,] jewelMap = runtimeboard.GetBoardData().GetMap();
      //int width = jewelMap.GetLength(0);
      //int height = jewelMap.GetLength(1);

      ////This needs to be pulled in from another file
      //JewelDatabase db = JewelDatabase.Instance;
      //List<JewelData> jewels = db.GetFullList();

      //// Draw the initial jewels the board needs here!
      //// The position relates to the offset position from center
      //Vector2 middle = new Vector2((int) width / 2, (int) height / 2);
      //for (int x = 0; x < width; x++)
      //{
      //  for (int y = 0; y < height; y++)
      //  {
      //    IRuntimeJewel thisJewel = new RuntimeJewel(jewels[Random.Range(0, jewels.Count)]);
      //    // Sets the jewel's position in the data map. This needs to be somewhere else
      //    runtimeboard.GetBoardData().SetJewel(thisJewel, new Vector2(x, y));
      //    // Sets the jewel's position on the board
      //    OnDrawJewel(thisJewel, new Vector2(x - middle.x, y - middle.y));
      //  }
      //}
      //GameEvents.Instance.Notify<ICascadeBoard>(i => i.OnBoardCascadeCheck());
    }

    /// <summary>
    ///     Dispatch start game event to the listeners.
    /// </summary>
    /// <param name="starterPlayer"></param>
    private void OnDrawJewel(IRuntimeJewel jewel, Vector2 pos)
    {
      //GameEvents.Instance.Notify<IBoardDrawJewel>(i => i.OnDraw(jewel, pos));
    }
  }
}
