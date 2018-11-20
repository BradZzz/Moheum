using System;
using System.Collections.Generic;

[Serializable]
public class PlayerRosterMeta {
  public string name;
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

    this.skills = compSkills.ToArray();
  }

  public float getPower(){
    return lust + greed + wrath + pride + gluttony + sloth + envy;
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
