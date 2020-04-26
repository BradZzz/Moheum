using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.UiJewelComponent
{
  public interface IUiJewelTransform
  {
    Action<IJewelData> Execute();
  }
}
