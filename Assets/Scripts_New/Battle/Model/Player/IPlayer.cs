using Battle.Configurations;
using Battle.Model.Jewel;
using Battle.Model.Player;
using Tools;

namespace Battle.Model.Player
{
    public interface IPlayer
    {
        Battle.Configurations.Configurations Configurations { get; }
        //Collection<IRuntimeJewel> Hand { get; }
        PlayerSeat Seat { get; }
        //Player spells and 
        IRoster Roster { get; }
        void StartTurn();
        void SwapTurn();
        void FinishTurn();
        bool IsUser { get; }
        bool HasSwapped { get; set; }
    }
}