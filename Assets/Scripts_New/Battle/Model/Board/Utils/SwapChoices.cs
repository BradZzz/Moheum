using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Utils
{
  public class SwapChoices
  {
    public IRuntimeJewel jewel1;
    public IRuntimeJewel jewel2;
    public int matches;

    public SwapChoices(IRuntimeJewel Jewel1, IRuntimeJewel Jewel2, int Matches)
    {
      jewel1 = Jewel1;
      jewel2 = Jewel2;
      matches = Matches;
    }
  }
}
