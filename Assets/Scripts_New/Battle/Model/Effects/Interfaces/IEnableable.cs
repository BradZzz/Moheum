using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.Effects.Interfaces
{
    public interface IEnableable
    {
        void DoEnable(bool enabled, IEffectable item);
    }
}
