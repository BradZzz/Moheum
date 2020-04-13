using UnityEngine;

namespace Battle.Data
{
    public interface IBaseData
    {
        string Name { get; }
        string Description { get; }
        Sprite Artwork { get; }
        //JewelType Type { get; }
        //JewelEffects Effects { get; }
    }
}