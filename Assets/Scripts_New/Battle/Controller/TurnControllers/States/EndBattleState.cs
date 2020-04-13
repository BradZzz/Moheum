using Battle.GameEvent;
using Battle.Model.Player;

namespace Battle.Controller.TurnControllers.States
{
  /// <summary>
  ///     Holds the Game flow when a match is Finished.
  /// </summary>
  public class EndBattleState : BaseBattleState, IFinishGame
  {
    //----------------------------------------------------------------------------------------------------------

    #region Constructor

    public EndBattleState(TurnBasedFsm fsm, IGameData gameData, Battle.Configurations.Configurations configurations) : base(fsm, gameData,
        configurations)
    {
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Game Events

    void IFinishGame.OnFinishGame(IPlayer winner)
    {
      Fsm.EndBattle();
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------
  }
}