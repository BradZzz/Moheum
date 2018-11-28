using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NPCMeta {
  public string name;
  [HideInInspector]
  public PosMeta pos;
  public bool wander;
  public bool trainer;
  public bool defeated;
  public bool talkedTo;
  public bool curious;
  public int lookDistance;

  public bool visible;
  public bool canFight;

  public bool infiniteTrainer;
  public int infiniteTrainerDiff;

  public QuestMeta[] questsToAppear;
  public QuestMeta[] questsToDisappear;
  public QuestMeta[] questsToFight;

  public DialogMeta[] dialogs;
  public DialogMeta[] defeatedDialogs;
  public PlayerRosterMeta[] roster;

  public NPCMeta(){
    visible = true;
    canFight = true;
  }

  public NPCMeta(NPCMeta meta){
    string json = JsonUtility.ToJson(meta);
    NPCMeta copy = JsonUtility.FromJson<NPCMeta>(json);
    name = copy.name;
    pos = copy.pos;
    wander = copy.wander;
    trainer = copy.trainer;
    defeated = copy.defeated;
    talkedTo = copy.talkedTo;
    curious = copy.curious;
    lookDistance = copy.lookDistance;

    visible = copy.visible;
    canFight = copy.canFight;

    questsToAppear = copy.questsToAppear;
    questsToDisappear = copy.questsToDisappear;
    questsToFight = copy.questsToFight;

    dialogs = copy.dialogs;
    defeatedDialogs = copy.defeatedDialogs;

    infiniteTrainer = copy.infiniteTrainer;
    infiniteTrainerDiff = copy.infiniteTrainerDiff;

    roster = copy.roster;
  }

  public override string ToString()
  {
    string buffer = "";
    buffer += "NPC: " + name + "\n";
    buffer += "Wander: " + wander.ToString() + "\n";
    buffer += "Trainer: " + trainer.ToString() + "\n";
    buffer += "Defeated: " + defeated.ToString() + "\n";
    buffer += "Dialogs\n";
    foreach(DialogMeta dialog in dialogs){
      buffer += "\t" + dialog.msg;
    }
    buffer += "DefeatedDialogs\n";
    foreach (DialogMeta dialog in defeatedDialogs)
    {
      buffer += "\t" + dialog.msg;
    }
    buffer += "Roster\n";
    foreach (PlayerRosterMeta monster in roster)
    {
      buffer += monster.ToString();
    }
    return buffer;
  }
}
