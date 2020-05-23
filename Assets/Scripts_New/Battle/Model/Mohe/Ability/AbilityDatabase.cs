using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Patterns;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class AbilityDatabase : Singleton<AbilityDatabase>
  {
    private const string PathDataBase = "Battle/Abilities";
    private List<AbilityData> Abilities { get; }

    public AbilityDatabase()
    {
      Abilities = Resources.LoadAll<AbilityData>(PathDataBase).ToList();
    }

    public AbilityData Get(AbilityID id)
    {
      return Abilities?.Find(ability => ability.AbilityID == id);
    }

    public List<AbilityData> GetFullList()
    {
      return Abilities;
    }
  }
}
