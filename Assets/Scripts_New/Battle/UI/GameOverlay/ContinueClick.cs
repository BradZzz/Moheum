using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueClick : MonoBehaviour
{
    public void Click()
    {
      Debug.Log("Continue Clicked!");
      SceneManager.LoadScene("BattleAfterScene");
    }
}
