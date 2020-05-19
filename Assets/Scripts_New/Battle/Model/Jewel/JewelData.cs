using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

namespace Battle.Model.Jewel
{
    [CreateAssetMenu(menuName = "Data/Jewel")]
    public class JewelData : ScriptableObject, IJewelData
    {
        [SerializeField] private JewelID jewelID;
        [SerializeField] private string jewelName;
        [TextArea] [SerializeField] private string description;
        [TextArea] [SerializeField] private string lore;
        [SerializeField] private Sprite artwork;
        //[SerializeField] private RelicRarityType rType;
        //[FormerlySerializedAs("dataEffects")] [SerializeField] private EffectsSet effects;

        //--------------------------------------------------------------------------------------------------------------
        public JewelID JewelID => jewelID;
        public string Name => jewelName;
        public string Description => description;
        public string Lore => lore;
        public Sprite Artwork => artwork;

    }
}