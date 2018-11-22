using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMain : MonoBehaviour
{
  //TreasureMain(TreasureMeta meta, treasType type){
  //  if (treasType.Board == type) {
  //    boardTreas = (BoardTreasMeta) meta;
  //  } else {
  //    monTreas = (MonTreasMeta) meta;
  //  } 
  //}

  void Start()
  {
    UpdateInteractable();
  }

  public enum treasType
  {
    Monster, Board, None
  };

  public MonTreasMeta monTreas;
  //public BoardTreasMeta boardTreas;
  public treasType tType;

  void OnTriggerEnter2D(Collider2D other)
  {
    if (tag.Equals("Player"))
    {
      GameObject.FindWithTag("Player").GetComponent<PlayerMain>().AddItem(gameObject);
      BaseSaver.putAdventure(GameObject.FindWithTag("Player").GetComponent<PlayerMain>().playerMeta);
      GetComponent<BoxCollider2D>().enabled = false;
      GetComponent<SpriteRenderer>().enabled = false;
    }
  }

  public void UpdateInteractable()
  {
    StartCoroutine(checkItemPickedUp());
  }

  IEnumerator checkItemPickedUp()
  {
    bool pickedUp = GameUtilities.getPickedUp(BaseSaver.getMap(), transform.position);
    //Debug.Log("Item: " + name);
    //Debug.Log("Picked Up: " + pickedUp.ToString());
    if (pickedUp)
    {
      //Destroy(this.gameObject);
      this.gameObject.SetActive(false);
    }
    yield return null;
  }
}
