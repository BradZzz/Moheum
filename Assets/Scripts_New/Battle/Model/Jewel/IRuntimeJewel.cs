using System;
using System.Collections.Generic;
using Battle.Model.Effects.Interfaces;
using Battle.UI.ModelJewel.Mechanics;
using UnityEngine;

namespace Battle.Model.Jewel
{
    public interface IRuntimeJewel : IEffectable, ISelectable, IUnSelectable
  {
        Vector2 Pos { get; }
        IJewelData Data { get; }
        bool IsSelected { get; set; }

        Action<IRuntimeJewel> OnSelect { get; set; }
        Action<IRuntimeJewel> OnUnSelect { get; set; }
    }
}