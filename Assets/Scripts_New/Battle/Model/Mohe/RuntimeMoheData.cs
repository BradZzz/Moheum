using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using Battle.Model.MoheModel.ExpTypes;
using Battle.Model.MoheModel.Mechanics;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class RuntimeMoheData : IRuntimeMoheData
  {
    /*
     * Health
     * Progress w/ abilities
     */
    public RuntimeMoheData(IMohe Mohe, PlayerSeat seat, int idx)
    {
      baseMohe = Mohe;

      playerSeat = seat;
      baseExpType = BaseExpType.TranslateType(baseMohe.Data.ExperienceType);
      instanceID = baseMohe.Data.MoheID.ToString() + playerSeat + idx.ToString();
      Health = (int)baseMohe.Stats.health;
      Exp = 0;
      abilities = new List<IRuntimeAbility>();
      List<AbilityData> db = AbilityDatabase.Instance.GetFullList();
      foreach (AbilityID abil in baseMohe.Abilities)
      {
        if (db?.Find(ability => ability.AbilityID == abil))
          abilities.Add(new RuntimeAbility(db?.Find(ability => ability.AbilityID == abil)));
      }

      DamageMoheMechanic = new DamageMoheMechanic(this);
      DeathMoheMechanics = new DeathMoheMechanic(this);
      GainExpMoheMechanic = new GainExpMoheMechanic(this);
      GainLvlMoheMechanic = new GainLvlMoheMechanic(this);
      GainStatusMoheMehanic = new GainStatusMoheMehanic(this);
      HealMoheMechanic = new HealMoheMechanic(this);
      LoseStatusMoheMechanic = new LoseStatusMoheMechanic(this);
    }

    public string InstanceID => instanceID;
    public int Health { get; set; }
    public int Exp { get; set; }
    public IMohe BaseMohe => baseMohe;
    public BaseExpType BaseExpType => baseExpType;
    public List<IRuntimeAbility> Abilities => abilities;
    public PlayerSeat PlayerSeat => playerSeat;

    private string instanceID;
    private IMohe baseMohe;
    private List<IRuntimeAbility> abilities;
    private BaseExpType baseExpType;
    private PlayerSeat playerSeat;

    private DamageMoheMechanic DamageMoheMechanic;
    private DeathMoheMechanic DeathMoheMechanics;
    private GainExpMoheMechanic GainExpMoheMechanic;
    private GainLvlMoheMechanic GainLvlMoheMechanic;
    private GainStatusMoheMehanic GainStatusMoheMehanic;
    private HealMoheMechanic HealMoheMechanic;
    private LoseStatusMoheMechanic LoseStatusMoheMechanic;

    public void PopulateAbilities(JewelID jewel, int amount)
    {
      foreach (IRuntimeAbility abil in abilities)
      {
        abil.PowerAbility(jewel, amount);
      }
    }

    public bool UseableAbility()
    {
      foreach (IRuntimeAbility abil in abilities)
      {
        if (abil.AbilityCharged())
        {
          return true;
        }
      }
      return false;
    }

    public bool MoheDead()
    {
      return Health <= 0;
    }
  }
}
