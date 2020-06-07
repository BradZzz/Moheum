using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Model.Game;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard;
using Patterns;
using UnityEngine;

namespace Battle.Controller
{
    /// <summary>
    ///     All classes dependent of the game data.
    /// </summary>
    public interface IGameDataHandler
    {
        IGameData Data { get; }
    }

    /// <summary>
    ///     Game data public interface
    /// </summary>
    public interface IGameData
    {
        IPrimitiveGame RuntimeGame { get; }
        //TeamsCurrentData GetTeamResources();
        void CreateGame();
        void LoadGame();
        void Clear();
    }

    /// <summary>
    ///     Game data concrete implementation with Singleton Pattern.
    /// </summary>
    public class GameData : SingletonMB<GameData>, IGameData
    {
        //--------------------------------------------------------------------------------------------------------

        [SerializeField] private Battle.Configurations.Configurations configurations;
        //[SerializeField] private LibraryData deckData;
        //[SerializeField] private TeamsCurrentData currentTeams;

        //private TeamData TeamData => currentTeams.PlayerTeam;
        //private TeamData EnemiesData => currentTeams.EnemyTeam;

        #region Properties

        /// <summary>
        ///     All game data.
        /// </summary>
        public IPrimitiveGame RuntimeGame { get; private set; }

        #endregion

        //--------------------------------------------------------------------------------------------------------

        #region Unity Callbacks

        /// <summary>
        ///     Initialize game data OnAwake.
        /// </summary>
        protected override void OnAwake()
        {
            Logger.Log<GameData>("Awake");
            CreateGame();
        }

        private void Start()
        {
            Logger.Log<GameData>("Start");
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------

        #region Operations

        /// <summary>
        ///     Clears the game data.
        /// </summary>
        public void Clear()
        {
            RuntimeGame = null;
        }

        public IRuntimeMoheData CreateTestMohe(MoheID moheID, PlayerSeat seat)
        {
          MoheData mData = MoheDatabase.Instance.Get(moheID);
          MoheData.MoheStatData stats = new MoheData.MoheStatData();
          stats.health = 10;
          Mohe sampleMohe = new Mohe(mData, mData.Abilities.Select(ability => ability.ability.AbilityID).ToList(), stats);
          RuntimeMoheData sampleRuntimeMohe = new RuntimeMoheData(sampleMohe, seat, 1);
          return sampleRuntimeMohe;
        }

        public IRoster CreateTestRoster(PlayerSeat seat)
        {
            List<IRuntimeMoheData> moheList = new List<IRuntimeMoheData>() {
              CreateTestMohe(MoheID.Beanlock, seat),
              CreateTestMohe(MoheID.SunSlime, seat)
            };
            return new Roster(moheList);
        }

        /// <summary>
        ///     Create a new game data overriding the previous one. Produces Garbage.
        /// </summary>
        public void CreateGame()
        {
            //create and connect players to their seats
            var player1 = new Player(configurations.PlayerTurn.UserSeat, CreateTestRoster(configurations.PlayerTurn.UserSeat), configurations: configurations);

            //if the second player doesn't have a deck, send null
            var player2 = new Player(PlayerSeat.Right, CreateTestRoster(PlayerSeat.Right), configurations: configurations);

            var board = new Board(configurations);

            //create game data
            RuntimeGame = new Game(new List<IPlayer> { player1, player2 }, board, configurations);
        }

        public void LoadGame()
        {
            throw new NotImplementedException();
        }

        //public TeamsCurrentData GetTeamResources()
        //{
        //    return currentTeams;
        //}

        #endregion

        //--------------------------------------------------------------------------------------------------------
    }
}