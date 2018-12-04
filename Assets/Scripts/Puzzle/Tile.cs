using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using cakeslice;

public class Tile : MonoBehaviour {
  public TileMeta type;

	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static Tile previousSelected = null;

	private SpriteRenderer render;
	private bool isSelected = false;

  public static Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
  public bool toBeDeleted = false;
  private bool matchFound = false;

	void Awake() {
		render = GetComponent<SpriteRenderer>();
   }

	public void Select() {
		isSelected = true;
    gameObject.GetComponent<Outline>().eraseRenderer = false;
    gameObject.GetComponent<Outline>().color = 0;
    previousSelected = gameObject.GetComponent<Tile>();
		SFXManager.instance.PlaySFX(Clip.Select);
	}

  public void SelectHint()
  {
    gameObject.GetComponent<Outline>().eraseRenderer = false;
    gameObject.GetComponent<Outline>().color = 1;
  }

  public void Deselect() {
		isSelected = false;
    gameObject.GetComponent<Outline>().eraseRenderer = true;
		previousSelected = null;
	}

  void OnMouseDown() {
    Debug.Log("Tile Clicked");
    Debug.Log("BoardManager.instance.GameOver(): " + BoardManager.instance.GameOver().ToString());
    Debug.Log("BoardManager.instance.getPlayerTurn(): " + BoardManager.instance.getPlayerTurn().ToString());
    Debug.Log("BoardManager.instance.IsProcessing(): " + BoardManager.instance.IsThinking().ToString());
    if (!BoardManager.instance.GameOver() && BoardManager.instance.getPlayerTurn() && !BoardManager.instance.IsThinking()) {
      if (render.sprite == null || BoardManager.instance.IsThinking()) {
        return;
      }
      if (isSelected) {
        Deselect();
      } else {
        SkillEffect[] effects = PanelManager.instance.getActiveSkill ();
        if (effects != null) {
          Debug.Log ("Active Skill!");
          bool valid = true;
          foreach (SkillEffect effect in effects)
          {
            valid = checkEffect(effect);
            if (!valid) {
              break;
            }
          }
          if (valid)
          {
            foreach (SkillEffect effect in effects)
            {
              doEffect(effect);
            }
            PanelManager.instance.useSkill();
          }
        } else {
          if (previousSelected == null) {
            Debug.Log("Selected: " + type.type.ToString());
            BoardManager.instance.deselectAll();
            Select();
          } else {
            if (GetAllAdjacentTiles().Contains(previousSelected.gameObject)) {
              SwapSprite(render, previousSelected.render, false);
              if (checkMatch(previousSelected, 1)) {
                BoardManager.instance.setClicked (true);
                previousSelected.ClearAllMatches ();
                if (previousSelected != null) {
                  previousSelected.Deselect();
                }
                ClearAllMatches ();
              } else {
                SwapSprite(render, previousSelected.render, false);
              }
            } else {
              previousSelected.GetComponent<Tile>().Deselect();
              Select();
            }
          }
        }
      }
    }
  }

  bool checkEffect(SkillEffect effect){
    switch (effect.effect)
    {
      case SkillEffect.Effect.Poke:
        SkillEffect.PokeSkill pokeskill = (SkillEffect.PokeSkill)effect;
        if (type.type == pokeskill.toRemove)
        {
          return true;
        }
        return false;
      default:
        return true;
    }
  }

  void doEffect(SkillEffect effect){
    Debug.Log ("Skill: " + effect.ToString());
    Debug.Log ("Effect: " + effect.effect.ToString());
    switch(effect.effect){
      case SkillEffect.Effect.Change:
        SkillEffect.ChangeSkill cskill = (SkillEffect.ChangeSkill)effect;
        BoardManager.instance.switchAllTiles (cskill.from, cskill.to);
        break;
      case SkillEffect.Effect.ChangeSome:
        SkillEffect.ChangeSomeSkill csomeskill = (SkillEffect.ChangeSomeSkill)effect;
        BoardManager.instance.switchSomeTiles (csomeskill.from, csomeskill.to, csomeskill.lower, csomeskill.upper);
        break;
      case SkillEffect.Effect.Damage:
        characterHit (effect.amount + (int)PanelManager.instance.getCurrentMonster().sloth);
        break;
      case SkillEffect.Effect.Destroy:
        SkillEffect.DestroySkill destroyskill = (SkillEffect.DestroySkill)effect;
        BoardManager.instance.destroyTiles (destroyskill.toRemove);
        break;
      case SkillEffect.Effect.DestroySome:
        SkillEffect.DestroySomeSkill destroysomeskill = (SkillEffect.DestroySomeSkill)effect;
        BoardManager.instance.destroySomeTiles (destroysomeskill.toRemove, destroysomeskill.from, destroysomeskill.to);
        break;
      case SkillEffect.Effect.Heal:
        characterHit (-(effect.amount + (int)PanelManager.instance.getCurrentMonster().sloth));
        break;
      case SkillEffect.Effect.Poke:
        SkillEffect.PokeSkill pokeskill = (SkillEffect.PokeSkill)effect;
        if (type.type == pokeskill.toRemove) {
          BoardManager.instance.PokeTile(this, effect.amount);
        }
        break;
      case SkillEffect.Effect.Poison:
        SkillEffect.PoisonSkill poisonskill = (SkillEffect.PoisonSkill)effect;
        BoardManager.instance.buff(poisonskill.amount);
        break;
      case SkillEffect.Effect.Reset:
        BoardManager.instance.reset (true);
        break;
      case SkillEffect.Effect.Sabotage:
        BoardManager.instance.Sabotage(effect.amount);
        break;
      case SkillEffect.Effect.Slice:
        SkillEffect.SliceSkill sliceskill = (SkillEffect.SliceSkill)effect;
        if (type.type == sliceskill.toRemove)
        {
          BoardManager.instance.SliceTile(this, effect.amount);
        }
        break;
      case SkillEffect.Effect.xTurn:
        BoardManager.instance.addExtraTurns (effect.amount);
        break;
    }
  }

