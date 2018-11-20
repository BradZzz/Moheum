using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DynamicSorting : MonoBehaviour
{
  void LateUpdate()
  {
    var renderer = GetComponent<Renderer>();
    renderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(renderer.bounds.min).y * -1;
  }

  //function Awake()
  //{
  //  tempRend = gameObject.GetComponent(SpriteRenderer);
  //}
  //function LateUpdate()
  //{
  //  tempRend.sortingOrder = Camera.main.WorldToScreenPoint(tempRend.bounds.min).y * -1;
  //}
}