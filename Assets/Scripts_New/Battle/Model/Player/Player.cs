using System;
using Battle.Controller;
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
        public Player(PlayerSeat seat, IRoster Roster, IInventory Inventory, Battle.Configurations.Configurations configurations = null)
        {
            Configurations = configurations;
            Seat = seat;
            roster = Roster;
            inventory = Inventory;

            StartTurnMechanics = new StartTurnMechanics(this);
            FinishTurnMechanics = new FinishTurnMechanics(this);
            SwapTurnMechanics = new SwapTurnMechanics(this);
            SwapMoheMechanics = new SwapMoheMechanics(this);
            ChargeAbilityMechanics = new ChargeAbilityMechanics(this);
            FleeBattleMechanics = new FleeBattleMechanics(this);
            //UseAbilityMechanics = new UseAbilityMechanics(this);
            GainJewelBonusMechanics = new GainJewelBonusMechanics(this);
        }

        //----------------------------------------------------------------------------------------------------------

        public Battle.Configurations.Configurations Configurations { get; }

        public PlayerSeat Seat { get; }

        public bool IsUser => Seat == Configurations.PlayerTurn.UserSeat;

        #region Mechanics

        //public SwapMechanics SwapMechanics { get; }
        public StartTurnMechanics StartTurnMechanics { get; }
        public SwapTurnMechanics SwapTurnMechanics { get; }
        public ChargeAbilityMechanics ChargeAbilityMechanics { get; }
        public SwapMoheMechanics SwapMoheMechanics { get; }
        //public UseAbilityMechanics UseAbilityMechanics { get; }
        public FinishTurnMechanics FinishTurnMechanics { get; }
        public GainJewelBonusMechanics GainJewelBonusMechanics { get; }
        public FleeBattleMechanics FleeBattleMechanics { get; }

        public bool HasSwapped { get; set; }

        public IRoster Roster => roster;
        private IRoster roster;

        public IInventory Inventory => inventory;
        private IInventory inventory;

        public void StartTurn()
        {
          StartTurnMechanics.StartTurn();
        }

        public void SwapTurn()
        {
          SwapTurnMechanics.SwappedOnTurn();
        }

        public void FinishTurn()
        {
          FinishTurnMechanics.FinishTurn();
        }

        #endregion 
    }
}