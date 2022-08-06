using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.GameEvent;
using Battle.Model.AI;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Controller;
using Battle.Model.RuntimeBoard.Utils;
using Battle.UI.Player;
using UnityEngine;

namespace Battle.Controller.TurnControllers.States
{
  public class AiTurnState : TurnState
  {
    //----------------------------------------------------------------------------------------------------------

    #region Constructor

    protected AiTurnState(TurnBasedFsm fsm, IGameData gameData, Battle.Configurations.Configurations configurations) : base(fsm, gameData,
        configurations)
    {
      AIModule = new AIModule(Player, GameData.RuntimeGame);
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Properties

    private Coroutine AiFinishTurnRoutine { get; set; }
    private AIModule AIModule { get; }
    protected virtual AiArchetype AiArchetype => Configurations.Ai.TopPlayer.Archetype;
    private float AiFinishTurnDelay => Configurations.AiFinishTurnDelay;
    private float AiDoTurnDelay => Configurations.AiDoTurnDelay;

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Operations

    protected override IEnumerator StartTurn()
    {
      AIModule.SwapAiToArchetype(AiArchetype);
      yield return base.StartTurn();
      //call do turn routine
      Fsm.Handler.MonoBehaviour.StartCoroutine(AiDoTurn());
      //call finish turn routine
      //AiFinishTurnRoutine = Fsm.Handler.MonoBehaviour.StartCoroutine(AiFinishTurn(AiFinishTurnDelay));
    }

    protected override void RestartTimeouts()
    {
      base.RestartTimeouts();

      if (AiFinishTurnRoutine != null)
        Fsm.Handler.MonoBehaviour.StopCoroutine(AiFinishTurnRoutine);
      AiFinishTurnRoutine = null;
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Coroutines

    private IEnumerator AiDoTurn()
    {
      yield return new WaitForSeconds(AiDoTurnDelay);

      if (!IsMyTurn)
        yield break;

      if (!IsAi)
        yield break;

      //Fsm.Handler.MonoBehaviour.StartCoroutine(GameData.RuntimeGame.ExecuteAiTurn(Seat));
      Fsm.Handler.MonoBehaviour.StartCoroutine(ExecuteAiTurn(Seat));
    }

    public IEnumerator ExecuteAiTurn(PlayerSeat seat)
    {
      // Clear board
      GameEvents.Instance.Notify<IRemoveSelectedBoard>(i => i.OnBoardRemoveSelectedCheck());

      // wait until the board state is clean.
      int count = 0;
      while (!BoardController.Instance.CanClickJewel() && count < 20) {
        yield return new WaitForSeconds(1);
        Debug.Log("count: " + count.ToString());
        Debug.Log("state: " + BoardController.Instance.CurrentState.ToString());
        count++;
      }

      // Check to see if the computer can use any abilities
      IRuntimeMoheData mohe = GameController.Instance.GetPlayerController(seat).Player.Roster.CurrentMohe();

      yield return new WaitForSeconds(.25f);

      if (mohe.UseableAbility())
      {
        Fsm.Handler.MonoBehaviour.StartCoroutine(ExecuteAiAbility(Seat));
      }
      else
      {
        Fsm.Handler.MonoBehaviour.StartCoroutine(ExecuteAiSwap(Seat));
      }

      yield return null;
    }

    public IEnumerator ExecuteAiSwap(PlayerSeat seat)
    {
      List<SwapChoices> matchesBuff = AIModule.GetBestMove(seat);
      if (matchesBuff.Count > 0)
      {
        // click first gem
        GameEvents.Instance.Notify<ISelectJewel>(i => i.OnSelect(matchesBuff[0].jewel1));

        yield return new WaitForSeconds(0.5f);
        while (!BoardController.Instance.CanManipulate()) { }

        // click second gem
        GameEvents.Instance.Notify<ISelectJewel>(i => i.OnSelect(matchesBuff[0].jewel2));

        //while (!BoardController.Instance.CanManipulate()) { }

        AiFinishTurnRoutine = Fsm.Handler.MonoBehaviour.StartCoroutine(AiFinishTurn(AiFinishTurnDelay));
      }
      else
      {
        GameEvents.Instance.Notify<IResetBoard>(i => i.OnBoardResetCheck());
      }
    }

    public IEnumerator ExecuteAiAbility(PlayerSeat seat)
    {
      List<IRuntimeAbility> abilityBuff = AIModule.GetBestAbility(seat);
      if (abilityBuff.Count > 0)
      {
        GameObject GO = GameObject.FindGameObjectsWithTag("UiPanel").Where((go) => go.GetComponent<IUiPlayerHUD>().Seat == seat).ToList()[0];
        // Check for nav
        if (GO.GetComponent<IUiPlayerHUD>().UINavButtons.Current != NavID.Attack) {
          // click on the ability
          GameEvents.Instance.Notify<IPlayerNav>(i => i.OnPlayerNav(seat, NavID.Attack));
          yield return new WaitForSeconds(1f);
        }

        // click on the ability
        GameEvents.Instance.Notify<ISelectAtkActionButton>(i => i.OnSelectAtkActionButton(seat, abilityBuff[0].Ability.AbilityID));
        //Debug.Log("AI clicked on ability");

        yield return new WaitForSeconds(1f);
        while (!BoardController.Instance.CanClickJewel()) { }

        IRuntimeJewel jwl = AIModule.GetAbilityJewels(seat, abilityBuff[0])[0];
        GameEvents.Instance.Notify<ISelectJewel>(i => i.OnSelect(jwl));
        //Debug.Log("AI clicked on jewel");
        yield return new WaitForSeconds(2f);
      }
      //while (!BoardController.Instance.CanManipulate()) { }
      //Debug.Log("AI ready to perform next action!");
      Fsm.Handler.MonoBehaviour.StartCoroutine(ExecuteAiTurn(seat));
    }

    private IEnumerator AiFinishTurn(float delay)
    {
      yield return new WaitForSeconds(delay);
      if (!IsMyTurn)
        yield break;

      if (!IsAi)
        yield break;

      if (!Configurations.PlayerTurn.DebugAiTurn)
        Fsm.Handler.MonoBehaviour.StartCoroutine(TimeOut());
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------
  }
}
