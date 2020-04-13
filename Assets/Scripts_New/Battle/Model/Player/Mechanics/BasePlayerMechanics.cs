namespace Battle.Model.Player.Mechanics
{
  /// <summary>
  ///     Base class for any complex player mechanic.
  /// </summary>
  public abstract class BasePlayerMechanics
  {
    /// <summary>
    ///     Player reference.
    /// </summary>
    protected IPlayer Player { get; }


    protected BasePlayerMechanics(IPlayer player)
    {
      Player = player;
    }
  }
}