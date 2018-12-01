using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class PlayerRosterMeta {
  public string name;
  public string nickname;
  public int lvl;
  public int exp;
  public int curHealth;
  public int maxHealth;

  public float lust;
  public float greed;
  public float wrath;
  public float pride;
  public float gluttony;
  public float sloth;
  public float envy;

  public float gluttony_bonus;

  public string[] skills;

  public PlayerRosterMeta(){
    
  }

  public PlayerRosterMeta(PlayerRosterMeta meta){
    this.name = meta.name;
    this.nickname = meta.nickname;
    this.lvl = meta.lvl;
    this.curHealth = meta.curHealth;
    this.maxHealth = meta.maxHealth;
    this.exp = meta.exp;

    List<string> compSkills = new List<string>();
    foreach(string skill in meta.skills){
      compSkills.Add(skill);
    }

    this.lust = meta.lust;
    this.greed = meta.greed;
    this.wrath = meta.wrath;
    this.pride = meta.pride;
    this.gluttony = meta.gluttony;
    this.sloth = meta.sloth;
    this.envy = meta.envy;

    this.gluttony_bonus = meta.gluttony_bonus;

    this.skills = compSkills.ToArray();
  }

  public float getPower(){
    return lust + greed + wrath + pride + gluttony + sloth + envy;
  }

  public void AddToLowest(int pwr){
    int[] stats = new int[] { (int)lust * 100, (int)greed * 100, (int)wrath * 100, (int)pride * 100, (int)gluttony * 100, (int)sloth * 100, (int)envy * 100 };
    int minIdx = Array.IndexOf(stats, stats.Min());
    switch(minIdx)
    {
      case 0:
        lust += 1;
        break;
      case 1:
        greed += 1;
        break;
      case 2:
        wrath += 1;
        break;
      case 3:
        pride += 1;
        break;
      case 4:
        gluttony += 1;
        break;
      case 5:
        sloth += 1;
        break;
      default:
        envy += 1;
        break;
    }
  }

  public override string ToString()
  {
    string buffer = "";
    buffer += "Name: " + name + "\n";
    buffer += "Lvl: " + lvl.ToString() + "\n";
    buffer += "Exp: " + exp.ToString() + "\n";
    buffer += "Health: " + curHealth.ToString() + "/" + maxHealth.ToString() + "\n";

    buffer += "Lust: " + lust.ToString() + "\n";
    buffer += "Greed: " + greed.ToString() + "\n";
    buffer += "Wrath: " + wrath.ToString() + "\n";
    buffer += "Pride: " + pride.ToString() + "\n";
    buffer += "Gluttony: " + gluttony.ToString() + "\n";
    buffer += "Sloth: " + sloth.ToString() + "\n";
    buffer += "Envy: " + envy.ToString() + "\n";

    buffer += "Skills\n";
    foreach(string skill in skills){
      buffer += "\t" + skill;
    }
    return buffer;
  }
}
