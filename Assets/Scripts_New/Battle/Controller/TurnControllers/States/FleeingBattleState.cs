using Battle.GameEvent;
using Battle.Model.Player;

namespace Battle.Controller.TurnControllers.States
{
  /// <summary>
  ///     Holds the Game flow when a match is Finished.
  /// </summary>
  public class FleeingBattleState : BaseBattleState
  {
    //----------------------------------------------------------------------------------------------------------

    #region Constructor

    public FleeingBattleState(TurnBasedFsm fsm, IGameData gameData, Battle.Configurations.Configurations configurations) : base(fsm, gameData,
        configurations)
    {
    }

    #endregion
  }
}