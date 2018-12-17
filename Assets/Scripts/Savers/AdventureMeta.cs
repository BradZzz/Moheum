using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AdventureMeta
{
  public int yen;
  public int expBuff;
  public string[] treasure;
  public string[] temples;

  public PlayerRosterMeta[] roster;
  public PlayerRosterMeta[] vault;

  public NPCMeta trainer;
  public PlayerRosterMeta wild;
  public bool isTrainerEncounter;

  public void addYen(int val){
    Debug.Log("Adding Yen: " + val.ToString());
    yen += val;
    Debug.Log("Current Yen: " + yen.ToString());
  }

  public int getYen(){
    return yen;
  }

  public void UpdateTempleList(string name)
  {
    string temple = GameUtilities.GetTempleFromTrainer(name);
    if (temple.Length > 0)
    {
      List<string> tmp = new List<string>(temples);
      tmp.Add(temple);
      temples = tmp.ToArray();
    }
  }

  public void SwitchInVault(int rosterPos, int vaultPos){
    PlayerRosterMeta temp = roster[rosterPos];
    roster[rosterPos] = vault[vaultPos];
    vault[vaultPos] = temp;
  }

  public Dictionary<string, int> GetTreasureList(){
    Dictionary<string, int> treasureDict = new Dictionary<string, int>();
    foreach (string item in treasure){
      string[] split = item.Split(',');
      treasureDict.Add(split[0],int.Parse(split[1]));
    }
    return treasureDict;
  }

  public void AddToTreasureList(string name, int qty){
    Dictionary<string, int> treasureDict = GetTreasureList();
    if (treasureDict.ContainsKey(name))
    {
      treasureDict[name]+= qty;
      if (treasureDict[name] <= 0) {
        treasureDict.Remove(name);
      }
    }
    else
    {
      treasureDict.Add(name, 1);
    }
    List<string> newTreasure = new List<string>();
    foreach(string key in treasureDict.Keys){
      newTreasure.Add(key +","+ treasureDict[key]);
    }
    treasure = newTreasure.ToArray();
  }

  //public override string ToString()
  //{
  //  string buffer = "";
  //  buffer += "Name: " + name + "\n";
  //  buffer += "Lvl: " + lvl.ToString() + "\n";
  //  buffer += "Exp: " + exp.ToString() + "\n";
  //  buffer += "isTrainerEncounter: " + isTrainerEncounter.ToString() + "\n";

  //  buffer += "Lust: " + lust.ToString() + "\n";
  //  buffer += "Greed: " + greed.ToString() + "\n";
  //  buffer += "Wrath: " + wrath.ToString() + "\n";
  //  buffer += "Pride: " + pride.ToString() + "\n";
  //  buffer += "Gluttony: " + gluttony.ToString() + "\n";
  //  buffer += "Sloth: " + sloth.ToString() + "\n";
  //  buffer += "Envy: " + envy.ToString() + "\n";

  //  buffer += "Skills\n";
  //  foreach (string skill in skills)
  //  {
  //    buffer += "\t" + skill;
  //  }
  //  return buffer;
  //}
}
