using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.UI.Jewel;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Model.Jewel
{
    ////For generating the relics on the gameboard
    //public class UiJewelFactory : PrefabPooler<UiJewelFactory>
    //{
    //    public JewelDatabase Database => JewelDatabase.Instance;
    //    private IUiJewelController jewelController; // This is the place where all the jewels are controlled

    //    //Loop through the relics and instantiate
    //    //TODO: Right now this is instantiating all the relics, eventually we need to instantiate only relics that the player owns
    //    public List<IUiJewel> InstantiateRelicDatabase()
    //    {
    //        List<IUiJewel> jewels = new List<IUiJewel>();
    //        jewelController = transform.parent.GetComponent<IUiJewelController>();

    //        //List<JewelID> playerJewels = new List<JewelID>(BaseSaver.GetGameData().player.relics);
    //        List<JewelID> playerJewels = new List<JewelID>();

    //        foreach (var jewel in Database.GetFullList())
    //        {
    //            GameObject instantiatedRelic = Instantiate(modelsPooled[0], transform);

    //            //UiRelicDataComponent relicDataComponent = instantiatedRelic.GetComponent<UiRelicDataComponent>();
    //            //UiRelicComponent relicComponent = instantiatedRelic.GetComponent<UiRelicComponent>();
    //            //RuntimeRelic runtimeRelic = new RuntimeRelic(relic, relicComponent);

    //            //IUiRelic uiRel = instantiatedRelic.GetComponent<IUiRelic>();
    //            //relics.Add(uiRel);
    //            //relicDataComponent.SetData(runtimeRelic);

    //            //instantiatedRelic.GetComponent<RelicEventListener>().Init(runtimeRelic, relicComponent);

    //            IUiJewel uiJewel = instantiatedRelic.GetComponent<IUiJewel>();
    //            jewels.Add(uiJewel);
    //        }
    //        return jewels;
    //    }
    //}

    public class UiJewelPool : PrefabPooler<UiJewelPool>
    {
        public JewelDatabase Database => JewelDatabase.Instance;

        public IUiJewel Get(IRuntimeJewel jewel)
        {
            var obj = Get<IUiJewel>();
            obj.SetData(jewel.Data);

              //.Data.SetData(jewel);
            //busyObjects[prefabModel].Add(pooledObj);

            //pooledObj.SetActive(true);
            //OnPool(pooledObj);

            //return pooledObj;

            //GameObject instantiatedJewel = Instantiate(modelsPooled[0], transform);
            //instantiatedJewel.SetActive(true);
            //base.OnPool(instantiatedJewel);
            //IUiJewel uiJewel = instantiatedJewel.GetComponent<IUiJewel>();
            return obj;
        }

        protected override void OnRelease(GameObject prefabModel)
        {
            var cardUi = prefabModel.GetComponent<IUiJewel>();
            //cardUi.Restart();
            base.OnRelease(prefabModel);
        }
    }
}