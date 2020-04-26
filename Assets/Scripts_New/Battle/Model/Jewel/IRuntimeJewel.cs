using System.Collections.Generic;
using Battle.Model.Effects.Interfaces;

namespace Battle.Model.Jewel
{
    public interface IRuntimeJewel : IEffectable, IEnableable, IRemovable, ISwappable
    {
        IJewelData Data { get; set; }
    }
}