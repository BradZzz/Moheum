using Patterns;
using UnityEngine;
using Battle.Configurations;
using Battle.Controller;
using Battle.Controller.TurnControllers;
using Battle.Model.Player;
using Battle.GameEvent;

namespace Battle.Controller
{
    /// <summary>
    ///     Main Controller. Holds the FSM which controls the game flow. Also provides access to the players controllers.
    /// </summary>
    public class GameController : SingletonMB<GameController>, IGameController
    {
        [SerializeField] private Battle.Configurations.Configurations configurations;

        //----------------------------------------------------------------------------------------------------------

        #region Properties

        /// <summary>
        ///     All game data. Access via Singleton Pattern.
        /// </summary>
        public IGameData Data => GameData.Instance;

        /// <summary>
        ///     State machine that holds the game logic.
        /// </summary>
        private TurnBasedFsm TurnBasedLogic { get; set; }

        /// <summary>
        ///     Handler for the state machine. Used to dispatch coroutines.
        /// </summary>
        public MonoBehaviour MonoBehaviour => this;

        public string Name => gameObject.name;

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Initialization

        protected override void OnAwake()
        {
            Logger.Log<GameController>("Awake");
        }

        private void Start()
        {
            Logger.Log<GameController>("Start");

            StartBattle();
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Operations

        /// <summary>
        ///  Return the Left Player. TODO: Define this value inside the configurations.
        /// </summary>
        /// <returns></returns>
        public IPlayerTurn GetUser()
        {
            return GetPlayerController(configurations.PlayerTurn.UserSeat);
        }

        /// <summary>
        ///     Provides access to players controllers according to the player seat.
        /// </summary>
        /// <param name="seat"></param>
        /// <returns></returns>
        public IPlayerTurn GetPlayerController(PlayerSeat seat)
        {
            return TurnBasedLogic.GetPlayerController(seat);
        }

        public IPlayerTurn GetOpponentPlayersController(PlayerSeat Seat)
        {
          if (Seat == PlayerSeat.Left)
          {
            return TurnBasedLogic.GetPlayerController(PlayerSeat.Right);
          } else
          {
            return TurnBasedLogic.GetPlayerController(PlayerSeat.Left);
          }
        }

        /// <summary>
        ///     Start the battle. Called only once after being initialized by the Bootstrapper.
        /// </summary>
        [Button]
        public void StartBattle()
        {
            TurnBasedLogic = new TurnBasedFsm(this, Data, configurations);
            TurnBasedLogic.StartBattle();
        }

        [Button]
        public void EndBattle()
        {
            TurnBasedLogic.EndBattle();
        }

        [Button]
        public void RestartGameImmediately()
        {
            GameEvents.Instance.Notify<IRestartGame>(i => i.OnRestart());
            Data.CreateGame();
            StartBattle();
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------
    }
}