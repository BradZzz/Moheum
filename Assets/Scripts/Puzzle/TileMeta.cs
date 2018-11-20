using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class TileMeta {
  public GemType type;

  public enum GemType {
    Blue, Green, Silver, Purple, Red, Gold, Fight, None
  }
}
