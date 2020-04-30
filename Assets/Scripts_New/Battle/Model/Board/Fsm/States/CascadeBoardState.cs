using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * The board needs to be filled up with missing gems
   */

  public class CascadeBoardState : BaseBoardState
  {
    public CascadeBoardState(BoardBasedFsm Fsm, IBoardData BoardData) : base(Fsm, BoardData)
    {
      boardData = BoardData;
    }

    private IBoardData boardData;

    public override void OnEnterState()
    {
      base.OnEnterState();

      Debug.Log("OnEnterState CascadeBoard");

      // Here is where I call the IEnumerator to bring each jewel from the top of the board down to the bottom
      IRuntimeJewel[,] jewelMap = boardData.GetMap();
      int width = jewelMap.GetLength(0);
      int height = jewelMap.GetLength(1);

      //This needs to be pulled in from another file
      JewelDatabase db = JewelDatabase.Instance;
      List<JewelData> jewels = db.GetFullList();

      // Draw the initial jewels the board needs here!
      // The position relates to the offset position from center
      Vector2 middle = new Vector2((int)width / 2, (int)height / 2);
      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height; y++)
        {
          IRuntimeJewel thisJewel = new RuntimeJewel(jewels[Random.Range(0, jewels.Count)]);
          // Sets the jewel's position in the data map. This needs to be somewhere else
          boardData.SetJewel(thisJewel, new Vector2(x, y));
          // Sets the jewel's position on the board
          OnCascadeJewel(thisJewel, new Vector2(x - middle.x, y - middle.y));
        }
      }


      //Fsm.
      //Fsm.Mon

      ////setup timer to end the turn
      //TimeOutRoutine = Fsm.Handler.MonoBehaviour.StartCoroutine(TimeOut());

      ////start current player turn
      //Fsm.Handler.MonoBehaviour.StartCoroutine(StartTurn());
    }

    /// <summary>
    ///     Dispatch start game event to the listeners.
    /// </summary>
    /// <param name="starterPlayer"></param>
    private void OnCascadeJewel(IRuntimeJewel jewel, Vector2 pos)
    {
      Debug.Log("OnCascadeJewel");
      GameEvents.Instance.Notify<ICascadeJewel>(i => i.OnJewelFall(jewel, pos));
      GameEvents.Instance.Notify<IBoardDrawJewel>(i => i.OnDraw(jewel, pos));
    }
  }
}
