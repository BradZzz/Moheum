//// Update is called once per frameusing SimpleCardGames.Data.Target;
//using UnityEngine;

//namespace Battle.Data.Effects
//{
//    /// <summary>
//    ///     Base class for all effects in the game.
//    /// </summary>
//    public abstract class BaseEffectData : ScriptableObject
//    {
//        public const string Path = "Data/Effect";

//        //---------------------------------------------------------------------------------------------------------------------

//        //[SerializeField]
//        //[Tooltip("Quantity of the effect.")]
//        //private int amount;
//        //public int Amount => amount;

//        [SerializeField] [Tooltip("Targets of this effect.")] private BaseTargetType target;
//        public BaseTargetType Target => target;

//        [SerializeField] private IntentionType intentionType;
//        public IntentionType IntentionType => intentionType;


//        //---------------------------------------------------------------------------------------------------------------------

//        /// <summary>
//        ///     Apply the effect into something which is able to take effects.
//        /// </summary>
//        /// <param name="targets"></param>
//        /// <param name="source"></param>
//        public abstract void Apply(ITargetable[] targets, IEffectable source);
//        //---------------------------------------------------------------------------------------------------------------------

//        #region Fields

//        //TODO: All these texts may be moved to a localization system and accessed via Ids.
//        [Header("Data")]
//        [SerializeField]
//        [Tooltip("A brief description of what it does. This text won't be show to the user")]
//        [Multiline]
//        private string description;

//        #endregion
//    }

//    public abstract class StaticEffectData : BaseEffectData
//    {
//        [SerializeField]
//        [Tooltip("Quantity of the effect.")]
//        private int amount;
//        public int Amount => amount;
//    }

//    public abstract class DynamicEffectData : BaseEffectData
//    {
//        public abstract int Amount(ITargetable target);
//    }
//}
//void Update()
//    {
        
//    }
//}