  //The swap the computer uses
  public void computerSwap(Vector2 dir){
    if (checkSwap(dir)) {
      Debug.Log("Can swap");
      BoardManager.instance.setClicked (true);
      Debug.Log("Check 1");
      GameObject adj = GetAdjacent(dir);
      Debug.Log("Check 2");
      SwapSprite(render, adj.GetComponent<SpriteRenderer>(), false);
      Debug.Log("Check 3");
      adj.GetComponent<Tile>().ClearAllMatches ();
      Debug.Log("Check 4");
      Deselect ();
      Debug.Log("Check 5");
      ClearAllMatches ();
      Debug.Log("Check 6");
    } else {
      Debug.Log("Cant swap!");
      Deselect();
      StartCoroutine(BoardManager.instance.loopAIMovement());
    }
  }

  public void computerUseSkill(){
    if (PanelManager.instance.getActiveSkill () != null) {
      foreach(SkillEffect effect in PanelManager.instance.getActiveSkill ()){
        doEffect(effect);
      }
      PanelManager.instance.useSkill ();
    }
  }

  private int checkMatchDirectionType(Vector2[] dirs, Tile other, bool checkingAdj){
    int total = 0;
    Tile tl;

    if (checkingAdj) {
      total = other.FindMatch(dirs[0], true).Count + other.FindMatch (dirs[1], true).Count;
      tl = other;
    } else {
      total = FindMatch (dirs[0], true).Count + FindMatch (dirs[1], true).Count;
      tl = this;
    }

    //matchA = (FindMatch(Vector2.left, swap).Count + FindMatch(Vector2.right, swap).Count > min);
    //matchB = (FindMatch(Vector2.up, swap).Count + FindMatch(Vector2.down, swap).Count > min);
    //matchC = (adjTile.FindMatch(Vector2.left, swap).Count + adjTile.FindMatch(Vector2.right, swap).Count > min);
    //matchD = (adjTile.FindMatch(Vector2.up, swap).Count + adjTile.FindMatch(Vector2.down, swap).Count > min);

    Debug.Log("checkMatchDirectionType Total: " + total.ToString());

    if (total == 4) {
      Debug.Log("Extreme move found");
      if (tl.type.type == TileMeta.GemType.Fight) {
        return 6;
      }
      return 5;
    } else if (total == 3) {
      Debug.Log("Awesome move found");
      if (tl.type.type == TileMeta.GemType.Fight) {
        return 4;
      }
      return 3;
    } else if (total == 2) {
      Debug.Log("Simple move found");
      if (tl.type.type == TileMeta.GemType.Fight) {
        return 2;
      }
      return 1;
    }
    return -1;
  }

  public int checkMatchType(Vector2 dir){

    Vector2 invDir = new Vector2(dir.y, dir.x);
    GameObject adj = GetAdjacent(dir);
    if (adj != null)
    {
      SpriteRenderer adjSprRend = adj.GetComponent<SpriteRenderer>();
      SwapSprite(render, adjSprRend, false, true);
      int normA = checkMatch(adj.GetComponent<Tile>(), 1, true) ? 1 : 0;
      int normB = checkMatch(adj.GetComponent<Tile>(), 2, true) ? 1 : 0;
      int normC = checkMatch(adj.GetComponent<Tile>(), 3, true) ? 1 : 0;
      int total = normA + normB + normC + (type.type == TileMeta.GemType.Fight ? 1 : 0);
      SwapSprite(render, adjSprRend, false, true);
      return total;
    }

    return 0;
  }

