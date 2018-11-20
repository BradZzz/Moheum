using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class PlayerTile : UnityEngine.Tilemaps.Tile {

  [SerializeField]
  private Sprite preview;

  public GameObject prefab;

  /// <summary>
  /// Refreshes this tile when something changes
  /// </summary>
  /// <param name="position">The tiles position in the grid</param>
  /// <param name="tilemap">A reference to the tilemap that this tile belongs to.</param>
  public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
  {
    if (!Application.isPlaying) {
      tileData.sprite = preview;
    } else {
      tileData.sprite = null;
    }

    if (prefab) { 
      tileData.gameObject = prefab;
    }
  }

  #if UNITY_EDITOR
  [MenuItem("Assets/Tiles/PlayerTile")]
  public static void createRoadTile(){
    string path = EditorUtility.SaveFilePanelInProject("Save Playertile", "New Playertile", "asset", "Save playertile", "Assets/Prefabs");
    if (path == ""){
      return;
    }
    AssetDatabase.CreateAsset (ScriptableObject.CreateInstance<PlayerTile> (), path);
  }
  #endif
}
