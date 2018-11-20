using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FadeMaterials : MonoBehaviour {

  public void FadeOut() {
    iTween.ValueTo(gameObject, iTween.Hash(
    "from", 1.0f, "to", 0.0f,
    "time", .5f, "easetype", "linear",
      "onupdate", "setAlpha"));
  }
  public void FadeIn() {
    iTween.ValueTo(gameObject, iTween.Hash(
    "from", 0f, "to", 1f,
    "time", .5f, "easetype", "linear",
      "onupdate", "setAlpha"));
  }
  public void setAlpha(float newAlpha) {

    Material mObj = GetComponent<Renderer> ().material;

//    foreach (Material mObj in GetComponent<Renderer>().material) {
      mObj.color = new Color(
        mObj.color.r, mObj.color.g, 
        mObj.color.b, newAlpha);
//    }
  }

  private IEnumerator Split()
  {
    if (GameObject.Find("BoardManager") != null)
    {

      //StartCoroutine(EmitParticleEffect());

      //GameObject temp = Instantiate(gameObject, transform.position, transform.rotation, GameObject.Find("BoardManager").transform);
      //temp.GetComponent<BoxCollider2D>().enabled = false;
      //gameObject.GetComponent<SpriteRenderer>().sprite = null;
      //gameObject.GetComponent<Tile>().type.type = TileMeta.GemType.None;

      float scale = .2f;

      List<Vector3> dirs = new List<Vector3>
      {
        transform.position + (Vector3.left * scale),
        transform.position + (Vector3.right * scale),
        transform.position + (Vector3.up * scale),
        transform.position + (Vector3.down * scale),
        transform.position + ((Vector3.left + Vector3.up) * scale),
        transform.position + ((Vector3.left + Vector3.down) * scale),
        transform.position + ((Vector3.right + Vector3.up) * scale),
        transform.position + ((Vector3.right + Vector3.down) * scale)
      };

      List<GameObject> projectiles = new List<GameObject>();

      //GameObject temp = Instantiate(gameObject, transform.position, transform.rotation, GameObject.Find("BoardManager").transform);
      //temp.GetComponent<BoxCollider2D>().enabled = false;
      //temp.gameObject.transform.localScale = new Vector3(.1f, .1f, .1f);
      //iTween.RotateTo(temp, new Vector3(0, 0, 1), .2f);
      //projectiles.Add(temp);

      foreach(Vector3 dir in dirs){
        GameObject temp = Instantiate(gameObject, transform.position, transform.rotation, GameObject.Find("BoardManager").transform);
        temp.GetComponent<BoxCollider2D>().enabled = false;
        temp.gameObject.transform.localScale = new Vector3(.025f,.025f,.025f);
        iTween.MoveTo(temp, dir, .2f);
        iTween.RotateTo(temp, new Vector3(0, 0, 1),.2f);
        projectiles.Add(temp);
      }

      gameObject.GetComponent<SpriteRenderer>().sprite = null;
      gameObject.GetComponent<Tile>().type.type = TileMeta.GemType.None;

      yield return new WaitForSeconds(.2f);

      foreach(GameObject proj in projectiles){
        Destroy(proj);
      }

      //iTween.RotateTo(temp, new Vector3(0, 0, 90), .1f);
      //iTween.ScaleTo(temp, new Vector3(0.01f, 0.01f, 0.01f), .15f);
      //yield return new WaitForSeconds(.15f);

      //Destroy(temp);
    }
    yield return null;
  }

  public void splitSprite(){
    StartCoroutine(Split());
  }

  private IEnumerator ColorFade(Sprite to)
  {
    if (GameObject.Find("BoardManager") != null)
    {
      GameObject temp = Instantiate(gameObject, transform.position, transform.rotation, GameObject.Find("BoardManager").transform);
      temp.GetComponent<BoxCollider2D>().enabled = false;

      //gameObject.GetComponent<SpriteRenderer>().sprite = null;

      iTween.FadeTo(temp, 0.01f, .1f);
      yield return new WaitForSeconds(.1f);

      gameObject.GetComponent<SpriteRenderer>().sprite = to;

      iTween.FadeTo(temp, 1f, .1f);
      yield return new WaitForSeconds(.1f);

      Destroy(temp);
    }
    yield return null;
  }

  public void colorFadeSprite(Sprite to)
  {
    StartCoroutine(ColorFade(to));
  }
}