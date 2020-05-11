using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class RuntimeMoheData : IRuntimeMoheData
  {
    /*
     * Health
     * Progress w/ abilities
     */
    public RuntimeMoheData(IMohe Mohe)
    {
      baseMohe = Mohe;

      health = baseMohe.Stats.Health;
      abilities = new List<IRuntimeAbility>();
      foreach (IAbility abil in baseMohe.Abilities)
      {
        abilities.Add(new RuntimeAbility(abil));
      }
    }

    public int Health => health;
    public IMohe BaseMohe => baseMohe;
    public List<IRuntimeAbility> Abilities => abilities;

    private int health;
    private IMohe baseMohe;
    private List<IRuntimeAbility> abilities;

    public void PopulateAbilities(JewelID jewel, int amount)
    {
      foreach (IRuntimeAbility abil in abilities)
      {
        abil.PowerAbility(jewel, amount);
      }
    }

    public bool MoheDead()
    {
      return health <= 0;
    }
  }
}
