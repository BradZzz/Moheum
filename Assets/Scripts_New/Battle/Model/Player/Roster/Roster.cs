using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.MoheModel;
using UnityEngine;

namespace Battle.Model.Player
{
  public class Roster : IRoster
  {
    int MAX_ROSTER_SIZE = 0;

    public Roster(List<IRuntimeMoheData> MoheRoster)
    {
      currentIdx = 0;
      moheRoster = MoheRoster;
    }


    public int CurrentIdx => currentIdx;
    public List<IRuntimeMoheData> MoheRoster => moheRoster;
    public bool AllVanquished => allVanquished();

    private int currentIdx;
    private List<IRuntimeMoheData> moheRoster;

    public IRuntimeMoheData CurrentMohe()
    {
      return MoheRoster[CurrentIdx];
    }

    // Set current roster position
    public void SetRoster(int idx)
    {
      if (moheRoster.Count > idx && !AllVanquished)
      {
        if (!moheRoster[idx].MoheDead())
        {
          currentIdx = idx;
        } else {
          int nxt = NextAvailableMohe();
          if (nxt >= 0)
          {
            currentIdx = nxt;
          }
        }
      }
    }

    public void AutoRoster()
    {
      if (!AllVanquished)
      {
        currentIdx = NextAvailableMohe();
      }
    }

    // Checks to see if roster is completed
    public bool allVanquished()
    {
      bool allVanquished = true;
      foreach (IRuntimeMoheData mohe in MoheRoster)
      {
        if (!mohe.MoheDead())
        {
          allVanquished = false;
          break;
        }
      }
      return allVanquished;
    }

    // Checks for next available Mohe
    private int NextAvailableMohe()
    {
      for (int i = 0; i < MoheRoster.Count; i++)
      {
        if (!MoheRoster[i].MoheDead())
        {
          return i;
        }
      }
      return -1;
    }
  }
}
