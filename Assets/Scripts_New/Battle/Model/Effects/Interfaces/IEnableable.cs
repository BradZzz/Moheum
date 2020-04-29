using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.Effects.Interfaces
{
    public interface IEnableable : IEffectable
    {
        void DoEnable(bool enabled, IEffectable jewel);
    }
}
