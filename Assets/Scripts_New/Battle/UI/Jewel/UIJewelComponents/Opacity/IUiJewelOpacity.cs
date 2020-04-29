using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public interface IUiJewelOpacity : IPostSelectJewel
  {
    void Execute(IRuntimeJewel jewelData);
    IRuntimeJewel Jewel { get; }
    SpriteRenderer Renderer { get; }
  }
}
