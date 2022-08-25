using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  [CreateAssetMenu(menuName = "Data/Ability")]
  public class AbilityData : ScriptableObject, IAbilityData
  {
    [SerializeField] private AbilityID abilityID;
    [SerializeField] private string abilityName;
    [TextArea] [SerializeField] private string description;

    [SerializeField] private JewelID triggerJewel;
    [SerializeField] private BaseEffect afterEffect;

    public AbilityID AbilityID => abilityID;
    public string AbilityName => abilityName;
    public string Description => description;

    public JewelID TriggerJewel => triggerJewel;
    public BaseEffect AfterEffect => afterEffect;

    [SerializeField] public List<AbilityCostData> abilityCost = new List<AbilityCostData>();

    [Serializable]
    public class AbilityCostData
    {
      [Tooltip("Jewel to charge")]
      public JewelID jewel;

      [Tooltip("Amount to charge")]
      [Range(1, 20)]
      public int amount;
    }
  }
}
