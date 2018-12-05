using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderListener : MonoBehaviour {

  private float roll_wait = 6f;
  private float convo_wait = 1f;

  private bool waitingForRoll;
  private bool waitingForConversation;
  private string lastMonster;

  void Start()
  {
    //Debug.Log("ColliderListener Start");
    waitingForRoll = true;
    waitingForConversation = true;
    StartCoroutine(WaitForAction(roll_wait));
    StartCoroutine(WaitForConversation(convo_wait));
  }

  public bool WaitingForRoll(){
    return waitingForRoll;
  }

  IEnumerator DestWait(Collider2D other)
  {
    Destroy(other.gameObject);
    yield return new WaitForSeconds(1f);
    BaseSaver.putBoard(GameUtilities.getBoardState(BaseSaver.getMap(), new PosMeta(transform.position)));
  }

  void OnTriggerEnter2D (Collider2D other)
  {
    Debug.Log("Other Checking: " + other.name);
    if (tag.Equals("Player"))
    {

      Debug.Log("Other Checking: " + other.name);
      Debug.Log("Has Dialog: " + DialogManager.instance.hasDialog().ToString());
      Debug.Log("other.name.Contains(NPC_Trainer): " + other.name.Contains("NPC_Trainer").ToString());

      if (!waitingForConversation)
      {
        if (other.tag.Contains("Item") && !DialogManager.instance.hasDialog())
        {
          DialogMeta[] diag = new DialogMeta[1];
          diag[0] = new DialogMeta("Received: " + other.name + "\n" + other.GetComponent<TreasureMain>().monTreas.description);
          DialogManager.instance.SetMsgs(other.gameObject.GetComponent<SpriteRenderer>().sprite, diag);
          AdventureMeta meta = BaseSaver.getAdventure();
          if (other.GetComponent<TreasureMain>().monTreas.effects == MonTreasMeta.Type.Money)
          {
            meta.addYen(other.GetComponent<TreasureMain>().monTreas.value);
          }
          else
          {
            meta.AddToTreasureList(other.name, 1);
          }
          BaseSaver.putAdventure(meta);
          GameObject.FindWithTag("Player").GetComponent<PlayerMain>().playerMeta = BaseSaver.getAdventure();
          StartCoroutine(DestWait(other));
        }
        if (other.tag.Contains("Signpost") && !DialogManager.instance.hasDialog())
        {
          DialogManager.instance.SetMsgs(other.gameObject.GetComponent<SpriteRenderer>().sprite, other.gameObject.GetComponent<SignDialog>().dialogs);
        }
        if (other.name.Contains("NPC_WhiteMage") && !DialogManager.instance.hasDialog())
        {
          //Debug.Log("NPC_WhiteMage");
          //NPCMeta meta = other.gameObject.GetComponent<NPCMain>().meta;

          NPCMeta npcmeta = other.gameObject.GetComponent<NPCMain>().meta;
          npcmeta.talkedTo = true;
          DialogManager.instance.SetMsgs(other.gameObject.GetComponent<SpriteRenderer>().sprite, other.gameObject.GetComponent<NPCMain>().meta.dialogs);
        }
        if (other.name.Contains("NPC_Trainer") && !DialogManager.instance.hasDialog())
        {
          Debug.Log("Other Collided: " + other.name);

          NPCMeta npcmeta = other.gameObject.GetComponent<NPCMain>().meta;

          if (npcmeta.dialogs.Length > 0)
          {
            Debug.Log("Dialogs good");
            npcmeta.talkedTo = true;
            if (npcmeta.lookDistance > 0 && !npcmeta.defeated){
              other.gameObject.GetComponent<NPCMain>().moveToPos(transform.position);
            } else if (npcmeta.trainer && !npcmeta.defeated) {
              other.gameObject.GetComponent<NPCMain>().startleNPC();
            }

            AdventureMeta meta = BaseSaver.getAdventure();
            //meta.playerPos = new PosMeta(transform.position);
            NPCMain trainerMain = other.gameObject.GetComponent<NPCMain>();
            if (trainerMain.meta.infiniteTrainer) {
              meta.trainer = trainerMain.getInfiniteMeta(meta);
            } else {
              meta.trainer = trainerMain.getRefinedMeta();
            }
            meta.trainer.pos = new PosMeta(other.gameObject.transform.position);
            meta.wild = null;
            meta.isTrainerEncounter = true;

            Debug.Log(meta.ToString());
            Debug.Log(meta.trainer.ToString());

            BaseSaver.putAdventure(meta);
            BaseSaver.putBoard(GameUtilities.getBoardState(BaseSaver.getMap(), new PosMeta(transform.position)));
            //BaseSaver.saveState();

            if (npcmeta.canFight) {
              Debug.Log("Can Fight");

              DialogManager.instance.SetMsgs(other.gameObject.GetComponent<SpriteRenderer>().sprite,
                                           other.gameObject.GetComponent<NPCMain>().meta.defeated ?
                                           other.gameObject.GetComponent<NPCMain>().meta.defeatedDialogs :
                                           other.gameObject.GetComponent<NPCMain>().meta.dialogs
                                          );
            } else {
              DialogMeta d1 = new DialogMeta("The stars have not yet aligned for us to battle.");
              DialogMeta d2 = new DialogMeta("Perhaps we will meet each other again...");

              DialogMeta[] comeAgainDialog = { d1, d2 };
              DialogManager.instance.SetMsgs(other.gameObject.GetComponent<SpriteRenderer>().sprite, comeAgainDialog);
            }
          }
        }
        if (other.name.Contains("ExitTileGrass"))
        {
          GameManager.instance.FadeOutNoScene();
          waitingForRoll = true;
          StartCoroutine(WaitForAction(roll_wait));
          StartCoroutine(WaitForConversation(convo_wait));
          BaseSaver.putBoard(GameUtilities.getBoardState(BaseSaver.getMap(),new PosMeta(transform.position)));
          other.gameObject.GetComponent<ExitTile>().loadSteppedScene();
        }
      } else {
        Debug.Log("Waiting for roll");
      }
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    /*
     * Conditions
     * NPC Appears when another talked to
     * NPC Disappears when another talked to 
     * NPC Refuses to battle until another talked to
     * NPC Appears when another battled
     * NPC Disappears when another battled
     * NPC Refuses to battle until another battled
     * 
     * One test npc to battle
     * One test npc to talk to
     */
  }

  void OnTriggerStay2D(Collider2D other)
  {
    if (tag.Equals("Player"))
    {
      if (!waitingForRoll)
      {
        //Debug.Log("Hit: " + other.name);
        if (other.name.Contains("Plant"))
        {
          //Debug.Log("ColliderListener Rolling");
          waitingForRoll = true;
          StartCoroutine(WaitTrigger(.5f));
        }
      } 
    }
  }

  public IEnumerator WaitForAction(float wait){
    yield return new WaitForSeconds(wait);
    waitingForRoll = false;
    Debug.Log("WaitForAction Active");
    yield return null; 
  }

  public IEnumerator WaitForConversation(float wait)
  {
    yield return new WaitForSeconds(wait);
    waitingForConversation = false;
    Debug.Log("WaitForConversation Active");
    yield return null;
  }

  public IEnumerator WaitTrigger(float wait)
  {
    int roll = Random.Range(0, 10);
    if (roll < 1 && GetComponent<Move>().Moving() && !PauseManager.instance.IsOpen() && !DialogManager.instance.ShopActive() && !DialogManager.instance.DialogActive())
    {
      Debug.Log("Hit");

      //GetComponent<Move>().disableMove();
      AdventureMeta meta = BaseSaver.getAdventure();
      Glossary glossy = GameObject.Find("PauseCanvas").GetComponent<PauseManager>().glossaryObj.GetComponent<Glossary>();
      SceneMeta thisScene = glossy.GetScene(BaseSaver.getMap()).meta;

      List<PlayerRosterMeta> monsters = new List<PlayerRosterMeta>();
      foreach(string mons in thisScene.monsters){
        if (!mons.Equals(lastMonster))
        {
          MonsterMeta met = glossy.GetMonsterMain(mons).meta;
          int lvl = Random.Range(thisScene.monsterLvls[0], thisScene.monsterLvls[1]);
          monsters.Add(MonsterMeta.returnMonster(met, lvl, true));
        }
      }

      PlayerRosterMeta[] scrbMons = monsters.ToArray();
      GameUtilities.ShuffleArray(scrbMons);

      meta.trainer = null;
      meta.wild = scrbMons[0];
      meta.isTrainerEncounter = false;
      lastMonster = meta.wild.name;

      BaseSaver.putAdventure(meta);
      BaseSaver.putBoard(GameUtilities.getBoardState(BaseSaver.getMap(),new PosMeta(transform.position)));

      StartCoroutine(DialogManager.FightFlash(false));
      //Initiate.Fade("BejeweledScene", Color.black, 1.0f);

    }
    yield return new WaitForSeconds(wait);
    waitingForRoll = false;
    yield return null; 
  }

  //IEnumerator FightFlash()
  //{
  //  yield return new WaitForSeconds(.2f);
  //  GameManager.instance.FightFlash();
  //  yield return new WaitForSeconds(3f);
  //  Initiate.Fade("BejeweledScene", Color.black, 1.0f);
  //  //GameManager.instance.LoadScene("BejeweledScene");
  //  yield return null;
  //}
}
