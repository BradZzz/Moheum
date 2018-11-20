using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glossary : MonoBehaviour {
  public GameObject hero;
  public GameObject[] items;
  public GameObject[] monsters;
  public GameObject[] particleSystems;
  public GameObject[] skills;
  public GameObject[] scenes;
  public GameObject[] stories;
  public Sprite[] gems;

  public Sprite getGemSprite(TileMeta.GemType gem){
    return gems[(int)gem];
  }

  public SceneMain GetScene(string name){
    foreach (GameObject scene in scenes)
    {
      if (scene.name.Equals(name))
      {
        return scene.GetComponent<SceneMain>();
      }
    }
    return null;
  }

  public StoryHolder GetStory(string name)
  {
    foreach (GameObject story in stories)
    {
      if (story.name.Equals(name))
      {
        return story.GetComponent<StoryHolder>();
      }
    }
    return null;
  }

  public GameObject GetParticleSystem(string name)
  {
    foreach (GameObject system in particleSystems)
    {
      if (system.name.Contains(name))
      {
        return system;
      }
    }
    return null;
  }

  public SkillMain GetSkill(string name){
    foreach(GameObject skill in skills){
      if (skill.name.Equals(name)) {
        return skill.GetComponent<SkillMain>();
      }
    }
    return null;
  }

  public TreasureMain GetItem(string name)
  {
    foreach (GameObject item in items)
    {
      if (item.name.Equals(name))
      {
        return item.GetComponent<TreasureMain>();
      }
    }
    return null;
  }

  public Sprite GetMonsterImage(string name)
  {
    foreach (GameObject monster in monsters)
    {
      if (monster.name.Equals(name))
      {
        return monster.GetComponent<SpriteRenderer>().sprite;
      }
    }
    return null;
  }

  public Sprite GetMonsterImage(int pos)
  {
    if (monsters.Length > pos)
    {
      return monsters[pos].GetComponent<SpriteRenderer>().sprite;
    }
    return null;
  }

  public MonsterMain GetMonsterMain(string name)
  {
    foreach (GameObject monster in monsters)
    {
      if (monster.name.Equals(name))
      {
        return monster.GetComponent<MonsterMain>();
      }
    }
    return null;
  }

  public MonsterMain GetMonsterMain(int pos)
  {
    if (monsters.Length > pos)
    {
      return monsters[pos].GetComponent<MonsterMain>();
    }
    return null;
  }
}
