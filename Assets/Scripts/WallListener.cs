//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WallListener : MonoBehaviour {

//  void Start()
//  {
//      Debug.Log(name);
//  }

//  void OnTriggerEnter2D (Collider2D other)
//  {
//      ////Debug.Log("OnTriggerEnter2D");
//      //if (name.Equals("HeroKnight") && other.name.Equals("Plants") ) {
//      //  Debug.Log("Entering : " + other.name);
//      //}
//    Debug.Log ("name: " + other.name);
//    Debug.Log("hit: " + name);

//    if (other.tag.Equals("Solid"))
//    {
//      Vector3 newPos = transform.position;
//      newPos = other.bounds.ClosestPoint(newPos);

//      Vector3 collisionDir = transform.position - newPos;
//      collisionDir.Normalize();
//      collisionDir *= 1.1f;
//      collisionDir.x *= other.bounds.extents.x;
//      collisionDir.y *= other.bounds.extents.y;
//      collisionDir.z *= other.bounds.extents.z;

//      transform.position = newPos + collisionDir;
//    }
//  }

//  void OnTriggerExit2D(Collider2D other)
//  {
//    if (name.Equals("HeroKnight") && other.name.Equals("Plants"))
//    {
//      Debug.Log("Leaving : " + other.name);
//    }
//      //Debug.Log ("OnTriggerExit2D");
//      //Debug.Log ("name: " + other.name);
//  }

//  void OnTriggerStay2D(Collider2D other)
//  {
//    if (name.Equals("HeroKnight") && other.name.Contains("Plant"))
//    {
//      Debug.Log("In : " + other.name);
//      int roll = Random.Range(0,1000);
//      if (roll > 996 && GetComponent<Move>().Moving()) {
//        Debug.Log("Hit");
//        //Initiate.Fade("BejeweledScene", Color.black, 1.0f);
//      }
//    }
//      //Debug.Log("OnTriggerStay2D");
//      //Debug.Log("name: " + other.name);
//  }
//}
