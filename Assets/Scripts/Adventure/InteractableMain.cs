using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMain : MonoBehaviour {
  public InteractableMeta meta;
  private Glossary glossy;

  void Start()
  {
    glossy = GameObject.Find("PauseCanvas").GetComponent<PauseManager>().glossaryObj.GetComponent<Glossary>();
    UpdateInteractable();
  }

  public void UpdateInteractable()
  {
    StartCoroutine(checkQuestSatisfied());
  }

  IEnumerator checkQuestSatisfied()
  {
    bool qsts = meta.questsToAppear.Length > 0 || meta.questsToDisappear.Length > 0;
    if (qsts && glossy != null)
    {
      //Debug.Log("This interactable has some fucking quests!");

      List<string> talkedTo = GameUtilities.getInteractedWith(glossy, true, false);
      List<string> defeated = GameUtilities.getInteractedWith(glossy, false, true);

      //foreach(string talked in talkedTo){
      //  Debug.Log("Talked To: " + talked);
      //}

      if (meta.questsToAppear.Length > 0)
      {
        /*
         * Hide character until this happens
         */
        bool hideCharacter = true;
        foreach (QuestMeta npc in meta.questsToAppear)
        {
          if (npc.goal == QuestMeta.QuestType.TalkTo && !talkedTo.Contains(npc.questTarget))
          {
            hideCharacter = false;
          }
          if (npc.goal == QuestMeta.QuestType.Defeated && !defeated.Contains(npc.questTarget))
          {
            hideCharacter = false;
          }
        }
        if (hideCharacter)
        {
          meta.visible = true;
        }
        else
        {
          meta.visible = false;
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
          //Debug.Log(npc.ToString());
          if (npc.goal == QuestMeta.QuestType.TalkTo)
          {
            if (!talkedTo.Contains(npc.questTarget))
            {
              //Debug.Log("Haven't talked to: " + npc.questTarget);
              //Debug.Log("Talked to");
              foreach (string talked in talkedTo)
              {
                Debug.Log(talked);
              }
              showCharacter = true;
            }
          }
          if (npc.goal == QuestMeta.QuestType.Defeated)
          {
            if (!defeated.Contains(npc.questTarget))
            {
              showCharacter = true;
            }
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

      //Debug.Log("Interactable Visible: " + meta.visible.ToString());

      if (meta.visible)
      {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
      }
      else
      {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
      }
    }
    if (glossy == null)
    {
      glossy = GameObject.Find("PauseCanvas").GetComponent<PauseManager>().glossaryObj.GetComponent<Glossary>();
    }
    yield return null;
  }
}
