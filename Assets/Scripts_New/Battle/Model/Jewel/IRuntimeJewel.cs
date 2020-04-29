using System;
using System.Collections.Generic;
using Battle.Model.Effects.Interfaces;
using Battle.UI.ModelJewel.Mechanics;

namespace Battle.Model.Jewel
{
    public interface IRuntimeJewel : IEffectable, ISelectable
    {
        IJewelData Data { get; }
        bool IsSelected { get; set; }

        SelectJewelMechanics SelectJewelMechanic { get; }

        Action<IRuntimeJewel> OnSelect { get; set; }
    }
}