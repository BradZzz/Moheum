using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Effects.Interfaces;
using Battle.UI.ModelJewel.Mechanics;
using UnityEngine;

namespace Battle.Model.Jewel
{
    public class RuntimeJewel : IRuntimeJewel
    {
        public RuntimeJewel(IJewelData Data, Vector2 Pos, string JewelID)
        {
          jewelID = JewelID;
          data = Data;
          pos = Pos;
          lastPos = new Vector2(-1,-1);
          IsSelected = false;

          selectJewelMechanic = new SelectJewelMechanics(this);
          unSelectJewelMechanic = new UnselectJewelMechanics(this);
          jewelFallMechanics = new JewelFallMechanics(this);
        }

        private SelectJewelMechanics selectJewelMechanic;
        private UnselectJewelMechanics unSelectJewelMechanic;
        private JewelFallMechanics jewelFallMechanics;

        private string jewelID;
        
        private IJewelData data;
        private Vector2 lastPos;
        private Vector2 pos;

        public SelectJewelMechanics SelectJewelMechanic => selectJewelMechanic;
        public UnselectJewelMechanics UnselectJewelMechanic => unSelectJewelMechanic;
        public JewelFallMechanics JewelFallMechanics => jewelFallMechanics;

        public string JewelID => jewelID;
        public IJewelData Data => data;
        public Vector2 LastPos => lastPos;
        public Vector2 Pos => pos;
        public bool IsSelected { get; set; }

        public Action<IRuntimeJewel> OnSelect { get; set; }
        public Action<IRuntimeJewel> OnUnSelect { get; set; }

        // Put the new pos into the old pos. rotate
        public bool IsNew()
        {
            return (int) lastPos.x == -1 && (int) lastPos.y == -1;
        }

        public void RotatePos(Vector2 Pos)
        {
            lastPos = pos;
            pos = Pos;
        }
     
        public void DoSelect()
        {
          OnSelect.Invoke(this);
          PostSelect();
        }

        private void PostSelect()
        {
          GameEvents.Instance.Notify<IPostSelectJewel>(i => i.OnPostSelect(this));
        }

        public void DoUnselect()
        {
          OnUnSelect.Invoke(this);
          PostSelect();
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
