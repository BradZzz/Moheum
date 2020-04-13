using Battle.Model.AI;
using Battle.Model.Player;

namespace Battle.Controller.TurnControllers.States
{
  public class TopPlayerState : AiTurnState
  {
    //----------------------------------------------------------------------------------------------------------

    #region Constructor

    public TopPlayerState(TurnBasedFsm fsm, IGameData gameData, Battle.Configurations.Configurations configurations) : base(fsm, gameData,
        configurations)
    {
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Properties

    public override PlayerSeat Seat => PlayerSeat.Right;
    public override bool IsAi => Configurations.TopIsAi;
    protected override AiArchetype AiArchetype => Configurations.TopAiArchetype;
    public override bool IsUser => false;

    #endregion

    //----------------------------------------------------------------------------------------------------------
  }
}