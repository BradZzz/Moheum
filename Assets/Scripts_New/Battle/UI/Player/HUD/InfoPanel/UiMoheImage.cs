using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.UI.Utils;
using Battle.UI.Utils.Tools.UiTransform;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiMoheImage : UiListener, IUiMoheImage, IPlayerUpdateRuntime, IMoheTakeDamage
  {
    // Start is called before the first frame update
    void Awake()
    {
      UiPlayerHUD = transform.parent.GetComponentInParent<IUiPlayerHUD>();
      Image = GetComponent<Image>();
      Motion = new UiMotion(this);
      initialized = true;
    }

    public void OnPlayerUpdateRuntime()
    {
      Refresh();
    }

    private void Update()
    {
      if (!initialized)
        return;
      
      Motion?.Update();
    }

    private void Refresh()
    {
      Image.sprite = UiPlayerHUD.PlayerController.Player.Roster.CurrentMohe().BaseMohe.Data.Artwork;
      moheInstanceID = UiPlayerHUD.PlayerController.Player.Roster.CurrentMohe().InstanceID;
    }

    public UiMotion Motion { get; set; }
    
    private IUiPlayerHUD UiPlayerHUD;
    private Image Image;
    private bool initialized;
    private string moheInstanceID;
    public void OnMoheTakeDamage(string MoheInstanceID, int dmg)
    {
      if (MoheInstanceID != moheInstanceID) return;
      StartCoroutine(UIMoveMohe(transform.position));
    }
    
    IEnumerator UIMoveMohe(Vector3 position)
    {
      Motion.MoveTo(new Vector3(position.x+0.5f,position.y,position.z),1,0);
      yield return new WaitForSeconds(2f);
      Motion.MoveTo(new Vector3(position.x,position.y,position.z),1,0);
    }
  }
}
