using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.Effects.Interfaces
{
    public interface ISwappable : IEffectable
  {
        void DoSwap(IEffectable jewel1, IEffectable jewel2);
    }
}
