using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardListener : MonoBehaviour {
  public void Update()
  {
    //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
    //Debug.Log(coordinate);

    if (Input.GetMouseButtonDown(0))
    {
      //Debug.Log("Pressed primary button.");
      //Debug.Log(Input.mousePosition.ToString());
    }

    if (Input.GetMouseButtonUp(0))
    {
      //Debug.Log("Mouse up: " + Camera.main.nearClipPlane.ToString());
      //Debug.Log("<== 1 ==>: " + Input.mousePosition.ToString());
      ////float z = Camera.main.transform.position.y - transform.position.y;
      //Debug.Log("camera transform: " + Camera.main.transform.position.ToString());
      //Debug.Log("board transform: " + transform.position.ToString());
      //    Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
      //Input.mousePosition.y, Camera.main.nearClipPlane)));

      Debug.Log("Camera Pos: " + Camera.main.transform.position.ToString());

      Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                         Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z)));

      StartCoroutine(ClickAtPos(mouseWorldPos));
      //GameObject clicky = Instantiate(PauseManager.instance.clickSprite, mouseWorldPos, Quaternion.identity, BoardManager.instance.transform);
      //Debug.Log("<== 2 ==>: " + mouseWorldPos.ToString());
      //Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
      //Debug.Log(coordinate);

      GameObject player = GameObject.FindWithTag("Player");
      GameObject pause = GameObject.Find("PauseCanvas");
      if (player != null && pause != null)
      {
        if (!pause.GetComponent<PauseManager>().IsOpen())
        {
          Debug.Log("Moving Player");
          StartCoroutine(player.GetComponent<Move>().MoveToDest(mouseWorldPos));
        }
      } else {
        Debug.Log("Error Moving");
      }
    }
  }

  IEnumerator ClickAtPos(Vector3 pos){
    Debug.Log("Setting Target At: " + pos.ToString());
    GameObject clicky = Instantiate(PauseManager.instance.clickSprite, pos, Quaternion.identity);
    yield return new WaitForSeconds(.5f);
    Destroy(clicky);
  }
}
