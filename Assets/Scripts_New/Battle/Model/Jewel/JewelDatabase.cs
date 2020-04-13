using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Patterns;
using UnityEngine;

namespace Battle.Model.Jewel
{
    public class JewelDatabase : Singleton<JewelDatabase>
    {
        private const string PathDataBase = "Battle/Jewel";
        private List<JewelData> Jewels { get; }

        public JewelDatabase()
        {
            Jewels = Resources.LoadAll<JewelData>(PathDataBase).ToList();
        }

        public JewelData Get(JewelID id)
        {
            return Jewels?.Find(relic => relic.JewelID == id);
        }

        public List<JewelData> GetFullList()
        {
            return Jewels;
        }
    }
}