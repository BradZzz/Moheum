using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.UI.Utils;
using UnityEngine;
using UnityEngine.Animations;

namespace Battle.UI.Jewel.Component
{
  public class UIJewelAnimator : IUiJewelAnimator
  {
    public UIJewelAnimator(IUiJewelComponents parent)
    {
      parent.UIRuntimeData.OnSetData += Execute;
      animator = parent.Animator;
      animator.enabled = false;
      parent.OnPreRemove += OnPreRemoveJewel;
      parent.OnRemove += OnRemoveJewel;
    }

    Animator animator;
    private IRuntimeJewel data;

    public void Execute(IRuntimeJewel data)
    {
      animator.runtimeAnimatorController = data.Data.Animator;
      this.data = data;
    }

    public void OnPreRemoveJewel(IRuntimeJewel jewel)
    {
      if (jewel.JewelID == data.JewelID)
      {
        animator.enabled = true;
        animator.Play("Idle");
      }
    }

    public void OnRemoveJewel(IRuntimeJewel jewel)
    {
      if (jewel.JewelID == data.JewelID)
      {
        animator.enabled = false;
      }
    }
  }
}