  public bool checkMatch(Tile adjTile, int min, bool swap = false){
    bool matchA, matchB, matchC, matchD;

    matchA = (FindMatch(Vector2.left, swap).Count + FindMatch(Vector2.right, swap).Count > min);
    matchB = (FindMatch(Vector2.up, swap).Count + FindMatch(Vector2.down, swap).Count > min);
    matchC = (adjTile.FindMatch(Vector2.left, swap).Count + adjTile.FindMatch(Vector2.right, swap).Count > min);
    matchD = (adjTile.FindMatch(Vector2.up, swap).Count + adjTile.FindMatch(Vector2.down, swap).Count > min);

    return matchA || matchB || matchC || matchD;
  }

  /*
   * TODO: This function takes a direction and indicates if a swap in that direction will lead to a match
   */

  public bool checkSwap(Vector2 dir){
    //Get nearest gem in that direction
    Vector2 invDir = new Vector2(dir.y, dir.x);
    GameObject adj = GetAdjacent(dir);
    if (adj != null) {
      SpriteRenderer adjSprRend = adj.GetComponent<SpriteRenderer> ();
      SwapSprite(render, adjSprRend, false, true);
      bool canSwap = checkMatch(adj.GetComponent<Tile>(), 1, true);
      SwapSprite(render, adjSprRend, false, true);
      return canSwap;
    }
    return false;
  }

  public void SwapSprite(SpriteRenderer render1, SpriteRenderer render2, bool sound, bool testing = false) {
    if (render1.sprite == render2.sprite) {
      return;
    }

    if (!testing) {
      StartCoroutine(BoardManager.instance.AnimateGemSwap(render1.gameObject, render2.gameObject));
    } else {
      TileMeta type1 = render1.gameObject.GetComponent<Tile>().type;
      TileMeta type2 = render2.gameObject.GetComponent<Tile>().type;

      //Sprite tempSprite = render2.sprite;
      //render2.sprite = render.sprite;
      render2.gameObject.GetComponent<Tile>().type = type1;
      //render1.sprite = tempSprite;
      render1.gameObject.GetComponent<Tile>().type = type2;
    }

    if (sound) {
      SFXManager.instance.PlaySFX(Clip.Swap);
    }
  }

  public GameObject GetAdjacent(Vector2 castDir) {
    RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position, castDir);
    if (hits.Length > 1 && hits[1].collider != null) {
      return hits[1].collider.gameObject;
    }
    return null;
  }

  private List<GameObject> GetAllAdjacentTiles() {
    List<GameObject> adjacentTiles = new List<GameObject>();
    for (int i = 0; i < adjacentDirections.Length; i++) {
      adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
    }
    return adjacentTiles;
  }

