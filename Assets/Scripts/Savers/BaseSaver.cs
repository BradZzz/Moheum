using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSaver {
  private static string SAVE_NUMBER = "SAVE_NUMBER";

  private static string ADVENTURE = "ADVENTURE";
  private static string BOARD = "BOARD";
  private static string MAP_NAME = "MAP_NAME";
  private static string SAVE_ADVENTURE = "SAVE_ADVENTURE";
  private static string SAVE_BOARD = "SAVE_BOARD";
  private static string SAVE_MAP_NAME = "SAVE_MAP_NAME";
  private static string COMPUTER = "COMPUTER";
  private static string MAP_NAME_PREV = "MAP_NAME_PREV";
  private static string MAP_CONNECTION = "MAP_CONNECTION";

  private static string SLIDESHOW_NAME = "SLIDESHOW_NAME";

  public static void saveState(){
    Debug.Log("saveState!");

    PlayerPrefs.SetString(adjKy(SAVE_ADVENTURE), JsonUtility.ToJson(getAdventure()));
    PlayerPrefs.SetString(adjKy(SAVE_BOARD), JsonUtility.ToJson(getBoard(getMap())));
    PlayerPrefs.SetString(adjKy(SAVE_MAP_NAME), getMap());

    Debug.Log("SAVE_ADVENTURE: " + PlayerPrefs.GetString(adjKy(SAVE_ADVENTURE)));
    Debug.Log("SAVE_BOARD: " + PlayerPrefs.GetString(adjKy(SAVE_BOARD)));
    Debug.Log("SAVE_MAP_NAME: " + PlayerPrefs.GetString(adjKy(SAVE_MAP_NAME)));
  }

  public static void restoreState()
  {
    Debug.Log("restoreState!");

    PlayerPrefs.SetString(adjKy(ADVENTURE), PlayerPrefs.GetString(SAVE_ADVENTURE));
    PlayerPrefs.SetString(adjKy(BOARD + "_" + getMap()), PlayerPrefs.GetString(SAVE_BOARD));
    PlayerPrefs.SetString(adjKy(MAP_NAME), adjKy(SAVE_MAP_NAME));
    PlayerPrefs.SetString(adjKy(MAP_NAME_PREV), "");

    Debug.Log("ADVENTURE: " + getAdventure());
    Debug.Log("BOARD: " + getBoard(getMap()));
    Debug.Log("MAP_NAME: " + getMap());
  }

  public static string adjKy(string key)
  {
    return key + getSN();
  }

  public static void ResetKeys(int save)
  {
    putSaveNumber(save);
    PlayerPrefs.SetString(adjKy(ADVENTURE), "");
    PlayerPrefs.SetString(adjKy(BOARD), "");
    PlayerPrefs.SetString(adjKy(MAP_NAME), "");
    PlayerPrefs.SetString(adjKy(SAVE_ADVENTURE), "");
    PlayerPrefs.SetString(adjKy(SAVE_BOARD), "");
    PlayerPrefs.SetString(adjKy(SAVE_MAP_NAME), "");
    PlayerPrefs.SetString(adjKy(COMPUTER), "");
    PlayerPrefs.SetString(adjKy(MAP_NAME_PREV), "");
    PlayerPrefs.SetString(adjKy(MAP_CONNECTION), "");
  }

  /*
   * SAVE NUMBER
   */

  public static string getSN()
  {
    return PlayerPrefs.GetString(SAVE_NUMBER);
  }

  public static void putSaveNumber(int save)
  {
    PlayerPrefs.SetString(SAVE_NUMBER, save.ToString());

    Debug.Log("Save set: " + save.ToString());
  }

  /*
   * ADVENTURE
   */

  //public static void resetAdventure()
  //{
  //  PlayerPrefs.SetString(ADVENTURE, "");
  //  PlayerPrefs.SetString(SAVE_ADVENTURE, "");

  //  Debug.Log("ADVENTURE RESET");
  //}

  public static void putAdventure(AdventureMeta info)
  {
    string json = JsonUtility.ToJson(info);
    PlayerPrefs.SetString(adjKy(ADVENTURE), json);

    Debug.Log("ADVENTURE set: " + adjKy(ADVENTURE) + ":" + json);
  }


  public static AdventureMeta getAdventure()
  {
    string json = PlayerPrefs.GetString(adjKy(ADVENTURE)); 
    Debug.Log("ADVENTURE got: " + adjKy(ADVENTURE) + ":" + json);
    if (json.Length == 0)
    {
      return null;
    }
    return JsonUtility.FromJson<AdventureMeta>(json);
  }

  /*
   * BOARD
   */

  //public static void resetBOARD()
  //{
  //  PlayerPrefs.SetString(BOARD, "");
  //  PlayerPrefs.SetString(SAVE_BOARD, "");

  //  Debug.Log("BOARD RESET");
  //}

  public static void putBoard(BoardMeta info)
  {
    Debug.Log("Set board at: " + adjKy(BOARD) + "_" + info.mapName);
    string json = JsonUtility.ToJson(info);
    PlayerPrefs.SetString(adjKy(BOARD) + "_" + info.mapName, json);
    Debug.Log("BOARD put: " + json);
    updateNPCS();
  }


  public static BoardMeta getBoard(string name)
  {
    Debug.Log("Getting board at: " + adjKy(BOARD) + "_" + name);
    string json = PlayerPrefs.GetString(adjKy(BOARD) + "_" + name);
    Debug.Log("BOARD got: " + json);
    if (json.Length == 0)
    {
      return null;
    }
    return JsonUtility.FromJson<BoardMeta>(json);
  }

  /*
   * COMPUTER
   */

  //public static void resetCOMPUTER()
  //{
  //  PlayerPrefs.SetString(COMPUTER, "");

  //  Debug.Log("COMPUTER RESET");
  //}

  public static void putComputer(PlayerRosterMeta[] info)
  {
    string json = JsonHelper.ToJson(info);
    PlayerPrefs.SetString(adjKy(COMPUTER), json);

    Debug.Log("COMPUTER set: " + json);

    updateNPCS();
  }


  public static PlayerRosterMeta[] getComputer()
  {
    string json = PlayerPrefs.GetString(adjKy(COMPUTER));
    if (json.Length == 0)
    {
      return new PlayerRosterMeta[0];
    }
    Debug.Log("COMPUTER got");
    return JsonHelper.FromJson<PlayerRosterMeta>(json);
  }

  /*
   * MAP
   */

  public static string getMap()
  {
    string str = PlayerPrefs.GetString(adjKy(MAP_NAME));
    Debug.Log("Map: " + str);
    return str;
  }

  public static string getMapPrev()
  {
    string str = PlayerPrefs.GetString(adjKy(MAP_NAME_PREV));
    Debug.Log("Map Prev: " + str);
    return str;
  }

  public static string getMapConnection()
  {
    string str = PlayerPrefs.GetString(adjKy(MAP_CONNECTION));
    Debug.Log("Map Connection: " + str);
    return str;
  }

  public static void setMapName(string name)
  {
    setMapPrevName(getMap());
    PlayerPrefs.SetString(adjKy(MAP_NAME), name);
    //Debug.Log("Map Set: " + name);
  }

  public static void setMapPrevName(string name)
  {
    PlayerPrefs.SetString(adjKy(MAP_NAME_PREV), name);
    //Debug.Log("Map Set Prev: " + name);
  }

  public static void setMapConnection(string name)
  {
    PlayerPrefs.SetString(adjKy(MAP_CONNECTION), name);
    //Debug.Log("Connection Set: " + name);
  }

  public static void updateNPCS(){
    //Debug.Log("updateNPCS");

    foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
    {
      npc.GetComponent<NPCMain>().UpdateNPC();
    }
    foreach (GameObject npc in GameObject.FindGameObjectsWithTag("QInteractable"))
    {
      npc.GetComponent<InteractableMain>().UpdateInteractable();
    }
    foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
    {
      item.GetComponent<TreasureMain>().UpdateInteractable();
    }
  }

  /*
   * SLIDESHOW
   */

  public static void resetSLIDESHOW()
  {
    PlayerPrefs.SetString(SLIDESHOW_NAME, "");
    Debug.Log("SLIDESHOW RESET");
  }

  public static void putSlideshow(string info)
  {
    PlayerPrefs.SetString(SLIDESHOW_NAME, info);
    Debug.Log("SLIDESHOW set: " + info);
  }


  public static string getSlideshow()
  {
    return PlayerPrefs.GetString(SLIDESHOW_NAME);
  }
}
