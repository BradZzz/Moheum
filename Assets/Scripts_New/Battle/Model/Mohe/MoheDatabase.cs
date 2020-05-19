using System.Collections.Generic;
using System.Linq;
using Patterns;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class MoheDatabase : Singleton<MoheDatabase>
  {
    private const string PathDataBase = "Battle/Mohe";
    private List<MoheData> Mohe { get; }

    public MoheDatabase()
    {
      Mohe = Resources.LoadAll<MoheData>(PathDataBase).ToList();
    }

    public MoheData Get(MoheID id)
    {
      return Mohe?.Find(jewel => jewel.MoheID == id);
    }

    public List<MoheData> GetFullList()
    {
      return Mohe;
    }
  }
}