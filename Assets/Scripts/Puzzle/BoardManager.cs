using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class BoardManager : MonoBehaviour {
	public static BoardManager instance;
	public List<Sprite> characters = new List<Sprite>();
  public Sprite[] gemLoaders;
  public TileMeta.GemType[] gemTypes;
	public GameObject tile;
	public int xSize, ySize;
  public GameType gType;

  //public bool IsShifting { get; set; }
  public bool IsProcessing { get; set; }
  public bool IsPopping { get; set; }

  public enum GameType {
    Practice, Duel, None
  }

  private bool playerTurn;
  public bool clicked;

	private GameObject[,] tiles;
  private GameObject crossHairA, crossHairB;
  private int xTurns;

  private int processingCnt;
  private float animationWait = 1f;
  private IEnumerator aiMove = null;
  private IEnumerator resetMove = null;
  private IEnumerator moveWait = null;
  private IEnumerator popping = null;
  public IEnumerator hinty;

  void Start () {
		instance = GetComponent<BoardManager>();

    crossHairA = GameObject.Find ("HCrosshairs");
    crossHairB = GameObject.Find ("MCrosshairs");

    crossHairA.SetActive(false);
    crossHairB.SetActive(false);

    Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
    CreateBoard(offset.x, offset.y);

    playerTurn = false;
    clicked = true;
    xTurns = 0;

    //SwapTurn ();
    StartCoroutine(CheckBoard());
    StartCoroutine(CheckForEnd());
  }

	private void CreateBoard (float xOffset, float yOffset) {
		tiles = new GameObject[xSize, ySize];

    float startX = transform.position.x;
		float startY = transform.position.y;

    Sprite[] previousLeft = new Sprite[ySize];
    Sprite previousBelow = null;

		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
				tiles[x, y] = newTile;

        newTile.transform.parent = transform;
        List<TileMeta.GemType> possibleGems = new List<TileMeta.GemType>(gemTypes);
        List<Sprite> possibleCharacters = new List<Sprite>(characters);
          
        possibleCharacters.Remove(previousLeft[y]);
        possibleCharacters.Remove(previousBelow);

        TileMeta meta = new TileMeta ();
        Sprite newSprite = possibleCharacters[UnityEngine.Random.Range (0, possibleCharacters.Count)];
        meta.type = gemTypes[characters.IndexOf(newSprite)];
        newTile.GetComponent<Tile> ().type = meta;
        newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
        previousLeft[y] = newSprite;
        previousBelow = newSprite;
			}
    }
    //Debug.Log("Prev Local: " + transform.localScale.ToString());
    transform.localScale = new Vector3(1.15f,1.15f,1.15f);
  }

  public void emitExplosion()
  {
    Glossary glossy = PanelManager.instance.glossaryObj.GetComponent<Glossary>();
    Transform dest = !playerTurn ? GameObject.Find("HMonsterImg").transform : GameObject.Find("MMonsterImg").transform;
    StartCoroutine(sendProjectile(glossy.GetParticleSystem("SlashParticleSystem"), true, dest, dest, SkillEffect.Effect.None, TileMeta.GemType.None, new Vector3(2,2,2)));
  }

  public void emitHearts()
  {
    Glossary glossy = PanelManager.instance.glossaryObj.GetComponent<Glossary>();
    Transform dest = playerTurn ? GameObject.Find("HMonsterImg").transform : GameObject.Find("MMonsterImg").transform;
    StartCoroutine(sendProjectile(glossy.GetParticleSystem("HeartParticleSystem"), true, dest, dest, SkillEffect.Effect.None, TileMeta.GemType.None, Vector3.one));
  }

  public void emitStars(bool player)
  {
    Glossary glossy = PanelManager.instance.glossaryObj.GetComponent<Glossary>();
    Transform dest = player ? GameObject.Find("HMonsterImg").transform : GameObject.Find("MMonsterImg").transform;
    StartCoroutine(sendProjectile(glossy.GetParticleSystem("StarParticleSystem"), true, dest, dest, SkillEffect.Effect.None, TileMeta.GemType.None, Vector3.one));
  }

  public IEnumerator provideAnimation(bool playerTurn, Transform dest, SkillEffect.Effect effect, TileMeta.GemType gem){
    Glossary glossy = PanelManager.instance.glossaryObj.GetComponent<Glossary>();
    Transform start = GameObject.Find("HMonsterImg").transform;
    if (!playerTurn)
    {
      start = GameObject.Find("MMonsterImg").transform;
    }

    GameObject throwObj;
    bool playerToGem = true;
    if (effect == SkillEffect.Effect.Change)
    {
      throwObj = glossy.GetParticleSystem("MagicParticleSystem");
      StartCoroutine(sendProjectile(throwObj, playerToGem, start, dest, effect, gem, Vector3.one));
    }
    else if (effect == SkillEffect.Effect.Sabotage)
    {
      throwObj = glossy.GetParticleSystem("PoisonParticleSystem");
      String[] playerPanels = new String[] { "HClick1", "HClick2", "HClick3", "HClick4" };
      String[] enemyPanels = new String[] { "MClick1", "MClick2", "MClick3", "MClick4" };

      List<String> destinations = new List<string>();
      for (int i = 0; i < (playerTurn ? PanelManager.instance.getComputerSkills() : PanelManager.instance.getPlayerSkills()).Count; i++)
      {
        string thisDest = playerTurn ? enemyPanels[i] : playerPanels[i];
        Transform newDest = GameObject.Find(thisDest).transform;
        StartCoroutine(sendProjectile(throwObj, playerToGem, start, newDest, effect, gem, Vector3.one));
      }
    }
    else
    {
      throwObj = glossy.GetParticleSystem("ExplosionParticleSystem");
      playerToGem = false;
      StartCoroutine(sendProjectile(throwObj, playerToGem, start, dest, effect, gem, Vector3.one));
    }

    yield return null;
  }

  public IEnumerator sendProjectile(GameObject obj, bool playerToGem, Transform start, Transform dest, SkillEffect.Effect effect, TileMeta.GemType gem, Vector3 scale)
  {
    Vector3 sPos = new Vector3(start.position.x, start.position.y, 80);
    Vector3 ePos = new Vector3(dest.position.x, dest.position.y, 80);

    GameObject projectile = Instantiate(obj, playerToGem ? sPos : ePos, Quaternion.identity);
    projectile.transform.localScale = scale;

    projectile.GetComponent<ParticleSystem>().Play();
    if (effect != SkillEffect.Effect.Sabotage && effect != SkillEffect.Effect.None)
    {
      projectile.GetComponent<ParticleSystem>().startColor = SkillEffect.ColorGem(gem);
    }
    iTween.MoveTo(projectile, playerToGem ? ePos : sPos, animationWait - .1f);
    yield return new WaitForSeconds(animationWait);
    Destroy(projectile);
  }

  public IEnumerator switchTwoTiles(Tile from, TileMeta.GemType to, Sprite img) {
    StartCoroutine(provideAnimation(playerTurn, from.transform, SkillEffect.Effect.Change, to));
    yield return new WaitForSeconds(animationWait);
    from.GetComponent<Tile>().type.type = to;
    from.GetComponent<SpriteRenderer>().sprite = img;
    //from.GetComponent<FadeMaterials>().FadeIn();
    //yield return new WaitForSeconds(.8f);
    //checkAllMatches ();
    yield return null;
  }

  public IEnumerator WaitThenCheck(){
    yield return new WaitForSeconds(animationWait);

    StartCoroutine(FindNullTiles());
  }

  public void PoppingWait(){
    IsPopping = true;
    if (popping != null) {
      StopCoroutine(popping);
    }
    popping = PopProcess();
    StartCoroutine(popping);
  }

  IEnumerator PopProcess(){
    yield return new WaitForSeconds(.5f);
    IsPopping = false;
  }

  public bool IsThinking(){
    return IsProcessing && IsPopping;
  }

  public void switchAllTiles(TileMeta.GemType from, TileMeta.GemType to)
  {
    for (int x = 0; x < xSize; x++)
    {
      for (int y = 0; y < ySize; y++)
      {
        if (tiles[x, y].GetComponent<Tile>().type.type == from)
        {
          int idx = (new List<TileMeta.GemType>(gemTypes)).IndexOf(to);
          tiles[x, y].GetComponent<Tile>().type.type = to;
          StartCoroutine(switchTwoTiles(tiles[x, y].GetComponent<Tile>(), to, characters[idx]));
        }
      }
    }
    StartCoroutine(WaitThenCheck());
  }

  //- [ ] Lust => Change more gems with skill
  public void switchSomeTiles(TileMeta.GemType from, TileMeta.GemType to, int min, int max){
    min += (int) PanelManager.instance.getCurrentMonster().lust;
    max += (int) PanelManager.instance.getCurrentMonster().lust;
      
    List<GameObject> validTiles = new List<GameObject> ();
    for (int x = 0; x < xSize; x++) {
      for (int y = 0; y < ySize; y++) {
        if (tiles[x, y].GetComponent<Tile>().type.type == from) {
          validTiles.Add (tiles[x, y]);
        }
      }
    }
    GameObject[] endTiles = validTiles.ToArray ();
    GameUtilities.ShuffleArray (endTiles);
    int maxTls = UnityEngine.Random.Range (min, max + 1);
    for(int i = 0; i < maxTls && i < endTiles.Length; i++){
      int idx = (new List<TileMeta.GemType>(gemTypes)).IndexOf(to);
      endTiles [i].GetComponent<Tile> ().type.type = to;
      StartCoroutine(switchTwoTiles(endTiles [i].GetComponent<Tile> (), to, characters [idx]));
    }
    StartCoroutine(WaitThenCheck());
  }

  public void buff(int amount)
  {
    string bMonster = playerTurn ? "HMonsterImg" : "MMonsterImg";
    int currentBuff = GameObject.Find(bMonster).GetComponent<CharacterActionController>().buff;
    GameObject.Find(bMonster).GetComponent<CharacterActionController>().SetBuff(currentBuff + amount);
  }

  public void Sabotage(int amount){
    if(playerTurn)
    {
      StartCoroutine(provideAnimation(playerTurn, GameObject.Find("MMonsterImg").transform, SkillEffect.Effect.Sabotage, TileMeta.GemType.Purple));
    } else {
      StartCoroutine(provideAnimation(playerTurn, GameObject.Find("HMonsterImg").transform, SkillEffect.Effect.Sabotage, TileMeta.GemType.Purple));
    }

    PanelManager.instance.removeGems(amount);
  }

  //- [ ] Greed => Destroy more gems with skill
  public void SliceTile(Tile toSlice, int radius)
  {
    Vector2 pos = Vector2.zero;
    Debug.Log("Slice started");

    for (int x = 0; x < xSize; x++)
    {
      for (int y = 0; y < ySize; y++)
      {
        if (tiles[x, y].GetComponent<Tile>() == toSlice)
        {
          pos = new Vector2(x, y);
          Debug.Log("Found at: " + pos.ToString());
          break;
        }
      }
    }

    int dmg = 0;

    for (int i = 0; i <= radius; i++){
      if (i > 0) {
        Vector2[] dirs = new Vector2[]{
          ((Vector2.left + Vector2.up) * i) + pos,
          ((Vector2.left + Vector2.down) * i) + pos,
          ((Vector2.right + Vector2.up) * i) + pos,
          ((Vector2.right + Vector2.down) * i) + pos
        };

        foreach(Vector2 dir in dirs){
          if (dir.x < xSize && dir.y < ySize) {
            Tile pkTile = tiles[(int)dir.x, (int)dir.y].GetComponent<Tile>();
            if (pkTile.type.type == TileMeta.GemType.Fight)
            {
              dmg++;
            }
            StartCoroutine(DestroyTile(pkTile));
          }
        }
      } else {
        Tile pkTile = tiles[(int)pos.x, (int)pos.y].GetComponent<Tile>();
        if (pkTile.type.type == TileMeta.GemType.Fight)
        {
          dmg++;
        }
        StartCoroutine(DestroyTile(pkTile));
      }
    }

    Tile.characterHit(dmg);
    StartCoroutine(FindNullTiles());
  }

  //- [ ] Greed => Destroy more gems with skill
  public void PokeTile(Tile toPoke, int radius){
    Vector2 pos = Vector2.zero;
    Debug.Log("Poke started");

    for (int x = 0; x < xSize; x++)
    {
      for (int y = 0; y < ySize; y++)
      {
        if (tiles[x, y].GetComponent<Tile>() == toPoke)
        {
          pos = new Vector2(x,y);
          Debug.Log("Found at: " + pos.ToString());
          break;
        }
      }
    }

    int dmg = 0;
    Vector2 xLU = new Vector2(pos.x - radius >= 0 ? pos.x - radius : 0, pos.x + radius + 1 < xSize ? pos.x + radius + 1 : xSize);
    Vector2 yLU = new Vector2(pos.y - radius >= 0 ? pos.y - radius : 0, pos.y + radius + 1 < ySize ? pos.y + radius + 1 : ySize);

    Debug.Log("xLU: " + xLU.ToString());
    Debug.Log("yLU: " + yLU.ToString());

    for (int x = (int) xLU.x; x < (int)xLU.y; x++)
    {
      for (int y = (int)yLU.x; y < (int)yLU.y; y++)
      {
        Tile pkTile = tiles[x, y].GetComponent<Tile>();
        if (pkTile.type.type == TileMeta.GemType.Fight)
        {
          dmg++;
        }
        StartCoroutine(DestroyTile(pkTile));
      }
    }
    Tile.characterHit(dmg);
    StartCoroutine(FindNullTiles());
  }

  public IEnumerator CheckBoard()
  {
    for (int x = 0; x < xSize; x++)
    {
      for (int y = 0; y < ySize; y++)
      {
        if (tiles[x, y].GetComponent<SpriteRenderer>().sprite == null){
          StartCoroutine(FindNullTiles());
          break;
        }
      }
    }
    yield return new WaitForSeconds(1f);
    StartCoroutine(CheckBoard());
    yield return null;
  }

  public IEnumerator DestroyTile(Tile toRemove) {
    StartCoroutine(provideAnimation(playerTurn, toRemove.transform, SkillEffect.Effect.Destroy, toRemove.type.type));
    //yield return new WaitForSeconds(animationWait /2);
    PanelManager.instance.addGem(toRemove.type.type);
    toRemove.GetComponent<Tile>().toBeDeleted = true;
    yield return null;
  }

  public void destroyTiles(TileMeta.GemType toRemove){
    int dmg = 0;
    List<GameObject> validTiles = new List<GameObject> ();
    for (int x = 0; x < xSize; x++) {
      for (int y = 0; y < ySize; y++) {
        if (tiles[x, y].GetComponent<Tile>().type.type == toRemove) {
          StartCoroutine(DestroyTile(tiles [x, y].GetComponent<Tile> ()));
          if (toRemove == TileMeta.GemType.Fight) {
            dmg++;
          }
        }
      }
    }
    Tile.characterHit(dmg);
    StartCoroutine (FindNullTiles());
  }

  //- [ ] Greed => Destroy more gems with skill
  public void destroySomeTiles(TileMeta.GemType toRemove, int min, int max){
    min += (int)PanelManager.instance.getCurrentMonster().greed;
    max += (int)PanelManager.instance.getCurrentMonster().greed;

    int dmg = 0;
    List<GameObject> validTiles = new List<GameObject> ();
    for (int x = 0; x < xSize; x++) {
      for (int y = 0; y < ySize; y++) {
        if (tiles[x, y].GetComponent<Tile>().type.type == toRemove) {
          validTiles.Add (tiles[x, y]);
        }
      }
    }
    GameObject[] endTiles = validTiles.ToArray ();
    GameUtilities.ShuffleArray (endTiles);
    int maxTls = UnityEngine.Random.Range (min, max + 1);
    for(int i = 0; i < maxTls && i < endTiles.Length; i++){
      StartCoroutine(DestroyTile(endTiles [i].GetComponent<Tile> ()));
      if (toRemove == TileMeta.GemType.Fight)
      {
        dmg++;
      }
    }
    Tile.characterHit(dmg);
    StartCoroutine (FindNullTiles());
  }

  public void reset(bool anim)
  {
    if (resetMove != null) {
      StopCoroutine(resetMove);
    }
    resetMove = findNullLoop(anim);
    StartCoroutine (resetMove);
  }

  private IEnumerator findNullLoop(bool anim){
    if (anim)
    {
      GameObject background = GameObject.Find("Background");
      Color prev = background.GetComponent<SpriteRenderer>().color;
      background.GetComponent<SpriteRenderer>().color = new Color(1, .54f, .54f);
      iTween.ShakePosition(GameObject.Find("BoardManager"), new Vector3(1, 0, 0), animationWait - .4f);
      yield return new WaitForSeconds(animationWait - .4f);
      background.GetComponent<SpriteRenderer>().color = prev;
    }
    for (int x = 0; x < xSize; x++)
    {
      for (int y = 0; y < ySize; y++)
      {
        tiles[x, y].GetComponent<Tile>().toBeDeleted = true;

      }
    }
    StartCoroutine(FindNullTiles(0));
  }

  public void addExtraTurns(int turns){
    xTurns += turns;
  }

  public void deselectAll()
  {
    for (int x = 0; x < xSize; x++)
    {
      for (int y = 0; y < ySize; y++)
      {
        tiles[x, y].GetComponent<Tile>().Deselect();
      }
    }
  }

  public bool checkAllMatches(){
    bool blankTiles = false;
    for (int x = 0; x < xSize; x++) {
      for (int y = 0; y < ySize; y++) {
        tiles [x, y].GetComponent<Tile> ().ClearAllMatches ();
      }
    }
    for (int x = 0; x < xSize; x++)
    {
      for (int y = 0; y < ySize; y++)
      {
        Tile tileTBD = tiles[x, y].GetComponent<Tile>();
        if (tileTBD.toBeDeleted) {
          tileTBD.splitSprite();
          blankTiles = true;
          tileTBD.toBeDeleted = false;
        }
      }
    }
    //StopCoroutine(BoardManager.instance.FindNullTiles());
    //StartCoroutine(BoardManager.instance.FindNullTiles());
    return blankTiles;
  }

  /*
   * End Skill Effect Functions
   */

  public bool GameOver(){
    return GameObject.Find ("MOverlay").GetComponent<Progress> ().progress <= 0 || GameObject.Find ("HOverlay").GetComponent<Progress> ().progress <= 0;
  }

  public void setClicked(bool clicked){
    this.clicked = clicked;
  }

  public bool getPlayerTurn(){
    return playerTurn;
  }

  public IEnumerator FindNullTiles(float delay = .13f) {
    //if (hinty != null) { StopCoroutine(hinty); }
    if (moveWait != null){StopCoroutine(moveWait);}
    Debug.Log("FindNullTiles: " + delay.ToString());
    deselectAll();
    IsProcessing = true;
    bool tilesBlank=true;
    processingCnt = 0;
    while(tilesBlank) 
    {
      for (int x = 0; x < xSize; x++) {
        yield return StartCoroutine(ShiftTilesDown(x, delay));
      }
      tilesBlank = checkAllMatches();
      if (tilesBlank) {
        yield return new WaitForSeconds(delay > 0 ? .05f : .01f);
      }
    }
    IsProcessing = false;
    StartCoroutine(CheckForEnd());
  }

  class coords {
    public Tile tile;
    public Vector2 dir;
    public coords(Tile tile, Vector2 dir){
      this.tile = tile;
      this.dir = dir;
    }
  }

  public IEnumerator waitHint(){
    yield return new WaitForSeconds(20f);
    if (!GameManager.instance.gameOver)
    {

      List<coords> validMoves = new List<coords>();
      int[,] tempBoard = new int[xSize, ySize];
      for (int x = 0; x < xSize; x++)
      {
        for (int y = 0; y < ySize; y++)
        {
          tempBoard[x, y] = (int)tiles[x, y].GetComponent<Tile>().type.type;
          tiles[x, y].GetComponent<Tile>().Deselect();
        }
      }

      List<Vector2> positions = new List<Vector2>();

      int[,] scoreBoard = new int[xSize, ySize];
      for (int x = 0; x < xSize; x++)
      {
        for (int y = 0; y < ySize; y++)
        {
          foreach (Vector2 dir in Tile.adjacentDirections)
          {
            int[,] copyBoard = tempBoard.Clone() as int[,];
            if (dir.x + x >= 0 && dir.x + x < xSize && dir.y + y >= 0 && dir.y + y < ySize)
            {
              int temp = copyBoard[(int)dir.x + x, (int)dir.y + y];
              copyBoard[(int)dir.x + x, (int)dir.y + y] = copyBoard[x, y];
              copyBoard[x, y] = temp;
              // At the two points selected, look out in all the directions and see if we've found a match
              int mtchA = returnBoardMatches(copyBoard, new Vector2(x, y));
              int mtchB = returnBoardMatches(copyBoard, new Vector2((int)dir.x + x, (int)dir.y + y));

              int score = mtchA > mtchB ? mtchA : mtchB;
              scoreBoard[x, y] = score;

              if (score > 2)
              {
                positions.Add(new Vector2(x, y));
                validMoves.Add(new coords(tiles[x, y].GetComponent<Tile>(), dir));
              }
            }

          }
        }
      }

      coords[] moves = validMoves.ToArray();
      GameUtilities.ShuffleArray(moves);

      if (moves[0].tile.GetAdjacent(moves[0].dir) != null)
      {
        moves[0].tile.SelectHint();
        moves[0].tile.GetAdjacent(moves[0].dir).GetComponent<Tile>().SelectHint();
      }
    }
  }

  //public void StartAI()
  //{
  //   StartCoroutine(loopAIMovement());
  //}

  public void stopAI(){
    if (aiMove != null)
    {
      StopCoroutine(aiMove);
    }
  }

  public IEnumerator loopAIMovement(){
    stopAI();
    if (!getPlayerTurn())
    {
      aiMove = MoveAI();
      int counter = 0;
      int checksPassed = 0;
      while (checksPassed < 4 && counter < 30)
      {
        yield return new WaitForSeconds(.25f);
        counter++;
        if (!IsThinking())
        {
          checksPassed++;
        }
        else
        {
          checksPassed = 0;
        }
      }
      if (!getPlayerTurn())
      {
        StartCoroutine(aiMove);
      }
      yield return null;
    }
  }

  /*
   * ai sometimes moves for player
   * gems sometimes given to player when they are not players gems
   */
  public IEnumerator MoveAI() {
    bool hasSpecial = false;
    List<SkillMeta> compSkills = new List<SkillMeta> (PanelManager.instance.getComputerSkills ());
    for(int i =0; i < compSkills.Count; i++){
      bool ready = true;
      foreach(SkillReq req in new SkillReq[]{compSkills[i].req1,compSkills[i].req2}){
        if (req.has != req.req){
          ready = false;
        }
      }
      if (ready) {
        hasSpecial = true;
        PanelManager.instance.setActiveSkill (i);
        yield return new WaitForSeconds(.5f);

        bool found = false;
        List<Tile> toPoke = new List<Tile>();
        SkillMeta meta = compSkills[i];
        foreach(SkillEffect eff in meta.effects){
          if (eff.effect == SkillEffect.Effect.Poke || eff.effect == SkillEffect.Effect.Slice) {
            TileMeta.GemType tileType;
            if (eff.effect == SkillEffect.Effect.Poke) {
              tileType = ((SkillEffect.PokeSkill)eff).toRemove;
            } else {
              tileType = ((SkillEffect.SliceSkill)eff).toRemove;
            }
            for (int x = 0; x < xSize; x++){
              for (int y = 0; y < ySize; y++){
                if (tiles[x, y].GetComponent<Tile>().type.type == tileType)
                {
                  toPoke.Add(tiles[x, y].GetComponent<Tile>());
                }
              }
            }
            found = true;
          }
        }

        if (!found) {
          tiles[0, 0].GetComponent<Tile>().computerUseSkill();
        } else if (toPoke.Count > 0) {
          Tile[] pkArr = toPoke.ToArray();
          GameUtilities.ShuffleArray(pkArr);
          pkArr[0].computerUseSkill();
        }

        yield return new WaitForSeconds(.5f);
//        while(IsShifting && IsProcessing){}
        //Debug.Log("IsShifting: " + isShifting().ToString());
        Debug.Log("IsProcessing: " + IsThinking().ToString());
        int counter = 0;
        int checksPassed = 0;
        while (checksPassed < 3 && counter < 30) {
          yield return new WaitForSeconds(.25f);
          counter++;
          if (!IsThinking()) {
            checksPassed++;
          } else {
            checksPassed = 0;
          }
        }
        //yield return new WaitForSeconds(.25f);
        StartCoroutine(loopAIMovement());
        break;
      }
    }
    if (!hasSpecial) {
      // See which gems the computer needs to complete it's skills
      List<TileMeta.GemType> preferredGems = new List<TileMeta.GemType>();
      preferredGems.Add (TileMeta.GemType.Fight);
      for (int i = 0; i < compSkills.Count; i++) {
        foreach (SkillReq req in new SkillReq[]{compSkills[i].req1,compSkills[i].req2}) {
          if (req.has != req.req && !preferredGems.Contains(req.gem)) {
            preferredGems.Add (req.gem);
          }
        }
      }

      int counter = 0;
      int checksPassed = 0;
      while (checksPassed < 3 && counter < 30)
      {
        yield return new WaitForSeconds(.25f);
        counter++;
        if (!IsThinking())
        {
          checksPassed++;
        }
        else
        {
          checksPassed = 0;
        }
      }

      List<coords> validMoves = new List<coords> ();
      List<coords> preferredMoves = new List<coords> ();
      int max = 0;
      int[,] tempBoard = new int[xSize, ySize];
      for (int x = 0; x < xSize; x++){
        for (int y = 0; y < ySize; y++){
          tempBoard[x,y] = (int) tiles[x, y].GetComponent<Tile>().type.type;
        }
      }

      // board 0,0 = temp 0,9

      List<Vector2> positions = new List<Vector2>();

      int[,] scoreBoard = new int[xSize, ySize];
      for (int x = 0; x < xSize; x++) {
        for (int y = 0; y < ySize; y++) {
          foreach (Vector2 dir in Tile.adjacentDirections)
          {
            int[,] copyBoard = tempBoard.Clone() as int[,];
            if (dir.x + x >= 0 && dir.x + x < xSize && dir.y + y >= 0 && dir.y + y < ySize) {
              int temp = copyBoard[(int)dir.x + x, (int)dir.y + y];
              copyBoard[(int)dir.x + x, (int)dir.y + y] = copyBoard[x, y];
              copyBoard[x, y] = temp;
              // At the two points selected, look out in all the directions and see if we've found a match
              int mtchA = returnBoardMatches(copyBoard, new Vector2(x,y));
              int mtchB = returnBoardMatches(copyBoard, new Vector2((int)dir.x + x, (int)dir.y + y));

              int score = mtchA > mtchB ? mtchA : mtchB;
              scoreBoard[x, y] = score;

              if (score > 2) {
                positions.Add(new Vector2(x,y));

                validMoves.Add(new coords(tiles[x, y].GetComponent<Tile>(), dir));
                if (preferredGems.Contains(tiles[x, y].GetComponent<Tile>().type.type)){
                  score++;
                }

                if (max < score)
                {
                  Debug.Log("New Max!" + score.ToString());
                  preferredMoves.Clear();
                  max = score;
                }
                if (max == score)
                {
                  preferredMoves.Add(new coords(tiles[x, y].GetComponent<Tile>(), dir));
                }
              }
            }

          }
        }
      }

      coords[] moves = validMoves.ToArray ();
      coords[] prefMoves = preferredMoves.ToArray ();

      GameUtilities.ShuffleArray (moves);
      GameUtilities.ShuffleArray (prefMoves);

      if (prefMoves.Length > 0) {
        Debug.Log ("prefMoves: " + prefMoves.Length.ToString());
        prefMoves [0].tile.Select ();
        yield return new WaitForSeconds (.6f);
        prefMoves [0].tile.computerSwap (prefMoves [0].dir);
        moveWait = WaitForMovement();
        StartCoroutine(moveWait);
        yield return null; 
      } else if (moves.Length > 0) {
        Debug.Log ("moves: " + moves.Length.ToString());
        Debug.Log("moves: " + moves.Length.ToString());
        moves [0].tile.Select ();
        yield return new WaitForSeconds (.6f);
        moves [0].tile.computerSwap (moves [0].dir);
        moveWait = WaitForMovement();
        StartCoroutine(moveWait);
        yield return null; 
      } else {
        // There aren't any moves to make, reset the board and try again
        reset(false);
        counter = 0;
        while (IsThinking() && counter < 30)
        {
          yield return new WaitForSeconds(.25f);
          counter++;
          Debug.Log("Counter: " + counter);
          //Debug.Log("IsShifting: " + isShifting().ToString());
          Debug.Log("IsProcessing: " + IsThinking().ToString());
        }
        //yield return new WaitForSeconds(.25f);
        StartCoroutine(loopAIMovement());
      }
    }
  }

  IEnumerator WaitForMovement(){
    yield return new WaitForSeconds(2f);
    if (!IsThinking() && !playerTurn) {
      StartCoroutine(loopAIMovement());
    }
  }

  public int returnBoardMatches(int[,] board, Vector2 pos){
    int LR = 1;
    int UD = 1;

    int thisGem = board[(int)pos.x,(int)pos.y];
    Vector2 cPos;

    // L/R
    cPos = pos + Vector2.left;
    while (cPos.x >= 0 && cPos.x < xSize && board[(int)cPos.x, (int)cPos.y] == thisGem){
      LR += 1;
      cPos = cPos + Vector2.left;
    }

    cPos = pos + Vector2.right;
    while (cPos.x >= 0 && cPos.x < xSize && board[(int)cPos.x, (int)cPos.y] == thisGem)
    {
      LR += 1;
      cPos = cPos + Vector2.right;
    }

    // L/R
    cPos = pos + Vector2.up;
    while (cPos.y >= 0 && cPos.y < ySize && board[(int)cPos.x, (int)cPos.y] == thisGem)
    {
      UD += 1;
      cPos = cPos + Vector2.up;
    }

    cPos = pos + Vector2.down;
    while (cPos.y >= 0 && cPos.y < ySize && board[(int)cPos.x, (int)cPos.y] == thisGem)
    {
      UD += 1;
      cPos = cPos + Vector2.down;
    }

    return UD > LR ? UD : LR;
  }

  public bool MovesLeft(){
    bool movesLeft = false;
    for (int x = 0; x < xSize && !movesLeft; x++)
    {
      for (int y = 0; y < ySize && !movesLeft; y++)
      {
        Tile tile = tiles[x, y].GetComponent<Tile>();
        foreach (Vector2 dir in Tile.adjacentDirections)
        {
          if (tile.checkSwap(dir))
          {
            //Debug.Log("moves found: (" + x.ToString() + "," + y.ToString() + ") : " + dir.ToString());
            return true;
          }
        }
      }
    }
    return false;
  }

  public IEnumerator CheckForEnd() {
    bool movesLeft = MovesLeft();
    //Debug.Log ("movesLeft: " + movesLeft);
    if (!movesLeft) {
      reset (false);
    } else {
      // If there aren't any more moves, toggle the player
      if (xTurns > 0) {
        xTurns--;
        if (!getPlayerTurn()) {
          StartCoroutine(loopAIMovement());
        }
      } else if (gType == BoardManager.GameType.Duel) {
        if (getPlayerTurn())
        {
          int counter = 0;
          int checksPassed = 0;
          while (checksPassed < 3 && counter < 30)
          {
            yield return new WaitForSeconds(.1f);
            counter++;
            if (!IsThinking())
            {
              checksPassed++;
            }
            else
            {
              checksPassed = 0;
            }
          }
        }
        StartCoroutine(SwapTurn());
      }
    }
    yield return null; 
  }

  private IEnumerator SwapTurn(){
    if (clicked)
    {
      clicked = false;
      if (!playerTurn)
      {
        int counter = 0;
        int checksPassed = 0;
        while (checksPassed < 3 && counter < 30)
        {
          yield return new WaitForSeconds(.25f);
          counter++;
          if (!IsThinking())
          {
            checksPassed++;
          }
          else
          {
            checksPassed = 0;
          }
        }
      }
      playerTurn = !playerTurn;
      CheckTurn();
    }
  }

  //private void SwapTurn(){
  //  if (clicked) {
  //    clicked = false;
  //    yield return new WaitForSeconds(.25f);
  //    playerTurn = !playerTurn;
  //    CheckTurn ();
  //  }
  //}

  //public IEnumerator WaitForAIMove()
  //{
  //  int counter = 0;
  //  int checksPassed = 0;
  //  while (checksPassed < 2 && counter < 30)
  //  {
  //    yield return new WaitForSeconds(.5f);
  //    counter++;
  //    if (!IsProcessing)
  //    {
  //      checksPassed++;
  //    }
  //    else
  //    {
  //      checksPassed = 0;
  //    }
  //  }
  //  StartCoroutine(MoveAI());
  //  yield return null;
  //}

  private void CheckTurn(){
    if (gType == BoardManager.GameType.Duel) {
      if (hinty != null) { StopCoroutine(hinty); }
      if (playerTurn) {
        hinty = waitHint();
        StartCoroutine(hinty);
        crossHairA.SetActive (true);
        crossHairB.SetActive (false);
      } else {
        crossHairA.SetActive (false);
        crossHairB.SetActive (true);
        if (!GameOver()) {
          PanelManager.instance.IncrementCatchTurns();
          StartCoroutine(loopAIMovement());
          //StartCoroutine(WaitForAIMove());
        }
      }
    }
  }

  public void afterPlayerMove(object cmpParams)
  {
    Debug.Log("<==== afterPlayerMove =====>");
    Hashtable hstbl = (Hashtable)cmpParams;
    Destroy((GameObject)hstbl["obj"]);
  }

  //public bool isShifting(){
  //  return IsShifting;
  //}

  public IEnumerator AnimateGemSwap(GameObject prev, GameObject curr)
  {
    GameObject tempA = Instantiate(prev, prev.transform.position, prev.transform.rotation, GameObject.Find("BoardManager").transform);
    tempA.GetComponent<BoxCollider2D>().enabled = false;
    iTween.MoveTo(tempA, curr.transform.position, .1f);

    GameObject tempB = Instantiate(curr, curr.transform.position, curr.transform.rotation, GameObject.Find("BoardManager").transform);
    tempB.GetComponent<BoxCollider2D>().enabled = false;
    iTween.MoveTo(tempB, prev.transform.position, .1f);

    Color sprA = Color.white;
    sprA.a = 0;
    curr.GetComponent<SpriteRenderer>().color = sprA;
    prev.GetComponent<SpriteRenderer>().color = sprA;


    Sprite tempSprite = curr.GetComponent<SpriteRenderer>().sprite;
    curr.GetComponent<SpriteRenderer>().sprite = prev.GetComponent<SpriteRenderer>().sprite;
    prev.GetComponent<SpriteRenderer>().sprite = tempSprite;
    //end

    TileMeta metaA = new TileMeta();
    int idxA = characters.IndexOf(curr.GetComponent<SpriteRenderer>().sprite);
    metaA.type = gemTypes[idxA > -1 ? idxA : 0];

    TileMeta metaB = new TileMeta();
    int idxB = characters.IndexOf(prev.GetComponent<SpriteRenderer>().sprite);
    metaB.type = gemTypes[idxB > -1 ? idxB : 0];

    curr.GetComponent<Tile>().type = metaA;
    prev.GetComponent<Tile>().type = metaB;

    yield return new WaitForSeconds(.1f);

    sprA.a = 1;
    curr.GetComponent<SpriteRenderer>().color = sprA;
    prev.GetComponent<SpriteRenderer>().color = sprA;

    Destroy(tempA);
    Destroy(tempB);

    yield return null;
  }

  //public IEnumerator AnimateGemFall(Vector3 dest, GameObject prev, GameObject curr, int x)
  //{
  //  if (GameObject.Find("BoardManager") != null)
  //  {
  //    GameObject temp = Instantiate(prev, prev.transform.position, prev.transform.rotation, GameObject.Find("BoardManager").transform);



  //    temp.GetComponent<BoxCollider2D>().enabled = false;
  //    iTween.MoveTo(temp, dest, .1f);

  //    Color sprA = curr.GetComponent<SpriteRenderer>().color;
  //    Color sprB = prev.GetComponent<SpriteRenderer>().color;
  //    sprA.a = 0;
  //    sprB.a = 0;
  //    curr.GetComponent<SpriteRenderer>().color = sprA;
  //    prev.GetComponent<SpriteRenderer>().color = sprB;

  //    curr.GetComponent<SpriteRenderer>().sprite = prev.GetComponent<SpriteRenderer>().sprite;
  //    prev.GetComponent<SpriteRenderer>().sprite = GetNewSprite(x, ySize - 1);
  //    //end

  //    TileMeta metaA = new TileMeta();
  //    int idxA = characters.IndexOf(curr.GetComponent<SpriteRenderer>().sprite);
  //    metaA.type = gemTypes[idxA > -1 ? idxA : 0];

  //    TileMeta metaB = new TileMeta();
  //    int idxB = characters.IndexOf(prev.GetComponent<SpriteRenderer>().sprite);
  //    metaB.type = gemTypes[idxB > -1 ? idxB : 0];
        
  //    curr.GetComponent<Tile>().type = metaB;
  //    prev.GetComponent<Tile>().type = metaB;

  //    yield return new WaitForSeconds(.1f);

  //    sprA.a = 1;
  //    sprB.a = 1;
  //    curr.GetComponent<SpriteRenderer>().color = sprA;
  //    prev.GetComponent<SpriteRenderer>().color = sprB;
  //    Destroy(temp);
  //  }

  //  yield return null;
  //}

  public IEnumerator AnimateGemFall2(GameObject lower, Sprite newGem, float delay)
  {
    if (GameObject.Find("BoardManager") != null)
    {
      Vector3 fallStart = lower.transform.position;
      fallStart.y += newGem.rect.height + 10;
      GameObject temp = Instantiate(lower, fallStart, lower.transform.rotation, GameObject.Find("BoardManager").transform);
      temp.GetComponent<SpriteRenderer>().sprite = newGem;
      temp.GetComponent<BoxCollider2D>().enabled = false;

      Color sprA = lower.GetComponent<SpriteRenderer>().color;
      sprA.a = 0;
      lower.GetComponent<SpriteRenderer>().color = sprA;
      lower.GetComponent<SpriteRenderer>().sprite = newGem;

      TileMeta metaA = new TileMeta();
      int idxA = characters.IndexOf(newGem);
      metaA.type = gemTypes[idxA > -1 ? idxA : 0];
      lower.GetComponent<Tile>().type = metaA;

      iTween.MoveTo(temp, lower.transform.position, delay);
      yield return new WaitForSeconds(delay);

      sprA.a = 1;
      lower.GetComponent<SpriteRenderer>().color = sprA;
      Destroy(temp);
    }
    yield return null;
  }

  public IEnumerator AnimateGemFall2(GameObject lower, GameObject upper, float delay)
  {
    if (GameObject.Find("BoardManager") != null)
    {
      GameObject temp = Instantiate(upper, upper.transform.position, upper.transform.rotation, GameObject.Find("BoardManager").transform);
      temp.GetComponent<BoxCollider2D>().enabled = false;
      Color sprA = upper.GetComponent<SpriteRenderer>().color;
      sprA.a = 0;
      upper.GetComponent<SpriteRenderer>().color = sprA;
      lower.GetComponent<SpriteRenderer>().color = sprA;
      iTween.MoveTo(temp, lower.transform.position, delay);
      lower.GetComponent<SpriteRenderer>().sprite = upper.GetComponent<SpriteRenderer>().sprite;
      TileMeta metaA = new TileMeta();
      metaA.type = upper.GetComponent<Tile>().type.type;
      lower.GetComponent<Tile>().type = metaA;
      upper.GetComponent<SpriteRenderer>().sprite = null;
      upper.GetComponent<Tile>().type.type = TileMeta.GemType.None;
      yield return new WaitForSeconds(delay);
      sprA.a = 1;
      upper.GetComponent<SpriteRenderer>().color = sprA;
      lower.GetComponent<SpriteRenderer>().color = sprA;
      Destroy(temp);
    }
    yield return null;
  }

  private IEnumerator ShiftTilesDown(int x, float shiftDelay = .13f)
  {
    List<GameObject> renders = new List<GameObject>();
    int nullCount = 0;

    for (int y = 0; y < ySize; y++)
    {
      GameObject render = tiles[x, y];
      if (render.GetComponent<SpriteRenderer>().sprite == null)
      {
        nullCount++;
      }
      renders.Add(render);
    }

    for (int i = 0; i < nullCount; i++)
    {
      yield return new WaitForSeconds(shiftDelay);
      for (int k = 0; k < ySize; k++)
      {
        if (k < ySize - 1){
          if (renders[k].GetComponent<SpriteRenderer>().sprite == null)
          {
            StartCoroutine(AnimateGemFall2(renders[k], renders[k+1], shiftDelay > 0 ? .05f : 0));
          }
        } else {
          if (renders[k].GetComponent<SpriteRenderer>().sprite == null)
          {
            StartCoroutine(AnimateGemFall2(renders[k], GetNewSprite(x, ySize - 1), shiftDelay > 0 ? .05f : 0));
          }
        }
      }
    }
  }

  private Sprite GetNewSprite(int x, int y) {
    List<Sprite> possibleCharacters = new List<Sprite>();
    possibleCharacters.AddRange(characters);

    if (x > 0)
    {
      possibleCharacters.Remove(tiles[x - 1, y].GetComponent<SpriteRenderer>().sprite);
    }
    if (x < xSize - 1)
    {
      possibleCharacters.Remove(tiles[x + 1, y].GetComponent<SpriteRenderer>().sprite);
    }
    if (y > 0)
    {
      possibleCharacters.Remove(tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite);
    }

    return possibleCharacters[UnityEngine.Random.Range(0, possibleCharacters.Count)];
  }
}
