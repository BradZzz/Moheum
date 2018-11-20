using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour {
  public AdventureMeta playerMeta;

  public void AddItem(GameObject item){
    playerMeta.AddToTreasureList(item.name,1);
    //if (playerMeta.treasure.ContainsKey(item.name)){
    //  playerMeta.treasure[item.name]++;
    //} else {
    //  playerMeta.treasure.Add(item.name, 1);
    //}
  }

  public PosMeta GetPos(){
    Vector3 pos = transform.position;
    return new PosMeta(pos.x, pos.y, pos.z);
  }

  private void Update()
  {
    transform.rotation = Quaternion.identity;
  }
}
