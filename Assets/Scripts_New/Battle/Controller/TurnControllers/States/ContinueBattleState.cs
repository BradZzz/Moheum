using Battle.GameEvent;
using Battle.Model.Player;

namespace Battle.Controller.TurnControllers.States
{
  /// <summary>
  ///     Holds the Game flow when a match is Finished.
  /// </summary>
  public class ContiueBattleState : BaseBattleState
  {
    //----------------------------------------------------------------------------------------------------------

    #region Constructor

    public ContiueBattleState(TurnBasedFsm fsm, IGameData gameData, Battle.Configurations.Configurations configurations) : base(fsm, gameData,
        configurations)
    {
    }

    #endregion
  }
}