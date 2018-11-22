using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainerUpdateController : MonoBehaviour {

  public GameObject[] monsterUI;

  private void Start()
  {
    for (int i = 0; i < monsterUI.Length; i++)
    {
      monsterUI[i].SetActive(false);
    }
  }

  public void updateMonsterUI(PlayerRosterMeta[] roster, TrainerType tType){
    if (tType == TrainerType.Wild) {
      GameObject.Find("WildTxt").GetComponent<Text>().enabled = true;
    } else if (tType == TrainerType.Trainer)
    {
      GameObject.Find("WildTxt").GetComponent<Text>().enabled = false;
    }
    if (tType == TrainerType.Trainer || tType == TrainerType.Player) {
      for (int i = 0; i < 6; i++)
      {
        if (i < roster.Length)
        {
          monsterUI[i].SetActive(true);
          Color thisColor = monsterUI[i].transform.GetChild(0).gameObject.GetComponent<Image>().color;
          if (roster[i].curHealth > 0)
          {
            thisColor.a = 0;
          }
          else
          {
            thisColor.a = 1;
          }
          monsterUI[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = thisColor;
        }
        else
        {
          monsterUI[i].SetActive(false);
        }
      }
    }
  }

  public enum TrainerType {
    Player, Trainer, Wild, None
  }
}
