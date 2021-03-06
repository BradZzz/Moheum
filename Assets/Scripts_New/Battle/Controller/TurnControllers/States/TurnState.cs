﻿using System;
using System.Collections;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Game.Mechanics;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Controller;
using Battle.UI.UIAnimation;
using UnityEngine;

namespace Battle.Controller.TurnControllers.States
{
    public abstract class TurnState : BaseBattleState, IFinishPlayerTurn, IPlayerTurn
    {
        //----------------------------------------------------------------------------------------------------------

        #region Constructor

        protected TurnState(TurnBasedFsm fsm, IGameData gameData, Battle.Configurations.Configurations configurations) : base(fsm, gameData,
            configurations)
        {
            var game = GameData.RuntimeGame;

            //get player according to the seat
            Player = game.TurnLogic.GetPlayer(Seat);

            //register turn state
            Fsm.RegisterPlayerState(Player, this);
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Properties

        public IPlayer Player { get; }
        public IPlayer Opponent => GameData.RuntimeGame.TurnLogic.GetOpponent(Player);
        public bool IsMyTurn => GameData.RuntimeGame.TurnLogic.IsMyTurn(Player);
        public virtual PlayerSeat Seat => PlayerSeat.Right;
        public virtual bool IsAi => false;
        public virtual bool IsUser => false;

        private Coroutine TimeOutRoutine { get; set; }
        private Coroutine TickRoutine { get; set; }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Game Events

        void IFinishPlayerTurn.OnFinishPlayerTurn(IPlayer player)
        {
            if (IsMyTurn)
                NextTurn();
        }

        /// <summary>
        ///     Switches the turn according to the next player.
        /// </summary>
        private void NextTurn()
        {
            var game = GameData.RuntimeGame;
            var nextPlayer = game.TurnLogic.NextPlayer;
            var nextState = Fsm.GetPlayerController(nextPlayer);
            OnNextState(nextState);
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Operations

        public override void OnEnterState()
        {
            base.OnEnterState();

            //setup timer to end the turn
            TimeOutRoutine = Fsm.Handler.MonoBehaviour.StartCoroutine(TimeOut());

            //start current player turn
            Fsm.Handler.MonoBehaviour.StartCoroutine(StartTurn());
        }

        public override void OnExitState()
        {
            base.OnExitState();
            RestartTimeouts();
        }

        /// <summary>
        ///     Clear the state to the initial configuration and stops all the internal routines.
        /// </summary>
        public override void OnClear()
        {
            base.OnClear();
            RestartTimeouts();
        }

        protected virtual void RestartTimeouts()
        {
            if (TimeOutRoutine != null)
                Fsm.Handler.MonoBehaviour.StopCoroutine(TimeOutRoutine);
            TimeOutRoutine = null;

            if (TickRoutine != null)
                Fsm.Handler.MonoBehaviour.StopCoroutine(TickRoutine);
            TickRoutine = null;
        }

        /// <summary>
        ///     Check if the player can pass the turn and passes the turn to the next player.
        /// </summary>
        public bool PassTurn()
        {
            Fsm.Handler.MonoBehaviour.StartCoroutine(WaitForAnimations());
            return true;
        }

        private IEnumerator WaitForAnimations()
        {
            bool IsEmpty()
            {
                return UiAnimationQueue.Instance.IsEmpty;
            }

            yield return new WaitUntil(IsEmpty);
            GameData.RuntimeGame.FinishCurrentPlayerTurn();
        }

        //public void Swap(SwapMechanics.RuntimeSwapData swapData)
        //{
        //    GameData.RuntimeGame.Swap(swapData);
        //}

        //----------------------------------------------------------------------------------------------------------

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Coroutines

        private IEnumerator TickRoutineAsync()
        {
            while (true)
            {
                //every second
                yield return new WaitForSeconds(1);
                GameData.RuntimeGame.Tick();
            }
        }

        /// <summary>
        ///     Finishes the player turn.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator TimeOut()
        {
            //disable the timeout for player
            if (IsUser)
                yield return 0;

            if (TimeOutRoutine != null)
            {
                Fsm.Handler.MonoBehaviour.StopCoroutine(TimeOutRoutine);
                TimeOutRoutine = null;
            }
            else
                yield return new WaitForSeconds(Configurations.TimeOutTurn);

            PassTurn();
        }

        /// <summary>
        ///     Starts the player turn.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator StartTurn()
        {
            yield return new WaitForSeconds(Configurations.TimeStartTurn);
            //GameEvents.Instance.Notify<ICascadeBoard>(i => i.OnBoardCascadeCheck());

            //while (!BoardController.Instance.CanManipulate()){}

            //GameData.RuntimeGame.StartCurrentPlayerTurn();
            Fsm.Handler.MonoBehaviour.StartCoroutine(StartPlayerTurn());

            //setup tick routine
            TickRoutine = Fsm.Handler.MonoBehaviour.StartCoroutine(TickRoutineAsync());
        }

        private IEnumerator StartPlayerTurn()
        {
            //int count = 0;
            while (!BoardController.Instance.CanManipulate()) {
              yield return new WaitForSeconds(1);
              //Debug.Log("StartPlayerTurn count: " + count.ToString());
            }
            GameData.RuntimeGame.StartCurrentPlayerTurn();
        }

        #endregion

    //----------------------------------------------------------------------------------------------------------
  }
}