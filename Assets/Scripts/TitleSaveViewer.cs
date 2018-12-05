using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSaveViewer : MonoBehaviour {

  public GameObject Power;
  public GameObject Temples;
  public GameObject Map;
  public GameObject MoheCnt;
  public GameObject Yen;
  public int fileIdx;

	// Use this for initialization
	void Start () {
    BaseSaver.putSaveNumber(fileIdx);
    AdventureMeta meta = BaseSaver.getAdventure();
    if (meta != null) {
      float pwr = 0;
      foreach (PlayerRosterMeta pM in meta.roster)
      {
        pwr += pM.getPower();
      }
      Power.GetComponent<Text>().text = "Roster Pwr: " + Math.Round(pwr, 2).ToString();
      Temples.GetComponent<Text>().text = "Temples: " + meta.temples.Length.ToString();
      Map.GetComponent<Text>().text = "Map: " + BaseSaver.getMap();
      MoheCnt.GetComponent<Text>().text = "Mohe #: " + (meta.roster.Length + meta.vault.Length).ToString();
      Yen.GetComponent<Text>().text = "¥" + meta.getYen().ToString();
    }
    else
    {
      Power.GetComponent<Text>().text = "";
      Temples.GetComponent<Text>().text = "";
      Map.GetComponent<Text>().text = "New Save";
      MoheCnt.GetComponent<Text>().text = "";
      Yen.GetComponent<Text>().text = "";
    }
  }
}
