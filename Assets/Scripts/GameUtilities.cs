using UnityEngine;
using System.Collections.Generic;

public static class GameUtilities {
  public static void ShuffleArray<T>(T[] arr) {
    for (int i = arr.Length - 1; i > 0; i--) {
      int r = Random.Range(0, i);
      T tmp = arr[i];
      arr[i] = arr[r];
      arr[r] = tmp;
    }
  }
  public static SkillMeta[] parseSkills(string[] skills, Glossary glossary){
    List<SkillMeta> retSkills = new List<SkillMeta> ();
    foreach (string skill in skills) {
      foreach (GameObject meta in glossary.skills) {
        if (meta.name.Equals(skill)) {
          SkillMetaSerializable foundMeta = meta.GetComponent<SkillMain> ().meta;
          SkillMeta thisSkill = new SkillMeta ();
          thisSkill.name = foundMeta.name;
          thisSkill.req1 = new SkillReq();
          thisSkill.req1.gem = foundMeta.req1.gem;
          thisSkill.req1.req = foundMeta.req1.req;
          thisSkill.req1.has = 0;
          thisSkill.req2 = new SkillReq();
          thisSkill.req2.gem = foundMeta.req2.gem;
          thisSkill.req2.req = foundMeta.req2.req;
          thisSkill.req2.has = 0;
          List<SkillEffect> effects = new List<SkillEffect> ();
          foreach(SkillEffectSerializable effect in foundMeta.effects){
            switch(effect.effect){
            case SkillEffect.Effect.Change:
              SkillEffect.ChangeSkill changeEffect = new SkillEffect.ChangeSkill ();
              changeEffect.from = effect.gem1;
              changeEffect.to = effect.gem2;
              effects.Add (changeEffect);
              break;
            case SkillEffect.Effect.ChangeSome:
              SkillEffect.ChangeSomeSkill changeSomeEffect = new SkillEffect.ChangeSomeSkill ();
              changeSomeEffect.from = effect.gem1;
              changeSomeEffect.to = effect.gem2;
              changeSomeEffect.lower = effect.val;
              changeSomeEffect.upper = effect.val2;
              effects.Add (changeSomeEffect);
              break;
            case SkillEffect.Effect.Damage:
              SkillEffect.DamageSkill damageEffect = new SkillEffect.DamageSkill ();
              damageEffect.amount = effect.val;
              effects.Add (damageEffect);
              break;
            case SkillEffect.Effect.Destroy:
              SkillEffect.DestroySkill destroyEffect = new SkillEffect.DestroySkill ();
              destroyEffect.toRemove = effect.gem1;
              effects.Add (destroyEffect);
              break;
            case SkillEffect.Effect.DestroySome:
              SkillEffect.DestroySomeSkill destroySomeEffect = new SkillEffect.DestroySomeSkill ();
              destroySomeEffect.toRemove = effect.gem1;
              destroySomeEffect.from = effect.val;
              destroySomeEffect.to = effect.val2;
              effects.Add (destroySomeEffect);
              break;
            case SkillEffect.Effect.Heal:
              SkillEffect.HealSkill healEffect = new SkillEffect.HealSkill ();
              healEffect.amount = effect.val;
              effects.Add (healEffect);
              break;
            case SkillEffect.Effect.Poison:
              SkillEffect.PoisonSkill buffEffect = new SkillEffect.PoisonSkill();
              buffEffect.amount = effect.val;
              effects.Add(buffEffect);
              break;
            case SkillEffect.Effect.Poke:
              SkillEffect.PokeSkill pokeEffect = new SkillEffect.PokeSkill();
              pokeEffect.toRemove = effect.gem1;
              pokeEffect.amount = effect.val;
              effects.Add(pokeEffect);
              break;
            case SkillEffect.Effect.Reset:
              SkillEffect.ResetSkill resetEffect = new SkillEffect.ResetSkill ();
              effects.Add (resetEffect);
              break;
            case SkillEffect.Effect.Sabotage:
              SkillEffect.SabotageSkill sabotageEffect = new SkillEffect.SabotageSkill();
              sabotageEffect.amount = effect.val;
              effects.Add(sabotageEffect);
              break;
            case SkillEffect.Effect.Slice:
              SkillEffect.SliceSkill sliceEffect = new SkillEffect.SliceSkill();
              sliceEffect.amount = effect.val;
              sliceEffect.toRemove = effect.gem1;
              effects.Add(sliceEffect);
              break;
            case SkillEffect.Effect.xTurn:
              SkillEffect.xTurnSkill xTurnEffect = new SkillEffect.xTurnSkill ();
              xTurnEffect.amount = effect.val;
              effects.Add (xTurnEffect);
              break;
            }
          }
          thisSkill.effects = effects.ToArray ();
          retSkills.Add (thisSkill);
        }
      }
    }
    return retSkills.ToArray();
  }

