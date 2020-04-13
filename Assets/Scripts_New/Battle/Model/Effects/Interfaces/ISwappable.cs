using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.Effects.Interfaces
{
    public interface ISwappable
    {
        void DoSwap(IEffectable item1, IEffectable item2);
    }
}
