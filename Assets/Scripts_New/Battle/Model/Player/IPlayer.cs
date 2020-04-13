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
        //ITeam Team { get; }
        void StartTurn();
        void FinishTurn();
        void Swap(IRuntimeJewel jewel, IRuntimeJewel jewel2);
        bool IsUser { get; }
    }
}