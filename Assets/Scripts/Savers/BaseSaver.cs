using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSaver {
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

    PlayerPrefs.SetString(SAVE_ADVENTURE, PlayerPrefs.GetString(ADVENTURE));
    PlayerPrefs.SetString(SAVE_BOARD, PlayerPrefs.GetString(BOARD + "_" + PlayerPrefs.GetString(MAP_NAME)));
    PlayerPrefs.SetString(SAVE_MAP_NAME, PlayerPrefs.GetString(MAP_NAME));

    Debug.Log("SAVE_ADVENTURE: " + PlayerPrefs.GetString(SAVE_ADVENTURE));
    Debug.Log("SAVE_BOARD: " + PlayerPrefs.GetString(SAVE_BOARD));
    Debug.Log("SAVE_MAP_NAME: " + PlayerPrefs.GetString(SAVE_MAP_NAME));
  }

  public static void restoreState()
  {
    Debug.Log("restoreState!");

    //BoardMeta board = BaseSaver.getBoard(BaseSaver.getMap());
    //board.playerPos = new PosMeta(-99999, -99999, -99999);
    //BaseSaver.putBoard(board);

    PlayerPrefs.SetString(ADVENTURE, PlayerPrefs.GetString(SAVE_ADVENTURE));
    PlayerPrefs.SetString(BOARD + "_" + PlayerPrefs.GetString(SAVE_MAP_NAME), PlayerPrefs.GetString(SAVE_BOARD));
    PlayerPrefs.SetString(MAP_NAME, PlayerPrefs.GetString(SAVE_MAP_NAME));
    PlayerPrefs.SetString(MAP_NAME_PREV,"");

    Debug.Log("ADVENTURE: " + PlayerPrefs.GetString(ADVENTURE));
    Debug.Log("BOARD: " + PlayerPrefs.GetString(BOARD));
    Debug.Log("MAP_NAME: " + PlayerPrefs.GetString(MAP_NAME));
  }

  /*
   * ADVENTURE
   */

  public static void resetAdventure()
  {
    PlayerPrefs.SetString(ADVENTURE, "");
    PlayerPrefs.SetString(SAVE_ADVENTURE, "");

    Debug.Log("ADVENTURE RESET");
  }

  public static void putAdventure(AdventureMeta info)
  {
    string json = JsonUtility.ToJson(info);
    PlayerPrefs.SetString(ADVENTURE, json);

    Debug.Log("ADVENTURE set: " + json);
  }


  public static AdventureMeta getAdventure()
  {
    string json = PlayerPrefs.GetString(ADVENTURE);
    if (json.Length == 0)
    {
      return null;
    }
    Debug.Log("ADVENTURE got: " + json);
    return JsonUtility.FromJson<AdventureMeta>(json);
  }

  /*
   * BOARD
   */

  public static void resetBOARD()
  {
    PlayerPrefs.SetString(BOARD, "");
    PlayerPrefs.SetString(SAVE_BOARD, "");

    Debug.Log("BOARD RESET");
  }

  public static void putBoard(BoardMeta info)
  {
    //Debug.Log("Putting board at: " + BOARD + "_" + info.mapName);
    string json = JsonUtility.ToJson(info);
    PlayerPrefs.SetString(BOARD + "_" + info.mapName, json);
    Debug.Log("BOARD put: " + json);
    updateNPCS();
  }


  public static BoardMeta getBoard(string name)
  {
    //Debug.Log("Getting board at: " + BOARD + "_" + name);
    string json = PlayerPrefs.GetString(BOARD + "_" + name);
    //Debug.Log("BOARD got: " + json);
    if (json.Length == 0)
    {
      return null;
    }
    return JsonUtility.FromJson<BoardMeta>(json);
  }

  /*
   * COMPUTER
   */

  public static void resetCOMPUTER()
  {
    PlayerPrefs.SetString(COMPUTER, "");

    Debug.Log("COMPUTER RESET");
  }

  public static void putComputer(PlayerRosterMeta[] info)
  {
    string json = JsonHelper.ToJson(info);
    PlayerPrefs.SetString(COMPUTER, json);

    Debug.Log("COMPUTER set: " + json);

    updateNPCS();
  }


  public static PlayerRosterMeta[] getComputer()
  {
    string json = PlayerPrefs.GetString(COMPUTER);
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
    return PlayerPrefs.GetString(MAP_NAME);
  }

  public static string getMapPrev()
  {
    return PlayerPrefs.GetString(MAP_NAME_PREV);
  }

  public static string getMapConnection()
  {
    return PlayerPrefs.GetString(MAP_CONNECTION);
  }

  public static void setMapName(string name)
  {
    setMapPrevName(getMap());
    PlayerPrefs.SetString(MAP_NAME, name);
    //Debug.Log("Map Set: " + name);
  }

  public static void setMapPrevName(string name)
  {
    PlayerPrefs.SetString(MAP_NAME_PREV, name);
    //Debug.Log("Map Set Prev: " + name);
  }

  public static void setMapConnection(string name)
  {
    PlayerPrefs.SetString(MAP_CONNECTION, name);
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
