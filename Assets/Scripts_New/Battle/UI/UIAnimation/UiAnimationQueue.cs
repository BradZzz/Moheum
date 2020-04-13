using System;
using Patterns;
using UnityEngine;

namespace Battle.UI.UIAnimation
{
  public class UiAnimationQueue : TimeredCommandQueue<UiAnimationQueue, UiAnimationQueue.Animation>
  {
    public class Animation : Command
    {
      Action Animate = () => { };
      public string Id { get; }

      public Animation(Action animation)
      {
        AddListener(animation);
        Id = animation.Method.Name;
      }

      public override void Execute()
      {
        Animate?.Invoke();
        //            Debug.Log("Execute: "+ Id);
      }

      public void AddListener(Action listener)
      {
        if (listener == null)
          return;
        Animate += listener;
      }

      public override void Undo()
      {

      }
    }
  }
}
