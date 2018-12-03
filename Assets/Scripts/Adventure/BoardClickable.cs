using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardClickable : MonoBehaviour {

  public void Update()
  {
    if (Input.GetMouseButtonUp(0))
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      Debug.Log("GetMouseButtonDown Hit");

      RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.down);
      if (hit.collider != null)
      {
        //Debug.Log("BoardClickable Origin: " + ray.origin.ToString());
        Debug.Log("BoardClickable Hit: " + hit.point.ToString());

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(hit.point.x,
                                                                         hit.point.y, Mathf.Abs(Camera.main.transform.position.z)));

        //Debug.Log("Translation Hit: " + mouseWorldPos.ToString());

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

  //IEnumerator ClickAtPos(Vector3 pos)
  //{
  //  Debug.Log("Moving To: " + pos.ToString());
  //  GameObject clicky = Instantiate(PauseManager.instance.clickSprite, pos, Quaternion.identity);
  //  yield return new WaitForSeconds(.5f);
  //  Destroy(clicky);
  //}
}
