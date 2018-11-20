using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainerGameoverController : MonoBehaviour {

  public Text header;
  public Text rewards;
  public GameObject[] monsterUI;

  //private void Start()
  //{
  //  for (int i = 0; i < monsterUI.Length; i++)
  //  {
  //    monsterUI[i].SetActive(false);
  //  }
  //}

  public void updateMonsterUI(NPCMeta meta, Glossary glossy, int yenGained)
  {
    header.text = "" + meta.name;
    rewards.text = "Looted for: ¥" + yenGained.ToString();
    for (int i = 0; i < 6; i++)
    {
      if (i < meta.roster.Length)
      {
        monsterUI[i].SetActive(true);
        monsterUI[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = glossy.GetMonsterImage(meta.roster[i].name);

        Color deadC = monsterUI[i].transform.GetChild(1).gameObject.GetComponent<Image>().color;
        if (meta.roster[i].curHealth > 0)
        {
          deadC.a = 0;
        }
        else
        {
          deadC.a = 1;
        }
        monsterUI[i].transform.GetChild(1).gameObject.GetComponent<Image>().color = deadC;
      }
      else
      {
        monsterUI[i].SetActive(false);
      }
    }
  }
}
