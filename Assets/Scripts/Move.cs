using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Move : MonoBehaviour {

  Animator anim;

  //public GameObject camera;

  private bool canMove;
  private Vector3 offset;
  private bool isMoving;
  private Vector3 destination;
  private bool keyMv;

  public float EPSILON { get; private set; }

  // Use this for initialization
  void Start () {
    keyMv = false;
    canMove = true;
    anim = GetComponent<Animator> ();
    destination = GetComponent<Rigidbody2D>().position;
    //offset = camera.transform.position - transform.position;
    //Debug.Log("Offset 1: " + offset.ToString());
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    destination = Vector3.zero;
  }

  //public void recalcOffset(){
  //  //offset = camera.transform.position - transform.position;
  //  //camera.transform.position = transform.position;
  //  //Debug.Log("Offset 2: " + offset.ToString());
  //}

  public void disableMove(){
    canMove = false;
  }

  public void disableMoveTimed()
  {
    canMove = false;
    destination = Vector3.zero;
    StartCoroutine(WaitEnable());
  }

  IEnumerator WaitEnable(){
    yield return new WaitForSeconds(.25f);
    destination = Vector3.zero;
    enableMove();
  }

  public void enableMove()
  {
    canMove = true;
  }

  //// Update is called once per frame
  //void Update () {
  //   //camera.transform.position = transform.position;

  //}

  void FixedUpdate()
  {
    float speed = 3.6f;
    float x = 0;
    float y = 0;
    Vector3 pos = GetComponent<Rigidbody2D>().position;

    if (canMove && !DialogManager.instance.isShown() && !EventSystem.current.IsPointerOverGameObject())
    {

      if (Input.GetKey(KeyCode.LeftArrow))
      {
        if (!keyMv)
        {
          StartCoroutine(MovedByKey());
          anim.SetTrigger("MoveLeft");
        }
        x = -.1f;
        destination = Vector3.zero;
      } 
      if (Input.GetKey(KeyCode.RightArrow))
      {
        if (!keyMv)
        {
          StartCoroutine(MovedByKey());
          anim.SetTrigger("MoveRight");
        }
        x = .1f;
        destination = Vector3.zero;
      } 
      if (Input.GetKey(KeyCode.UpArrow))
      {
        if (!keyMv)
        {
          StartCoroutine(MovedByKey());
          anim.SetTrigger("MoveUp");
        }
        y = .1f;
        destination = Vector3.zero;
      } 
      if (Input.GetKey(KeyCode.DownArrow))
      {
        if (!keyMv)
        {
          StartCoroutine(MovedByKey());
          anim.SetTrigger("MoveDown");
        }
        y = -.1f;
        destination = Vector3.zero;
      }

      if (destination != Vector3.zero)
      {
        if (Math.Abs(Math.Round(destination.x, 1) - Math.Round(GetComponent<Rigidbody2D>().position.x, 1)) > EPSILON)
        {
          //Debug.Log("X different: " + destination.x.ToString() + ":" + GetComponent<Rigidbody2D>().position.x.ToString());
          x = GetComponent<Rigidbody2D>().position.x < destination.x ? .1f : -.1f;
          if (!keyMv)
          {
            StartCoroutine(MovedByKey());
            if (x < 0)
            {
              anim.SetTrigger("MoveLeft");
            }
            else
            {
              anim.SetTrigger("MoveRight");
            }
          }
        }
        if (Math.Abs(Math.Round(destination.y, 1) - Math.Round(GetComponent<Rigidbody2D>().position.y, 1)) > EPSILON)
        {
          //Debug.Log("Y different: " + destination.y.ToString() + ":" + GetComponent<Rigidbody2D>().position.y.ToString());
          y = GetComponent<Rigidbody2D>().position.y < destination.y ? .1f : -.1f;
          if (!keyMv)
          {
            StartCoroutine(MovedByKey());
            if (y < 0)
            {
              anim.SetTrigger("MoveDown");
            }
            else
            {
              anim.SetTrigger("MoveUp");
            }
          }
        }
      }

      isMoving = Math.Round(x,2) != 0 || Math.Round(y, 2) != 0 ? true : false;

      //Vector3 pos = GetComponent<Rigidbody2D>().position;

      //pos += y * transform.up * Time.deltaTime * speed;
      //pos += x * transform.right * Time.deltaTime * speed;

      //GetComponent<Rigidbody2D>().position = pos;
    }
    //if (System.Math.Abs(destination.x - GetComponent<Rigidbody2D>().position.x) > EPSILON 
    //    || System.Math.Abs(destination.y - GetComponent<Rigidbody2D>().position.y) > EPSILON) {
    //  x = GetComponent<Rigidbody2D>().position.x < destination.x ? .1f : -.1f;
    //  y = GetComponent<Rigidbody2D>().position.y < destination.y ? .1f : -.1f;
    //}

    //Vector3 pos = GetComponent<Rigidbody2D>().position;

    if(isMoving){
      //Debug.Log("Pos: " + GetComponent<Rigidbody2D>().position.ToString());
      //Debug.Log("Dest: " + destination.ToString());
      //Debug.Log("x: " + x.ToString() + " y: " + y.ToString());

      pos += y * transform.up * Time.deltaTime * speed;
      pos += x * transform.right * Time.deltaTime * speed;

      GetComponent<Rigidbody2D>().position = pos;
      //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    } else {
      destination = Vector3.zero;
    }
  }

  public IEnumerator MoveToDest(Vector3 dest)
  {
    destination = dest;
    yield return null;
  }

  public IEnumerator MovedByKey()
  {
    keyMv = true;
    yield return new WaitForSeconds(.025f);
    keyMv = false;
  }

  public void TweenedSomeValue(GameObject obj)
  {
    Debug.Log(obj.transform.position.ToString());
    if (!isMoving) {
      iTween.Stop();
    }
  }

  public void OnFullProgress()
  {
    //Debug.Log ("OnFullProgress: Done");
    //PanelManager.instance.playerActed();
    isMoving = false;
  }

  public bool Moving(){
    return isMoving;
  }
}
