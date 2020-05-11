using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public interface IMoheStats
  {
    int Health { get; }

    int Envy { get; }
    int Wrath { get; }
    int Greed { get; }
    int Gluttony { get; }
    int Pride { get; }
    int Lust { get; }
    int Sloth { get; }
  }
}
