using System;
using Battle.Model.Jewel;
using Battle.UI.Jewel.UiJewelComponent;
using Battle.UI.Jewel.UiJewelData;
using Battle.UI.Utils;
using UnityEngine;

namespace Battle.UI.Jewel
{
  /// <summary>
  ///     Main components of an UI card.
  /// </summary>
  public interface IUiJewelComponents
  {
    IUiJewelData UIRuntimeData { get; set; }
    IUIJewelSprite UIJewelSprite { get; set; }

    Camera MainCamera { get; }
    MeshRenderer[] MRenderers { get; }
    MeshRenderer MRenderer { get; }
    SpriteRenderer[] Renderers { get; }
    SpriteRenderer Renderer { get; }
    Collider Collider { get; }
    Rigidbody Rigidbody { get; }
    IMouseInput Input { get; }
    MonoBehaviour MonoBehavior { get; }
    GameObject gameObject { get; }
    Transform transform { get; }

    IUiJewelTransform UIJewelTransform { get; }

    Action<IJewelData> SetData { get; set; }
    //void SetData(IJewelData data);
  }
}