using Battle.Model.Game.Mechanics;
using Battle.Model.Player;

namespace Battle.Controller.TurnControllers
{
    public interface IPlayerTurn
    {
        bool IsAi { get; }
        bool IsUser { get; }
        bool IsMyTurn { get; }
        PlayerSeat Seat { get; }
        IPlayer Player { get; }
        bool PassTurn();
        //Add in ability to swap
        //void Swap(SwapMechanics.RuntimeSwapData swapData);
    }
}