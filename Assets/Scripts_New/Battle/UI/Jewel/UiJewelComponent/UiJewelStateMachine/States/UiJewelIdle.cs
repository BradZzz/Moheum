using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.UI.Jewel.UiJewelStateMachine.States
{
  /// <summary>
  ///     UI Card Idle behavior.
  /// </summary>
  public class UiJewelIdle : UiBaseJewelState
  {
    //--------------------------------------------------------------------------------------------------------------

    public UiJewelIdle(IUiJewel handler, BaseStateMachine fsm, Battle.UI.Jewel.UiJewelParameters.UiJewelParameters parameters) : base(handler, fsm,
        parameters)
    {
      DefaultSize = Handler.transform.localScale;
    }

    private Vector3 DefaultSize { get; }

    //--------------------------------------------------------------------------------------------------------------

    public override void OnEnterState()
    {
      Handler.Input.OnPointerDown += OnPointerDown;
      Handler.Input.OnPointerEnter += OnPointerEnter;

      if (Handler.Motion.Movement.IsOperating)
      {
        DisableCollision();
        Handler.Motion.Movement.OnFinishMotion += Enable;
      }
      else
      {
        Enable();
      }

      MakeRenderNormal();
      Handler.Motion.ScaleTo(DefaultSize, Parameters.ScaleSpeed);
    }

    public override void OnExitState()
    {
      Handler.Input.OnPointerDown -= OnPointerDown;
      Handler.Input.OnPointerEnter -= OnPointerEnter;
      Handler.Motion.Movement.OnFinishMotion -= Enable;
    }

    //--------------------------------------------------------------------------------------------------------------

    private void OnPointerEnter(PointerEventData obj)
    {
      if (Fsm.IsCurrent(this))
        Handler.Hover();
    }

    private void OnPointerDown(PointerEventData eventData)
    {
      if (Fsm.IsCurrent(this))
        Handler.Select();
    }

    //--------------------------------------------------------------------------------------------------------------
  }
}