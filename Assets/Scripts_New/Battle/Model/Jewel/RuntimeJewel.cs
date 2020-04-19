using System.Collections;
using System.Collections.Generic;
using Battle.Model.Effects.Interfaces;
using UnityEngine;

namespace Battle.Model.Jewel
{
    public class RuntimeJewel : IRuntimeJewel
    {
        public RuntimeJewel()
        {

        }

        public IJewelData Data => new JewelData();

        public void DoEnable(bool enabled, IEffectable item)
        {
            throw new System.NotImplementedException();
        }

        public void DoRemove(IEffectable item1)
        {
            throw new System.NotImplementedException();
        }

        public void DoSwap(IEffectable item1, IEffectable item2)
        {
            throw new System.NotImplementedException();
        }
    }
}
