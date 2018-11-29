using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMain : MonoBehaviour {
  public NPCMeta meta;
  GameObject emotion;
  Vector3 looking;
  bool waitingForRoll;
  bool foundHero;

  static float waitTime = 1f;

  Vector3[] dirs = {
    Vector3.up,
    Vector3.down,
    Vector3.left,
    Vector3.right
  };

  string[] anims = {
    "MoveUp",
    "MoveDown",
    "MoveLeft",
    "MoveRight"
  };

  private bool moving;
  private Glossary glossy;

  void Start()
  {
    if(gameObject.transform.Find("Emotion")){
      emotion = gameObject.transform.Find("Emotion").gameObject;
      emotion.SetActive(false);
    }
    moving = false;
    waitingForRoll = false;

    //glossy = null;
    glossy = GameObject.Find("PauseCanvas").GetComponent<PauseManager>().glossaryObj.GetComponent<Glossary>();
    UpdateNPC();
  }

  public NPCMeta getRefinedMeta(){
    List<PlayerRosterMeta> newRoster = new List<PlayerRosterMeta>();
    foreach (PlayerRosterMeta mohe in meta.roster){
      newRoster.Add(MonsterMeta.returnMonster(glossy.GetMonsterMain(mohe.name).meta, mohe.lvl, false));
    }
    NPCMeta returnMeta = new NPCMeta(meta);
    returnMeta.roster = newRoster.ToArray();
    return returnMeta;
  }

  public NPCMeta getInfiniteMeta(AdventureMeta advMeta)
  {
    float averageLevel = 0;
    foreach (PlayerRosterMeta mohe in advMeta.roster){
      averageLevel += mohe.lvl;
    }
    averageLevel = (float)Math.Ceiling((averageLevel / advMeta.roster.Length)) + meta.infiniteTrainerDiff;

    List<PlayerRosterMeta> newRoster = new List<PlayerRosterMeta>();
    for (int i = 0; i < advMeta.roster.Length; i++)
    {
      String monsterName = glossy.monsters[UnityEngine.Random.Range(0,glossy.monsters.Length-1)].name;
      newRoster.Add(MonsterMeta.returnMonster(glossy.GetMonsterMain(monsterName).meta, (int)averageLevel, false));
    }
    NPCMeta returnMeta = new NPCMeta(meta);
    returnMeta.roster = newRoster.ToArray();
    return returnMeta;
  }

  public IEnumerator WaitForAction(float wait)
  {
    yield return new WaitForSeconds(wait);
    waitingForRoll = false;
    yield return null;
  }

  public void UpdateNPC()
  {
    StartCoroutine(checkQuestSatisfied());
  }

  IEnumerator checkQuestSatisfied(){
    bool qsts = meta.questsToAppear.Length > 0 || meta.questsToFight.Length > 0 || meta.questsToDisappear.Length > 0;
    if (qsts && glossy != null)
    {
      Debug.Log("This character has some fucking quests!");

      List<string> talkedTo = GameUtilities.getInteractedWith(glossy, true, false);
      List<string> defeated = GameUtilities.getInteractedWith(glossy, false, true);

      if (meta.questsToAppear.Length > 0)
      {
        /*
         * Hide character until this happens
         */
        bool hideCharacter = true;
        foreach(QuestMeta npc in meta.questsToAppear){
          if (npc.goal == QuestMeta.QuestType.TalkTo && !talkedTo.Contains(npc.questTarget)) {
            hideCharacter = false;
          }
          if (npc.goal == QuestMeta.QuestType.Defeated && !defeated.Contains(npc.questTarget))
          {
            hideCharacter = false;
          }
        }
        if (hideCharacter) {
          meta.visible = true;
        } else {
          meta.visible = false;
        }
      }
      if (meta.questsToFight.Length > 0)
      {
        /*
         * Disable fight until this happens
         */
        bool disableFight = false;
        foreach (QuestMeta npc in meta.questsToFight)
        {
          if (npc.goal == QuestMeta.QuestType.TalkTo && !talkedTo.Contains(npc.questTarget))
          {
            disableFight = true;
          }
          if (npc.goal == QuestMeta.QuestType.Defeated && !defeated.Contains(npc.questTarget))
          {
            disableFight = true;
          }
        }
        if (disableFight)
        {
          meta.canFight = false;
        }
        else
        {
          meta.canFight = true;
        }
      }
      if (meta.questsToDisappear.Length > 0)
      {
        /*
         * Show character until this happens
         */
        bool showCharacter = false;
        foreach (QuestMeta npc in meta.questsToDisappear)
        {
          if (npc.goal == QuestMeta.QuestType.TalkTo && !talkedTo.Contains(npc.questTarget))
          {
            showCharacter = true;
          }
          if (npc.goal == QuestMeta.QuestType.Defeated && !defeated.Contains(npc.questTarget))
          {
            showCharacter = true;
          }
        }
        if (showCharacter)
        {
          meta.visible = true;
        }
        else
        {
          meta.visible = false;
        }
      }
      if (meta.visible) {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        Debug.Log("Enabled: " + meta.name);
      } else {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Disabled: " + meta.name);
      }
      if (!meta.canFight) {
        Debug.Log("Cant fight: " + meta.name);
      }
    }
    if (glossy == null) {
      glossy = GameObject.Find("PauseCanvas").GetComponent<PauseManager>().glossaryObj.GetComponent<Glossary>();
    }
    yield return null;
  }

  void FixedUpdate()
  {
    // Check completed quest status
    if (!meta.curious)
    {
      looking = transform.TransformDirection(Vector3.down);
    }
    else
    {
      if (!waitingForRoll && !moving && !meta.defeated)
      {
        waitingForRoll = true;
        int idx = UnityEngine.Random.Range(0, dirs.Length);
        looking = transform.TransformDirection(dirs[idx]);
        GetComponent<Animator>().SetTrigger(anims[idx]);
        StartCoroutine(WaitForAction(waitTime));
      }
    }

    float rayLength = (float)meta.lookDistance / 10f;
    if (rayLength > 0 && !meta.defeated)
    {
      //Debug.Log("rayLength: " + rayLength.ToString());
      RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, looking, rayLength);
      if (hits.Length > 1 && !moving && !meta.defeated && meta.trainer && meta.canFight && meta.visible)
      {
        for (int i = 1; i < hits.Length; i++)
        {
          if (hits[i].collider.gameObject.tag.Equals("Player") && hits[i].collider.gameObject.GetComponent<Move>().Moving() 
              && !hits[i].collider.gameObject.GetComponent<ColliderListener>().WaitingForRoll())
          {
            hits[i].collider.gameObject.GetComponent<Move>().disableMove();
            moveToPos(hits[i].collider.gameObject.transform.position);
          }
        }
      }
    }
  }

  public void moveToPos(Vector3 heroPos){
    if (!moving)
    {
      moving = true;
      meta.talkedTo = true;
      Vector3 pos = new Vector3(
        gameObject.transform.position.x + 4 * (heroPos.x - gameObject.transform.position.x) / 5,
        gameObject.transform.position.y + 4 * (heroPos.y - gameObject.transform.position.y) / 5,
        gameObject.transform.position.z + 4 * (heroPos.z - gameObject.transform.position.z) / 5
      );

      StartCoroutine(MoveNPC(pos));
    }
  }

  public void startleNPC()
  {
    if (!moving)
    {
      StartCoroutine(Startle());
    }
  }

  IEnumerator Startle()
  {
    emotion.SetActive(true);
    yield return new WaitForSeconds(1f);
    emotion.SetActive(false);
  }

  IEnumerator MoveNPC(Vector3 dest){
    emotion.SetActive(true);
    yield return new WaitForSeconds(1f);
    emotion.SetActive(false);
    iTween.MoveTo(gameObject, dest, 1.0f);
    yield return null;
  }
}
