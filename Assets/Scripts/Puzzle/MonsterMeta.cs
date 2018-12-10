using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class MonsterMeta {
  // Monster name
  public string name;
  // Monster lvl
  public int lvl;
  public int exp;
  // Health
  public int mxHealth;
  public int currHealth;

//- [ ] Pride => Receive bigger bonuses when matching 4/5 gems
//- [ ] Greed => Destroy more gems with skill
//- [ ] Lust => Change more gems with skill
//- [ ] Envy => Increases level gains by a small amount. Sabotage skills more effective. 
//- [ ] Gluttony => Receive more health on lvl up
//- [ ] Wrath => Deal more damage with fight gems
//- [ ] Sloth => Reset / Heal / Damage skills more effective

  // Skill Gains / Losses for lvlSpeed
  //1.25,     .75,  .25,        -.25,       -.75, -1.25
  //VerySlow, Slow, MediumSlow, MediumFast, Fast, VeryFast

  public float lust;
  public float greed;
  public float wrath;
  public float pride;
  public float gluttony;
  public float sloth;
  public float envy;

  public elements[] strengths;
  public elements[] weaknesses;
  public string[] skills;
  public lvlUpSkills[] skillsGainedOnLvlUp;
  public lvlUpSpeed lvlSpeed;

  [HideInInspector]
  public int maxCatchTurns;
  [HideInInspector]
  public int currCatchTurns;

  [HideInInspector]
  public SkillReq[] catchReq;

  public PlayerRosterMeta toAbbrev(){
    PlayerRosterMeta meta = new PlayerRosterMeta();
    meta.name = name;
    meta.lvl = lvl;
    meta.exp = exp;
    meta.curHealth = currHealth;
    meta.maxHealth = mxHealth;
    meta.skills = skills;
    return meta;
  }

  public static int getExperience(PlayerRosterMeta monster, bool wild){
    float wildMonster = wild ? 1.15f : 1.7f;
    float effortPoints = (monster.maxHealth + monster.lvl + monster.getPower()) * 11;
    return (int) ((wildMonster * effortPoints) / 7);
  }

  /*
   * TODO: 
   * Generate Monster given monster name and level
   * Add a function that returns a float[] with the level updates for the monster
   */

  public static PlayerRosterMeta returnMonster(MonsterMeta meta, int lvl, bool wild){
    PlayerRosterMeta newMonster = meta.toAbbrev();
    List<String> skills = new List<string>();
    for (int i = 1; i <= lvl; i++){
      float[] updates = returnLvlUpdates(meta, newMonster);
      newMonster.lust += updates[0] * (wild ? .85f : 1f);
      newMonster.greed += updates[1] * (wild ? .85f : 1f);
      newMonster.wrath += updates[2] * (wild ? .85f : 1f);
      newMonster.pride += updates[3] * (wild ? .85f : 1f);
      newMonster.gluttony += updates[4] * (wild ? .85f : 1f);
      newMonster.sloth += updates[5] * (wild ? .85f : 1f);
      newMonster.envy += updates[6] * (wild ? .85f : 1f);

      newMonster.gluttony_bonus += newMonster.gluttony + .55f;
      if (newMonster.gluttony_bonus >= 1) {
        newMonster.maxHealth += (int)newMonster.gluttony_bonus;
        newMonster.gluttony_bonus = newMonster.gluttony_bonus - ((int)newMonster.gluttony_bonus);
      }

      foreach(lvlUpSkills skill in meta.skillsGainedOnLvlUp){
        if (skill.lvl == i) {
          skills.Add(skill.skill);
          while (skills.Count > 4) {
            skills.RemoveAt(UnityEngine.Random.Range(0,skills.Count));
          }
        }
      }
      // Level 1 monsters don't have any exp
      if (i>1) {
        int[] lvlExp = CalcLvl(newMonster, meta.lvlSpeed);
        newMonster.exp += lvlExp[2];
      }
    }
    newMonster.lvl = lvl;
    newMonster.curHealth = newMonster.maxHealth;
    string[] monsterSkills = skills.ToArray();
    GameUtilities.ShuffleArray(monsterSkills);
    newMonster.skills = monsterSkills;
    return newMonster;
  }

  public static float[] returnLvlUpdates(MonsterMeta meta, PlayerRosterMeta monster){

    /*
     * The normal range is .1 increase with a 20% differential.
     * All stats = stat * .1 * 20% differential
     * Every point of envy adds a point to the differential in the upwards direction
     */

    float envy_mod = ((meta.strengths[0] == elements.Wind) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Wind) ? .1f : 0) + 
      ((meta.strengths[0] == elements.Lightning) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Lightning) ? -.1f : 0);
    float lust_mod = ((meta.strengths[0] == elements.Magma) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Magma) ? .1f : 0) +
      ((meta.strengths[0] == elements.Disease) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Disease) ? -.1f : 0);
    float greed_mod = ((meta.strengths[0] == elements.Lightning) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Lightning) ? .1f : 0) +
      ((meta.strengths[0] == elements.Sea) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Sea) ? -.1f : 0);
    float wrath_mod = ((meta.strengths[0] == elements.Fight) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Fight) ? .1f : 0);
    float pride_mod = ((meta.strengths[0] == elements.Sea) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Sea) ? .1f : 0) +
      ((meta.strengths[0] == elements.Magma) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Magma) ? -.1f : 0);
    float gluttony_mod = ((meta.strengths[0] == elements.Disease) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Disease) ? .1f : 0) +
      ((meta.strengths[0] == elements.Nature) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Nature) ? -.1f : 0);
    float sloth_mod = ((meta.strengths[0] == elements.Nature) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Nature) ? .1f : 0) +
      ((meta.strengths[0] == elements.Wind) || (meta.strengths.Length > 1 && meta.strengths[1] == elements.Wind) ? -.1f : 0);

    float min = .8f + monster.envy / 10;
    float max = 1.2f + monster.envy / 10;

    float lust = (1 + meta.lust) * .1f * UnityEngine.Random.Range(min,max);
    float greed = (1 + meta.greed) * .1f * UnityEngine.Random.Range(min,max);
    float wrath = (1 + meta.wrath) * .1f * UnityEngine.Random.Range(min,max);
    float pride = (1 + meta.pride) * .1f * UnityEngine.Random.Range(min,max);
    float gluttony = (1 + meta.gluttony) * .1f * UnityEngine.Random.Range(min,max);
    float sloth = (1 + meta.sloth) * .1f * UnityEngine.Random.Range(min,max);
    float envy = (1 + meta.envy) * .1f * UnityEngine.Random.Range(min,max);

    return new float[] { lust, greed, wrath, pride, gluttony, sloth, envy };
  }

  //Returns [level, currentLvlExp, neededExp]
  public static int[] CalcLvl(PlayerRosterMeta meta, lvlUpSpeed speed){
    int cntExp = meta.exp;
    int thisLvl = 2;
    while(cntExp >= 0){
      int neededExp = 0;
      switch (speed)
      {
        case lvlUpSpeed.VerySlow: 
          if (thisLvl <= 15) {
            neededExp = (int) (Math.Pow(thisLvl, 3f) * (((thisLvl + 1)/3 + 24) / 50));
          } else if (15 < thisLvl && thisLvl <= 36) {
            neededExp = (int)(Math.Pow(thisLvl, 3f) * ((thisLvl + 14) / 50));
          } else {
            neededExp = (int)(Math.Pow(thisLvl, 3f) * (((thisLvl) / 2 + 32) / 50));
          }
          break;
        case lvlUpSpeed.Slow: 
          neededExp = (int)(5 * Math.Pow(thisLvl, 3f) / 4);
          break;
        case lvlUpSpeed.MediumSlow: 
          neededExp = (int)((6 * Math.Pow(thisLvl, 3f) / 5) - 15 * Math.Pow(thisLvl, 2f) + 100 * thisLvl - 140);
          break;
        case lvlUpSpeed.MediumFast: 
          neededExp = (int) Math.Pow(thisLvl, 3f);
          break;
        case lvlUpSpeed.Fast: 
          neededExp = (int)(4 * Math.Pow(thisLvl, 3f) / 5);
          break;
        case lvlUpSpeed.VeryFast: 
          if (thisLvl <= 50) {
            neededExp = (int)((Math.Pow(thisLvl, 3f) * (100 - thisLvl)) / 50);
          }
          else if ((50 < thisLvl && thisLvl <= 68) || thisLvl > 98) {
            neededExp = (int)((Math.Pow(thisLvl, 3f) * (150 - thisLvl)) / 100);
          }
          else if (68 < thisLvl && thisLvl <= 98) {
            neededExp = (int)((Math.Pow(thisLvl, 3f) * ((1911 - 10 * thisLvl)/3)) / 500);
          }
          break;
      }
      if (cntExp - neededExp < 0) {
        //Debug.Log("Returning CalcLvl from Monster Meta: " + thisLvl.ToString() + "/" + cntExp.ToString() + "/" + neededExp.ToString());
        //Debug.Log(meta.ToString());
        return new int[]{thisLvl-1,cntExp,neededExp};
      } else {
        thisLvl++;
        cntExp -= neededExp;
      }
    }
    return new int[] { 0,0,0 };
  }

  public static TileMeta.GemType elementToGem(elements element){
    switch(element){
      case MonsterMeta.elements.Sea: return TileMeta.GemType.Silver;
      case MonsterMeta.elements.Nature: return TileMeta.GemType.Green;
      case MonsterMeta.elements.Wind: return TileMeta.GemType.Silver;
      case MonsterMeta.elements.Disease: return TileMeta.GemType.Purple;
      case MonsterMeta.elements.Magma: return TileMeta.GemType.Red;
      case MonsterMeta.elements.Lightning: return TileMeta.GemType.Gold;
      case MonsterMeta.elements.Fight: return TileMeta.GemType.Fight;
      default: return TileMeta.GemType.Fight;
    }
  }

  public enum elements {
    Sea, Nature, Wind, Disease, Magma, Lightning, Fight, None
  }

  public enum lvlUpSpeed
  {
    VerySlow, Slow, MediumSlow, MediumFast, Fast, VeryFast, None
  }
}
