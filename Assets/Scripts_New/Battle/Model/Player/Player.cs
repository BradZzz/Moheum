using System;
using Battle.Model.Game.Mechanics;
using Battle.Model.Jewel;
using Battle.Model.Player.Mechanics;
using Extensions;
using Tools;
using UnityEngine;

namespace Battle.Model.Player
{
    /// <summary>
    ///     A concrete player class.
    /// </summary>
    public class Player : IPlayer
    {
        public Player(PlayerSeat seat, Battle.Configurations.Configurations configurations = null)
        {
            Configurations = configurations;
            Seat = seat;

      //Hand = new Collection<IRuntimeJewel>();

      //if (teamData != null)
      //    Team = new Team(this, teamData);

      //if (deckData != null)
      //    Library = new Library(this, deckData, Configurations);

      //Graveyard = new Graveyard(this);

      #region Mechanics

      //DrawMechanics = new DrawMechanics(this);
      //DiscardMechanics = new DiscardMechanics(this);
      //PlayCardMechanics = new PlayCardMechanics(this);
      StartTurnMechanics = new StartTurnMechanics(this);
      FinishTurnMechanics = new FinishTurnMechanics(this);
      //SpawnMechanics = new SpawnMechanics(this);
      //ManaMechanics = new ManaMechanics(this);
      //PowerMechanic = new PowerMechanic(this);
      //GoldMechanic = new GoldMechanic(this);

      #endregion
    }

        //----------------------------------------------------------------------------------------------------------

        //public ILibrary Library { get; private set; }

        //public Graveyard Graveyard { get; private set; }

        //public ITeam Team { get; private set; }

        //public Collection<IRuntimeCard> Hand { get; private set; }

        public Battle.Configurations.Configurations Configurations { get; }

        public PlayerSeat Seat { get; }

        public bool IsUser => Seat == Configurations.PlayerTurn.UserSeat;

        #region Mechanics

        public SwapMechanics SwapMechanics { get; }

        public StartTurnMechanics StartTurnMechanics { get; }

        public FinishTurnMechanics FinishTurnMechanics { get; }

        public void StartTurn()
        {
          StartTurnMechanics.StartTurn();
        }

        public void FinishTurn()
        {
          FinishTurnMechanics.FinishTurn();
        }

        public void Swap(IRuntimeJewel jewel, IRuntimeJewel jewel2)
        {
          //SwapMechanics.Execute(SwapMechanics.RuntimeSwapData(jewel, jewel2));

          var attackData = new SwapMechanics.RuntimeSwapData()
          {
            Swapper = jewel,
            Swappee = jewel2
          };

        }

        #endregion

        ////----------------------------------------------------------------------------------------------------------

        //#region Play

        //void IPlayer.Swap(IRuntimeJewel jewel, IRuntimeJewel jewel2);
        //{
        //    if (card.IsPower)
        //        PowerMechanic.AddPower(card);

        //    return public void StartTurn()
        //{
        //    throw new NotImplementedException();
        //}

        //public void FinishTurn()
        //{
        //    throw new NotImplementedException();
        //}

        //PlayCardMechanics.Play(card);
        //}

        //public bool CanSwap(IRuntimeJewel card)
        //{
        //    return PlayCardMechanics.CanPlay(card);
        //}

        //#endregion

        ////----------------------------------------------------------------------------------------------------------

        //#region Turn

        //void IPlayer.FinishTurn()
        //{
        //    FinishTurnMechanics.FinishTurn();
        //}

        //void IPlayer.StartTurn()
        //{
        //    StartTurnMechanics.StartTurn();
        //}

        //#endregion

        ////----------------------------------------------------------------------------------------------------------        
    }
}