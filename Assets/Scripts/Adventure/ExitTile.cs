using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTile : MonoBehaviour {

  public string toScene;

  public void loadSteppedScene(){
    BaseSaver.setMapName(toScene.Split('.')[0]);
    BaseSaver.setMapConnection(toScene.Split('.')[1]);
    GameManager.instance.ReloadScene();
    //GameManager.instance.LoadScene("MainScene");
  }
}
