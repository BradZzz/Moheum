using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Model.MoheModel;
using UnityEngine;

namespace Battle.Model.Item
{
  [CreateAssetMenu(menuName = "Data/Item")]
  public class ItemData : ScriptableObject, IItemData
  {
    /*
     * id
     * ability effect
     * name
     * image
     * description
     */
    
    
    [SerializeField] private ItemID itemID;
    [SerializeField] private string itemName;
    [TextArea] [SerializeField] private string description;
    [SerializeField] private Sprite artwork;

    //--------------------------------------------------------------------------------------------------------------
    public ItemID ItemID => itemID;
    public string Name => itemName;
    public string Description => description;
    public Sprite Artwork => artwork;
    public List<ItemAbilityEffects> Abilities => abilities;

    [SerializeField] private List<ItemAbilityEffects> abilities = new List<ItemAbilityEffects>();

    [Serializable]
    public class ItemAbilityEffects
    {
      [Tooltip("Item Ability")]
      public AbilityData ability;
    }
  }
}
