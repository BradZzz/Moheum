using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.Effects.Interfaces
{
    public interface ISelectable : IEffectable
    {
        void DoSelect(IEffectable jewel);
    }
}
