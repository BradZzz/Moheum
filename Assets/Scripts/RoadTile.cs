using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class RoadTile : UnityEngine.Tilemaps.Tile {
  
  [SerializeField]
  private Sprite[] roadSprites;

  [SerializeField]
  private Sprite preview;

  /// <summary>
  /// Refreshes this tile when something changes
  /// </summary>
  /// <param name="position">The tiles position in the grid</param>
  /// <param name="tilemap">A reference to the tilemap that this tile belongs to.</param>
  public override void RefreshTile(Vector3Int position, ITilemap tilemap)
  {
    for (int y = -1; y <= 1; y++) //Runs through all the tile's neighbours 
    {
      for (int x = -1; x <= 1; x++)
      {
        //We store the position of the neighbour 
        Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);

        if ( HasRoad(tilemap, nPos)) //If the neighbour has road on it
        {
          tilemap.RefreshTile(nPos); //Them we make sure to refresh the neighbour aswell
        }
      }
    }
  }

  public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
  {
    string composition = string.Empty;//Makes an empty string as compostion, we need this so that we change the sprite

    for (int x = -1; x <= 1; x++)//Runs through all neighbours 
    {
      for (int y = -1; y <= 1; y++)
      {
        if (x != 0 || y != 0) //Makes sure that we aren't checking our self
        {
          //If the value is a roadtile
          if (HasRoad(tilemap, new Vector3Int(location.x + x, location.y + y, location.z)))
          {
            composition += 'X'; 
          }
          else
          {
            composition += 'O';
          }
        }
      }
    }
    /*29*/
    Hashtable dirMap = new Hashtable();
    dirMap.Add ("XXXX",roadSprites [27]);
    dirMap.Add ("OXXX",roadSprites [5]);
    dirMap.Add ("XOXX",roadSprites [1]);
    dirMap.Add ("XXOX",roadSprites [6]);
    dirMap.Add ("XXXO",roadSprites [0]);
    dirMap.Add ("XOOX",roadSprites [8]);
    dirMap.Add ("OXOX",roadSprites [7]);
    dirMap.Add ("XOXO",roadSprites [3]);
    dirMap.Add ("OXXO",roadSprites [2]);
    dirMap.Add ("OOXX",roadSprites [4]);
    dirMap.Add ("XXOO",roadSprites [9]);
    dirMap.Add ("XOOO",roadSprites [25]);
    dirMap.Add ("OXOO",roadSprites [26]);
    dirMap.Add ("OOXO",roadSprites [21]);
    dirMap.Add ("OOOX",roadSprites [20]);
    dirMap.Add ("OOOO",roadSprites [28]);

    //    tileData.sprite = (Sprite)dirMap[composition[1]+composition[6]+composition[3]+composition[4]];

    //    tileData.sprite = roadSprites [0];

    string composition2 = string.Empty;
    composition2 += composition[1]; 
    composition2 += composition[6]; 
    composition2 += composition[3]; 
    composition2 += composition[4]; 

    Debug.Log (composition2);

    tileData.sprite = (Sprite)dirMap[composition2];

    //if (tileData.sprite == flowerSprites [4] && composition2.Equals("XXXX")) {
    //  if(composition[0] == 'O'){
    //    tileData.sprite = flowerSprites [12];
    //  }
    //  if(composition[2] == 'O'){
    //    tileData.sprite = flowerSprites [9];
    //  }
    //  if(composition[5] == 'O'){
    //    tileData.sprite = flowerSprites [11];
    //  }
    //  if(composition[7] == 'O'){
    //    tileData.sprite = flowerSprites [10];
    //  }
    //}
  }

  private bool HasRoad(ITilemap tilemap, Vector3Int position)
  {
    return tilemap.GetTile(position) == this;
  }

  #if UNITY_EDITOR
  [MenuItem("Assets/Tiles/RoadTile")]
  public static void createRoadTile(){
    string path = EditorUtility.SaveFilePanelInProject("Save Roadtile", "New Roadtile", "asset", "Save roadtile", "Assets/Prefabs");
    if (path == ""){
      return;
    }
    AssetDatabase.CreateAsset (ScriptableObject.CreateInstance<RoadTile> (), path);
  }
  #endif
}