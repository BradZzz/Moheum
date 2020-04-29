using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Effects.Interfaces;
using Battle.UI.ModelJewel.Mechanics;
using UnityEngine;

namespace Battle.Model.Jewel
{
    public class RuntimeJewel : IRuntimeJewel
    {
        public RuntimeJewel(IJewelData data)
        {
          this.data = data;
          IsSelected = false;

          selectJewelMechanic = new SelectJewelMechanics(this);
        }

        private SelectJewelMechanics selectJewelMechanic;
        private IJewelData data;

        public SelectJewelMechanics SelectJewelMechanic => selectJewelMechanic;
        public IJewelData Data => data;
        public bool IsSelected { get; set; }

        public Action<IRuntimeJewel> OnSelect { get; set; }

        public void DoSelect(IEffectable jewel)
        {
            Debug.Log("DoSelect");
            if (this == (RuntimeJewel) jewel)
            {
                Debug.Log("Jewel Selected!");
                OnSelect.Invoke(this);
            }
        }

        //public void DoEnable(bool enabled, IEffectable jewel)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void DoRemove(IEffectable jewel)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void DoSwap(IEffectable jewel1, IEffectable jewel2)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
