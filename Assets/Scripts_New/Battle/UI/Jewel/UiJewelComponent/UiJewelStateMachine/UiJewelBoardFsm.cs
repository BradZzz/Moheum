using System;
using Battle.UI.Jewel.UiJewelStateMachine.States;
using Patterns.StateMachine;
using UnityEngine;

namespace Battle.UI.Jewel.UiJewelStateMachine
{
  /// <summary>
  ///     State Machine that holds all states of a UI Card.
  /// </summary>
  public class UiJewelBoardFsm : BaseStateMachine
  {
    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public UiJewelBoardFsm(Camera camera, Battle.UI.Jewel.UiJewelParameters.UiJewelParameters jewelConfigsParameters, IUiJewel handler = null) :
        base(handler)
    {
      JewelConfigsParameters = jewelConfigsParameters;

      IdleState = new UiJewelIdle(handler, this, JewelConfigsParameters);
      //DisableState = new UiCardDisable(handler, this, CardConfigsParameters);
      //DragState = new UiCardDrag(handler, camera, this, CardConfigsParameters);
      //HoverState = new UiCardHover(handler, this, CardConfigsParameters);
      //DrawState = new UiCardDraw(handler, this, CardConfigsParameters);
      //DiscardState = new UiCardDiscard(handler, this, CardConfigsParameters);
      //TargetState = new UiCardTarget(handler, camera, this, cardConfigsParameters);

      RegisterState(IdleState);
      //RegisterState(DisableState);
      //RegisterState(DragState);
      //RegisterState(HoverState);
      //RegisterState(DrawState);
      //RegisterState(DiscardState);
      //RegisterState(TargetState);

      Initialize();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Properties

    private UiJewelIdle IdleState { get; }
    //private UiCardDisable DisableState { get; }
    //private UiCardDrag DragState { get; }
    //private UiCardHover HoverState { get; }
    //private UiCardDraw DrawState { get; }
    //private UiCardDiscard DiscardState { get; }
    //private UiCardTarget TargetState { get; }
    private Battle.UI.Jewel.UiJewelParameters.UiJewelParameters JewelConfigsParameters { get; }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Operations

    public void Hover()
    {
      //PushState<UiCardHover>();
    }

    public void Disable()
    {
      //PushState<UiCardDisable>();
    }

    public void Enable()
    {
      PushState<UiJewelIdle>();
    }

    public void Select()
    {
      //PushState<UiCardDrag>();
    }

    public void Unselect()
    {
      Enable();
    }

    public void Draw()
    {
      //PushState<UiCardDraw>();
    }

    public void Discard()
    {
      //PushState<UiCardDiscard>();
    }

    public void Play()
    {

    }

    public void Target()
    {
      //PushState<UiCardTarget>();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------
  }
}