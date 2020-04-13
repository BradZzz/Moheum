using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.Effects.Interfaces
{
    public interface IRemovable
    {
        void DoRemove(IEffectable item1);
    }
}
