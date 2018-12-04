using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardClickable : MonoBehaviour {

  public void Update()
  {
    if (Input.GetMouseButtonUp(0))
    {
      Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z)));

      Debug.Log("GetMouseButtonDown Hit");

      RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, Vector2.down);
      if (hits.Length > 0){
        foreach(RaycastHit2D hit in hits)
        {
          if (hit.collider != null)
          {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(hit.point.x, hit.point.y, Mathf.Abs(Camera.main.transform.position.z)));

            //Debug.Log("BoardClickable Name: " + hit.collider.gameObject.name + ":" + hit.point.ToString() + ":" + mouseWorldPos.ToString());
            //StartCoroutine(ClickAtPos(mouseWorldPos));

            //GameObject player = GameObject.FindWithTag("Player");
            //GameObject pause = GameObject.Find("PauseCanvas");
            //if (player != null && pause != null)
            //{
            //  if (!pause.GetComponent<PauseManager>().IsOpen())
            //  {
            //    Debug.Log("Moving Player To : " + mouseWorldPos.ToString());
            //    StartCoroutine(player.GetComponent<Move>().MoveToDest(mouseWorldPos));
            //  }
            //}
            //else
            //{
            //  Debug.Log("Error Moving");
            //}
          }
        }
      }
    }
  }

  //IEnumerator ClickAtPos(Vector3 pos)
  //{
  //  Debug.Log("Moving To: " + pos.ToString());
  //  GameObject clicky = Instantiate(PauseManager.instance.clickSprite, pos, Quaternion.identity);
  //  yield return new WaitForSeconds(.5f);
  //  Destroy(clicky);
  //}
}
