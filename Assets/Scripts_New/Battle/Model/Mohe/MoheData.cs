using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  [CreateAssetMenu(menuName = "Data/Mohe")]
  public class MoheData : ScriptableObject, IMoheData
  {
    [SerializeField] private MoheID moheID;
    [SerializeField] private string moheName;
    [TextArea] [SerializeField] private string description;
    [SerializeField] private Sprite artwork;

    //--------------------------------------------------------------------------------------------------------------
    public MoheID MoheID => moheID;
    public string Name => moheName;
    public string Description => description;
    public Sprite Artwork => artwork;

    public MoheStatData BaseStatsLow => baseStatsLow;
    public MoheStatData BaseStatsHigh => baseStatsHigh;
    public MoheStatData LvlUpStatsLow => lvlUpStatsLow;
    public MoheStatData LvlUpStatsHigh => lvlUpStatsHigh;
    public MoheExperienceGain ExperienceType => experienceType;
    public List<MoheAbilityLevelData> Abilities => abilities;

    [SerializeField] private MoheStatData baseStatsLow = new MoheStatData();
    [SerializeField] private MoheStatData baseStatsHigh = new MoheStatData();
    [SerializeField] private MoheStatData lvlUpStatsLow = new MoheStatData();
    [SerializeField] private MoheStatData lvlUpStatsHigh = new MoheStatData();
    [SerializeField] private MoheExperienceGain experienceType;

    [Serializable]
    public class MoheStatData
    {
      [Tooltip("Envy Stat")]
      [Range(0f, 10f)]
      public float envy;

      [Tooltip("Wrath Stat")]
      [Range(0f, 10f)]
      public float wrath;

      [Tooltip("Greed Stat")]
      [Range(0f, 10f)]
      public float greed;

      [Tooltip("Gluttony Stat")]
      [Range(0f, 10f)]
      public float gluttony;

      [Tooltip("Pride Stat")]
      [Range(0f, 10f)]
      public float pride;

      [Tooltip("Lust Stat")]
      [Range(0f, 10f)]
      public float lust;

      [Tooltip("Sloth Stat")]
      [Range(0f, 10f)]
      public float sloth;

      [Tooltip("Health Stat")]
      [Range(0f, 10f)]
      public float health;
    }

    [SerializeField] private List<MoheAbilityLevelData> abilities = new List<MoheAbilityLevelData>();

    [Serializable]
    public class MoheAbilityLevelData
    {
      [Tooltip("When Mohe Aquires")]
      [Range(0, 100)]
      public int lvl;

      [Tooltip("Health Stat")]
      public AbilityData ability;
    }
  }
}