  public static int getNPCYenValue(AdventureMeta meta){
    int value = 0;
    foreach(PlayerRosterMeta mohe in meta.roster){
      value += mohe.lvl + mohe.maxHealth;
    }
    return value;
  }

  public static BoardMeta getBoardState(string mapName, PosMeta playerPos){
    //Debug.Log("getBoardState: " + mapName);
    List<NPCMeta> npcs = new List<NPCMeta>();
    foreach(GameObject npc in GameObject.FindGameObjectsWithTag("NPC")){
      NPCMeta thisMeta = npc.GetComponent<NPCMain>().meta;

      NPCMeta meta = new NPCMeta(thisMeta);
      meta.pos = new PosMeta(npc.transform.position);

      npcs.Add(meta);
    }
    List<MonTreasMeta> items = new List<MonTreasMeta>();
    foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
    {
      //Debug.Log("Found Item: " + item.name);
      //Debug.Log("Active: " + item.activeInHierarchy.ToString());
      if (item.activeInHierarchy)
      {
        MonTreasMeta thisMeta = item.GetComponent<TreasureMain>().monTreas;
        MonTreasMeta meta = new MonTreasMeta(thisMeta, new PosMeta(item.transform.position));
        items.Add(meta);
      }
    }
    BoardMeta board = new BoardMeta();
    board.NPCs = npcs.ToArray();
    board.mapName = mapName;
    board.playerPos = playerPos;
    board.Items = items.ToArray();
    return board;
  }

  public static List<string> getInteractedWith(Glossary glossary, bool talkedTo, bool defeated){
    List<string> validNPCS = new List<string>();
    foreach(GameObject lvl in glossary.scenes){
      BoardMeta meta = BaseSaver.getBoard(lvl.GetComponent<SceneMain>().name);
      if (meta != null)
      {
        foreach (NPCMeta npc in meta.NPCs)
        {
          if (talkedTo && npc.talkedTo)
          {
            validNPCS.Add(npc.name);
          }
          if (defeated && npc.defeated)
          {
            validNPCS.Add(npc.name);
          }
        }
      }
    }
    return validNPCS;
  }

  public static bool getPickedUp(string scene, Vector3 pos)
  {
    BoardMeta bMeta = BaseSaver.getBoard(scene);
    if (bMeta != null)
    {
      foreach(MonTreasMeta treasure in bMeta.Items)
      {
        PosMeta pos1 = new PosMeta(pos);
        if (pos1.Equals2(treasure.pos)) {
          return false;
        }
      }
      return true;
    }
    return false;
  }

  public static bool CanUseItem(TreasureMain itemToUse, AdventureMeta meta, int rosterPos)
  {
    switch (itemToUse.monTreas.effects)
    {
      case MonTreasMeta.Type.Exp:
        return false;
      case MonTreasMeta.Type.Heal:
        if (meta.roster[rosterPos].curHealth > 0)
        {
          return true;
        }
        return false;
      case MonTreasMeta.Type.Revive:
        if (meta.roster[rosterPos].curHealth <= 0)
        {
          return true;
        }
        return false;
      case MonTreasMeta.Type.Stats:
        return false;
      default:
        return false;
    }
  }

  public static AdventureMeta UseItem(TreasureMain itemToUse, AdventureMeta meta, int rosterPos)
  {
    /*
     * Verify item is in the list
     * update adventure meta
     */
    //TreasureMain itemToUse = glossy.GetItem(item);
    if (itemToUse != null)
    {
      //    public enum Type
      //{
      //  Exp, Heal, Money, Revive, Stats, None
      //}
      switch (itemToUse.monTreas.effects)
      {
        case MonTreasMeta.Type.Exp:
          meta.roster[rosterPos].exp += itemToUse.monTreas.value;
          break;
        case MonTreasMeta.Type.Heal:
          meta.roster[rosterPos].curHealth += itemToUse.monTreas.value;
          if (meta.roster[rosterPos].curHealth > meta.roster[rosterPos].maxHealth) {
            meta.roster[rosterPos].curHealth = meta.roster[rosterPos].maxHealth;
          }
          break;
        //case MonTreasMeta.Type.Money:
          //meta.addYen(itemToUse.monTreas.value);
          //break;
        case MonTreasMeta.Type.Revive:
          if (meta.roster[rosterPos].curHealth <= 0) {
            meta.roster[rosterPos].curHealth = meta.roster[rosterPos].maxHealth / 2;
          }
          break;
        case MonTreasMeta.Type.Stats:
          break;
        default:
          break;
      }
    }


    return meta;
  }

}