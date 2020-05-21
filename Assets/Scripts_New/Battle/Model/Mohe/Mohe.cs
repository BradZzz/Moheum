using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Battle.Model.MoheModel.MoheData;

namespace Battle.Model.MoheModel
{
  [Serializable]
  public class Mohe : IMohe
  {
    public Mohe (IMoheData Data, List<AbilityID> Abilities, MoheData.MoheStatData Stats)
    {
      data = Data;
      abilities = Abilities;
      stats = Stats;
    }

    public IMoheData Data => data;
    public List<AbilityID> Abilities => abilities;
    public MoheData.MoheStatData Stats => stats;

    private IMoheData data;
    private List<AbilityID> abilities;
    private MoheData.MoheStatData stats;
  }
}
