using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using Battle.UI.Board;
using Battle.UI.Jewel.Component;
using Battle.UI.Jewel.UiJewelData;
using Battle.UI.Utils.Tools.UiTransform;
using Patterns.StateMachine;
using UnityEngine;

namespace Battle.UI.Jewel
{
    public interface IUiJewel : IStateMachineHandler, IUiJewelComponents, IUiMotion
    {
      IJewelData Data { get;  }
      bool IsInitialized { get; }
      bool IsDragging { get; }
      bool IsHovering { get; }
      bool IsDisabled { get; }
      void Initialize();
      void Disable();
      void Enable();
      void Select();
      void Unselect();
      void Hover();
      void Draw();
      void Discard();
      void Play();
      void Restart();
      void Target();
    }
}
