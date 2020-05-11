using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IMoheData
  {
    string Name { get; }
    Sprite Image { get; }

    IMoheNature Nature { get; }
  }
}
