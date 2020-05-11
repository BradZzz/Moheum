using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class MoheNature : IMoheNature
  {
    public MoheNature()
    {

    }
    /*
     * Base Stats
     * How the Mohe levels up
     */
    public IMoheStats BaseStatsLow => baseStatsLow;
    public IMoheStats BaseStatsHigh => baseStatsHigh;
    public IMoheStats LvlUpStatsLow => lvlUpStatsLow;
    public IMoheStats LvlUpStatsHigh => lvlUpStatsHigh;

    private IMoheStats baseStatsLow;
    private IMoheStats baseStatsHigh;
    private IMoheStats lvlUpStatsLow;
    private IMoheStats lvlUpStatsHigh;
  }
}