//  public List<GameObject> FindMatch(Vector2 castDir) { // 1
//    List<GameObject> matchingTiles = new List<GameObject>(); // 2
//    RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position, castDir);
//    for (int i = 1; i < hits.Length; i++) {
//      if (hits[i].collider != null && hits[i].collider.GetComponent<SpriteRenderer> ().sprite == render.sprite) {
//        matchingTiles.Add (hits[i].collider.gameObject);
//      } else {
//        break;
//      }
//    }
//    return matchingTiles;
//  }

  public List<GameObject> FindMatch(Vector2 castDir, bool swap = false) { // 1
    List<GameObject> matchingTiles = new List<GameObject>(); // 2
    RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position, castDir);
    for (int i = 1; i < hits.Length; i++) {
      if (hits[i].collider != null 
          && ((!swap && hits[i].collider.GetComponent<SpriteRenderer> ().sprite == render.sprite)
              || (swap && hits[i].collider.GetComponent<Tile>().type.type == type.type))) {
        matchingTiles.Add (hits[i].collider.gameObject);
      } else {
        break;
      }
    }
    return matchingTiles;
  }

  //public List<GameObject> FindMatch(Vector2 castDir, TileMeta.GemType type)
  //{ // 1
  //  List<GameObject> matchingTiles = new List<GameObject>(); // 2
  //  RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, castDir);
  //  for (int i = 1; i < hits.Length; i++)
  //  {
  //    if (hits[i].collider != null && hits[i].collider.GetComponent<Tile>().type.type == type)
  //    {
  //      matchingTiles.Add(hits[i].collider.gameObject);
  //    }
  //    else
  //    {
  //      break;
  //    }
  //  }
  //  return matchingTiles;
  //}

  private int ClearMatch(Vector2[] paths)
  {
    int dmg = 0;

    List<GameObject> matchingTiles = new List<GameObject>();
    for (int i = 0; i < paths.Length; i++)
    {
      matchingTiles.AddRange(FindMatch(paths[i]));
    }
    if (matchingTiles.Count >= 2)
    {
      if (matchingTiles.Count >= 3){
        PanelManager.instance.addBonus(matchingTiles.ToArray());
      }
//      Debug.Log("4 match!: " + (matchingTiles.Count == 3).ToString());
//      Debug.Log("5 match!: " + (matchingTiles.Count == 4).ToString());
      for (int i = 0; i < matchingTiles.Count; i++)
      {
        PanelManager.instance.addGem (matchingTiles [i].GetComponent<Tile> ().type.type);

        //matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
        //matchingTiles[i].GetComponent<FadeMaterials>().splitSprite();
        matchingTiles[i].GetComponent<Tile>().toBeDeleted = true;

        //- [ ] Wrath => Deal more damage with fight gems
        if (matchingTiles [i].GetComponent<Tile> ().type.type == TileMeta.GemType.Fight) {
          // Triple 5 match damage. Double 4 match damage
          int pride = (int)PanelManager.instance.getCurrentMonster().pride;
          int wrath = (int)PanelManager.instance.getCurrentMonster().wrath;

          dmg += matchingTiles.Count < 3 ? 1 + wrath : (matchingTiles.Count == 3 ? 2 + pride : 3 + pride);
        }
      }
      matchFound = true;
    }
    return dmg;
  }

  public void splitSprite(){
    GetComponent<FadeMaterials>().splitSprite();
  }
    
  public void ClearAllMatches() {
    if (render.sprite == null || toBeDeleted)
      return;

    int dmg = 0;
    dmg += ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
    dmg += ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
    if (matchFound) {
      //Set the wait for turn slightly higher
      BoardManager.instance.PoppingWait();
      //GetComponent<FadeMaterials>().splitSprite();
      toBeDeleted = true;
      PanelManager.instance.addGem (type.type);
      if (type.type == TileMeta.GemType.Fight) {
        dmg += 1;
      }
      if (dmg > 0) {
        characterHit (dmg);
      }
      matchFound = false;
      StopCoroutine(BoardManager.instance.FindNullTiles());
      StartCoroutine(BoardManager.instance.FindNullTiles());
      SFXManager.instance.PlaySFX(Clip.Clear);
    }
  }

  public static void characterHit(int dmg){
    Debug.Log ("characterHit: " + dmg.ToString ());
    bool usePlayer = BoardManager.instance.getPlayerTurn ();
    if (dmg < 0) {
      usePlayer = !usePlayer;
    }

    string hBar = usePlayer ? "MOverlay" : "HOverlay";

    string atkMonster = usePlayer ? "HMonsterImg" : "MMonsterImg";
    string defMonster = usePlayer ? "MMonsterImg" : "HMonsterImg";


    if (dmg > 0)
    {
      Debug.Log("Damage: " + dmg.ToString());
      CharacterActionController atk = GameObject.Find(atkMonster).GetComponent<CharacterActionController>();
      CharacterActionController def = GameObject.Find(defMonster).GetComponent<CharacterActionController>();

      //Figure out if there is a buff for attacker
      int buff = atk.buff;
      atk.RemoveBuff();
      //If there is, add it to the dmg and remove it
      dmg += buff;
      //Figure out if there is a buff for defender
      buff = def.buff;
      dmg -= buff;
      //If there is, subtract it from the dmg and remove it
      def.RemoveBuff();

      if (dmg<0) {
        dmg = 0;
      }

      Debug.Log("Damage w/ Buff: " + dmg.ToString());

      atk.CharacterIsHitting(usePlayer);
      def.CharacterHit(usePlayer);
      def.showDamage(dmg);
    } else {
      Debug.Log("Healing: " + (dmg * -1).ToString());
      GameObject.Find(defMonster).GetComponent<CharacterActionController>().showDamage(dmg);
    }

    //GameObject.Find(atkMonster).GetComponent<CharacterActionController>().showDamage(dmg * -1);

    Debug.Log ("Using: " + hBar);

    int health = GameObject.Find (hBar).GetComponent<Progress> ().progress;
    Debug.Log ("Health Before: " + health.ToString ());
    health -= dmg;
    health = health >= 0 ? health : 0;
    int mxHlth = GameObject.Find (hBar).GetComponent<Progress> ().MAX_HEALTH;
    health = health < mxHlth ? health : mxHlth;
    Debug.Log ("Health After: " + health.ToString ());
    GameObject.Find (hBar).GetComponent<Progress> ().UpdateProgress (health);
    if (health == 0) {
      Debug.Log("PanelManager.instance.WaitUntilTileUpdate(!usePlayer);");
      PanelManager.instance.DelayedUpdateFromTile(!usePlayer);
    }
  }

}