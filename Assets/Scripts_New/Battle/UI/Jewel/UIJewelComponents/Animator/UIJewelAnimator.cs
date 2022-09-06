using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.Jewel.Component
{
  public class UIJewelAnimator : IUiJewelAnimator
  {
    public UIJewelAnimator(IUiJewelComponents parent)
    {
      parent.UIRuntimeData.OnSetData += Execute;
      this.animator = parent.Animator;
    }

    Animator animator;

    public void Execute(IRuntimeJewel data)
    {
      animator.runtimeAnimatorController = data.Data.Animator;
      animator.StartPlayback();
      /*if (data.Data.JewelID == JewelID.wrath)
      {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
      }*/
    }
  }
}
