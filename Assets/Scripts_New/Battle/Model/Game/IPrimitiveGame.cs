using System.Collections;
using System.Collections.Generic;
using Battle.Model.Game.Mechanics;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard;
using Battle.Model.TurnLogic;

namespace Battle.Model.Game
{
    /// <summary>
    ///     A game interface.
    /// </summary>
    public interface IPrimitiveGame
    {
        Battle.Configurations.Configurations Configurations { get; }

        List<BaseGameMechanics> Mechanics { get; }

        List<IPlayer> Players { get; }

        ITurnLogic TurnLogic { get; }

        bool IsGameStarted { get; set; }

        bool IsGameFinished { get; set; }

        bool IsTurnInProgress { get; set; }

        int TurnTime { get; set; }

        int TotalTime { get; set; }

        IRuntimeBoard GameBoard { get; }

        void PreStartGame();

        void StartGame();

        void StartCurrentPlayerTurn();

        void FinishCurrentPlayerTurn();

        void Tick();

        //void Swap(SwapMechanics.RuntimeSwapData data);

        //IEnumerator ExecuteAiSwap(PlayerSeat seat);

        //public IEnumerator ExecuteAiAction(PlayerSeat seat);
    }
}