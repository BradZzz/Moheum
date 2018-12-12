using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

  public static DialogManager instance;
  public GameObject DialogCanvas;
  public GameObject ShopCanvas;

  private Queue<DialogMeta> msgQueue;
  private bool waiting;
  private GameObject[] shopitemSubPnls;
  private bool shopActive;
  private bool dialogActive;

  private float waitingTime = .6f;

  private Dictionary<string, int> shopOwnersStock;

  private void Awake()
  {
    instance = GetComponent<DialogManager>();
    msgQueue = new Queue<DialogMeta>();
    DialogCanvas.SetActive(false);
    ShopCanvas.SetActive(false);
    waiting = false;
    shopOwnersStock = new Dictionary<string, int>();
    shopOwnersStock.Add("Bandage", 25);
    shopOwnersStock.Add("Resurrect", 100);
  }

  //private void Start()
  //{
  //  instance = this;
  //  msgQueue = new Queue<DialogMeta>();
  //  DialogCanvas.SetActive(false);
  //  DialogCanvas.transform.
  //}

  public bool ShopActive(){
    return shopActive;
  }

  public bool DialogActive(){
    return dialogActive;
  }

  void FixedUpdate()
  {
    if(Input.anyKeyDown && isShown()){
      NxtMsg();
    }
  }

  public void CloseShop(){
    shopActive = false;
    ShopCanvas.SetActive(false);
    GameObject.FindWithTag("Player").GetComponent<Move>().disableMoveTimed();
  }

  public void PurchaseShop(int click){
    Debug.Log("PurchaseShop: " + click.ToString());
    AdventureMeta meta = BaseSaver.getAdventure();
    List<string> shopKeys = new List<string>(shopOwnersStock.Keys);
    int money = meta.getYen();
    if (money>= shopOwnersStock[shopKeys[click]]) {
      meta.AddToTreasureList(shopKeys[click],1);
      meta.addYen(-shopOwnersStock[shopKeys[click]]);
      BaseSaver.putAdventure(meta);
      GameObject.FindWithTag("Player").GetComponent<PlayerMain>().playerMeta = meta;
      PopulateShop();
    } else {
      Debug.Log("Not enough ¥");
    }
  }

  public void PopulateShop(){
    shopActive = true;
    Debug.Log("PopulateShop");
    Glossary glossy = PauseManager.instance.glossaryObj.GetComponent<Glossary>();
    List<string> defeated = GameUtilities.getInteractedWith(glossy, false, true);
    if (defeated.Contains("Gaia Temple Leader Audrey") && !shopOwnersStock.ContainsKey("Stim"))
    {
      shopOwnersStock.Add("Stim", 150);
    }

    if (shopitemSubPnls == null)
    {
      shopitemSubPnls = new GameObject[]{
        GameObject.Find("ShopItemPnl01"),
        GameObject.Find("ShopItemPnl02"),
        GameObject.Find("ShopItemPnl03"),
        GameObject.Find("ShopItemPnl04"),
        GameObject.Find("ShopItemPnl05"),
        GameObject.Find("ShopItemPnl06"),
        GameObject.Find("ShopItemPnl07"),
        GameObject.Find("ShopItemPnl08"),
        GameObject.Find("ShopItemPnl09")
      };
    }

    AdventureMeta meta = BaseSaver.getAdventure();

    Dictionary<string, int> treasure = meta.GetTreasureList();
    List<string> inventoryKeys = new List<string>(treasure.Keys);
    List<string> shopKeys = new List<string>(shopOwnersStock.Keys);

    GameObject.Find("MoneyTxt").GetComponent<Text>().text = "¥" + meta.getYen();
    for (int i = 1; i < 10; i++)
    {
      if (shopKeys.Count >= i)
      {
        shopitemSubPnls[i - 1].SetActive(true);
        string img = "SI0" + i.ToString() + "Img";
        string nme = "SI0" + i.ToString() + "Name";
        string description = "SI0" + i.ToString() + "Desc";
        string qty = "SI0" + i.ToString() + "Qty";
        string cost = "SI0" + i.ToString() + "Cost";

        GameObject.Find(img).GetComponent<Image>().sprite = glossy.GetItem(shopKeys[i - 1]).GetComponent<SpriteRenderer>().sprite;
        GameObject.Find(nme).GetComponent<Text>().text = shopKeys[i - 1];
        GameObject.Find(description).GetComponent<Text>().text = glossy.GetItem(shopKeys[i - 1]).monTreas.description;
        GameObject.Find(qty).GetComponent<Text>().text = treasure.ContainsKey(shopKeys[i - 1]) ? treasure[shopKeys[i - 1]].ToString() : "0";
        GameObject.Find(cost).GetComponent<Text>().text = "¥" + shopOwnersStock[shopKeys[i - 1]].ToString();
      }
      else
      {
        shopitemSubPnls[i - 1].SetActive(false);
      }
    }
  }

  public bool hasDialog(){
    return msgQueue.Count > 0;
  }

  public bool isShown()
  {
    return DialogCanvas != null && DialogCanvas.activeSelf;
  }

  void NxtMsg(){
    Debug.Log("NxtMsg");
    if (msgQueue.Count > 0 && !waiting) {
      instance.StartCoroutine(WaitTime(waitingTime));
      Debug.Log("msgQueue.Count > 0");
      DialogMeta dialog = msgQueue.Dequeue();
      GameObject.Find("DialogText").GetComponent<Text>().text = dialog.msgNoTags();
      if (dialog.getHeal()) {
        Debug.Log("Heal");
        AdventureMeta meta = GameObject.FindWithTag("Player").GetComponent<PlayerMain>().playerMeta;
        meta.trainer = null;
        //meta.playerPos = new PosMeta(GameObject.Find("PlayerHero").transform.position);
        foreach(PlayerRosterMeta monster in meta.roster){
          monster.curHealth = monster.maxHealth;
        }
        BaseSaver.putAdventure(meta);
        BaseSaver.putBoard(GameUtilities.getBoardState(BaseSaver.getMap(), new PosMeta(GameObject.FindWithTag("Player").transform.position)));
        BaseSaver.saveState();

        instance.StartCoroutine(HealFlash());
        // Flash the screen right here
      } else if (dialog.getFight()) {
        Debug.Log("Fight");
        instance.StartCoroutine(FightFlash(true));
      } else if (dialog.getShop()) {
        ShopCanvas.SetActive(true);
        PopulateShop();
      }
    } else if (msgQueue.Count > 0 && waiting) {
      Debug.Log("Waiting");
    } else {
      //DialogCanvas.SetActive(false);
      StartCoroutine(WaitSetInactive());
    }
  }

  IEnumerator WaitTime(float time)
  {
    waiting = true;
    yield return new WaitForSeconds(time);
    waiting = false;
  }

  IEnumerator WaitSetInactive()
  {
    if (dialogActive && !waiting)
    {
      DialogCanvas.SetActive(false);
      dialogActive = false;
    }
    yield return null;
    //waiting = true;
    //yield return new WaitForSeconds(time);
    //DialogCanvas.SetActive(false);
    //waiting = false;
    //dialogActive = false;
  }

  IEnumerator HealFlash()
  {
    yield return new WaitForSeconds(waitingTime);
    GameManager.instance.HealFlash();
    yield return null;
  }

  //Iterate the fader transparency to 100%
  public static IEnumerator FightFlash(bool waitLonger)
  {
    GameObject.FindWithTag("Player").GetComponent<Move>().disableMove();
    if (waitLonger) {
      yield return new WaitForSeconds(1.5f);
    }
    yield return new WaitForSeconds(.6f);
    GameManager.instance.FightFlash();
    yield return new WaitForSeconds(3f);
    GameManager.instance.LoadScene("BejeweledScene");
    yield return null;
  }

  public void SetMsgs(Sprite image, DialogMeta[] dialogs)
  {
    dialogActive = true;
    Debug.Log("Messages Set");
    foreach(DialogMeta dialog in dialogs){
      Debug.Log(dialog.msg);
    }

    DialogCanvas.SetActive(true);
    msgQueue.Clear();
    msgQueue = new Queue<DialogMeta>(dialogs);

    GameObject.Find("DialogImage").GetComponent<Image>().sprite = image;
    NxtMsg();
  }
}
