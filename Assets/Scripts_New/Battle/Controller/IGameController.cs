using Battle.Controller.TurnControllers;
using Battle.Model.Player;
using Patterns.StateMachine;
using UnityEngine;

namespace Battle.Controller
{
    public interface IGameController : IStateMachineHandler, IGameDataHandler
    {
        MonoBehaviour MonoBehaviour { get; }
        IPlayerTurn GetUser();
        IPlayerTurn GetPlayerController(PlayerSeat seat);
        void StartBattle();
        void EndBattle();
        void RestartGameImmediately();
    }
}