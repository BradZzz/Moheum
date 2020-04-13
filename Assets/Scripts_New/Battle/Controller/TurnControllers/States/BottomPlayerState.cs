using Battle.Model.AI;
using Battle.Model.Player;

namespace Battle.Controller.TurnControllers.States
{
  /// <summary>
  ///     Bottom, where the User is always sitting.
  /// </summary>
  public class BottomPlayerState : AiTurnState
  {
    //----------------------------------------------------------------------------------------------------------

    #region Constructor

    public BottomPlayerState(TurnBasedFsm fsm, IGameData gameData, Battle.Configurations.Configurations configurations) : base(fsm, gameData, configurations)
    {
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Properties

    public override PlayerSeat Seat => PlayerSeat.Left;
    protected override AiArchetype AiArchetype => Configurations.BottomAiArchetype;
    public override bool IsAi => Configurations.BottomIsAi;
    public override bool IsUser => true;

    #endregion

    //----------------------------------------------------------------------------------------------------------
  }
}