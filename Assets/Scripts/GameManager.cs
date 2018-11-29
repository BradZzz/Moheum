/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public GameObject faderObj;
  public GameObject battleObj;
  public Image faderImg;
	public bool gameOver = false;

	public float fadeSpeed = .02f;

	private Color fadeTransparency = new Color(0, 0, 0, .04f);
	private string currentScene;
	private AsyncOperation async;
  private bool newScene = false;

	void Awake() {
		// Only 1 Game Manager can exist at a time
		//if (instance == null) {
		//	DontDestroyOnLoad(gameObject);
		//	instance = GetComponent<GameManager>();
		//	SceneManager.sceneLoaded += OnLevelFinishedLoading;
		//} else {
		//	Destroy(gameObject);
		//}

    instance = GetComponent<GameManager>();
    //GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<ParticleSystem>().Pause();
  }

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			ReturnToMenu();
		}
	}

	// Load a scene with a specified string name
	public void LoadScene(string sceneName) {
		instance.StartCoroutine(Load(sceneName));
    instance.StartCoroutine(FadeOut(instance.faderObj, instance.faderImg, Color.black));
	}

	// Reload the current scene
	public void ReloadScene() {
		LoadScene(SceneManager.GetActiveScene().name);
	}

	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		currentScene = scene.name;
		instance.StartCoroutine(FadeIn(instance.faderObj, instance.faderImg));
	}

  public void HealFlash(){
    instance.StartCoroutine(HealTransition(instance.faderObj, instance.faderImg, Color.white));
  }

  //Iterate the fader transparency to 100%
  IEnumerator HealTransition(GameObject faderObject, Image fader, Color fadeColor)
  {
    Debug.Log("HealTransition");

    GameObject glosary = PauseManager.instance.glossaryObj;
    Glossary glossy = glosary.GetComponent<Glossary>();
    GameObject magic = glossy.GetParticleSystem("HealingParticleSystem");

    GameObject projectile = Instantiate(magic, GameObject.FindWithTag("Player").transform.position, Quaternion.identity);
    projectile.transform.localScale = new Vector3(.2f,.2f,.2f);
    projectile.GetComponent<ParticleSystem>().Play();
    //if (effect != SkillEffect.Effect.Sabotage && effect != SkillEffect.Effect.None)
    //{
    //  projectile.GetComponent<ParticleSystem>().startColor = SkillEffect.ColorGem(gem);
    //}
    //iTween.MoveTo(projectile, playerToGem ? ePos : sPos, animationWait - .1f);
    yield return new WaitForSeconds(2f);
    projectile.GetComponent<ParticleSystem>().Pause();
    yield return new WaitForSeconds(.5f);
    Destroy(projectile);


    //GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<ParticleSystem>().Play();
    //yield return new WaitForSeconds(.6f);
    //GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<ParticleSystem>().Pause();

    //faderObject.SetActive(true);
    //faderImg.gameObject.SetActive(true);
    //faderImg.color = fadeColor;
    //while (fader.color.a < 1)
    //{
    //  fader.color += fadeTransparency;
    //  yield return new WaitForSeconds(fadeSpeed);
    //}
    //while (fader.color.a > 0)
    //{
    //  fader.color -= fadeTransparency;
    //  yield return new WaitForSeconds(fadeSpeed);
    //}
    //faderObject.SetActive(false);
  }

  public void FightFlash()
  {
    instance.StartCoroutine(FightTransition(battleObj));
  }

  //Iterate the fader transparency to 100%
  IEnumerator FightTransition(GameObject bObj)
  {
    Debug.Log("FightTransition");
    bObj.SetActive(true);

    iTween.ValueTo(bObj.transform.Find("Overlay1").gameObject, iTween.Hash(
      "from", bObj.transform.Find("Overlay1").GetComponent<RectTransform>().anchoredPosition,
      "to", Vector2.zero,
      "time", 1f,
      "onupdatetarget", this.gameObject,
      "onupdate", "moveOverlay1"));

    iTween.ValueTo(bObj.transform.Find("Overlay2").gameObject, iTween.Hash(
      "from", bObj.transform.Find("Overlay2").GetComponent<RectTransform>().anchoredPosition,
      "to", Vector2.zero,
      "time", 1f,
      "onupdatetarget", this.gameObject,
      "onupdate", "moveOverlay2"));

    yield return new WaitForSeconds(1f);

    iTween.ValueTo(bObj.transform.Find("Fight1").gameObject, iTween.Hash(
      "from", bObj.transform.Find("Fight1").GetComponent<RectTransform>().anchoredPosition,
      "to", Vector2.zero,
      "time", 1.5f,
      "onupdatetarget", this.gameObject,
      "onupdate", "moveBattle1"));

    iTween.ValueTo(bObj.transform.Find("Fight2").gameObject, iTween.Hash(
      "from", bObj.transform.Find("Fight2").GetComponent<RectTransform>().anchoredPosition,
      "to", Vector2.zero,
      "time", 1.5f,
      "onupdatetarget", this.gameObject,
      "onupdate", "moveBattle2"));

    yield return new WaitForSeconds(1.5f);

    Color faderC = Color.black;
    faderC.a = 0;
    Debug.Log("FightTransition");
    instance.faderObj.SetActive(true);
    faderImg.gameObject.SetActive(true);
    faderImg.color = faderC;
    while (instance.faderImg.color.a < 1)
    {
      instance.faderImg.color += fadeTransparency;
      yield return new WaitForSeconds(fadeSpeed * 5);
    }

    yield return null;
  }

  public void moveOverlay1(Vector2 position)
  {
    battleObj.transform.Find("Overlay1").GetComponent<RectTransform>().anchoredPosition = position;
  }

  public void moveOverlay2(Vector2 position)
  {
    battleObj.transform.Find("Overlay2").GetComponent<RectTransform>().anchoredPosition = position;
  }

  public void moveBattle1(Vector2 position)
  {
    battleObj.transform.Find("Fight1").GetComponent<RectTransform>().anchoredPosition = position;
  }

  public void moveBattle2(Vector2 position)
  {
    battleObj.transform.Find("Fight2").GetComponent<RectTransform>().anchoredPosition = position;
  }

  public void SceneFlash(bool newGame)
  {
    instance.StartCoroutine(SceneTransition(instance.faderObj, instance.faderImg, Color.black, newGame));
  }

  //Iterate the fader transparency to 100%
  IEnumerator SceneTransition(GameObject faderObject, Image fader, Color fadeColor, bool newGame)
  {
    fadeColor.a = 0;
    Debug.Log("FightTransition");
    faderObject.SetActive(true);
    faderImg.gameObject.SetActive(true);
    faderImg.color = fadeColor;
    while (fader.color.a < 1)
    {
      fader.color += fadeTransparency;
      yield return new WaitForSeconds(fadeSpeed * 5);
    }
    if (newScene)
    {
      BaseSaver.putSlideshow("Intro");
      LoadScene("ScrollerScene");
    }
    else
    {
      LoadScene("MainScene");
    }
  }


  //Iterate the fader transparency to 100%
  IEnumerator FadeOut(GameObject faderObject, Image fader, Color fadeColor) {
		faderObject.SetActive(true);
    faderImg.color = fadeColor;
		while (fader.color.a < 1) {
			fader.color += fadeTransparency;
			yield return new WaitForSeconds(fadeSpeed);
		}
		ActivateScene(); //Activate the scene when the fade ends
	}

	// Iterate the fader transparency to 0%
  IEnumerator FadeIn(GameObject faderObject, Image fader) {
		while (fader.color.a > 0) {
			fader.color -= fadeTransparency;
			yield return new WaitForSeconds(fadeSpeed);
		}
		faderObject.SetActive(false);
	}

	// Begin loading a scene with a specified string asynchronously
	IEnumerator Load(string sceneName) {
		async = SceneManager.LoadSceneAsync(sceneName);
		async.allowSceneActivation = false;
		yield return async;
		isReturning = false;
  }

	// Allows the scene to change once it is loaded
	public void ActivateScene() {
		async.allowSceneActivation = true;
	}

	// Get the current scene name
	public string CurrentSceneName {
		get{
			return currentScene;
		}
	}

	public void ExitGame() {
		// If we are running in a standalone build of the game
		#if UNITY_STANDALONE
			// Quit the application
			Application.Quit();
		#endif

		// If we are running in the editor
		#if UNITY_EDITOR
			// Stop playing the scene
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	private bool isReturning = false;
	public void ReturnToMenu() {
		if (isReturning) {
			return;
		}

    if (CurrentSceneName != "Menu") {
			StopAllCoroutines();
			LoadScene("Menu");
			isReturning = true;
    }
	}

  public void LoadTestChar(){
    if (BaseSaver.getAdventure() == null) { ResetAll(); }
    SceneFlash(newScene);
  }

  public void ResetAll(){
    Debug.Log("ResetAll");
    Glossary glossy = GameObject.Find("Glossary").GetComponent<Glossary>();
    PlayerPrefs.DeleteAll();

    BaseSaver.setMapName("ShallowGrove");
    AdventureMeta meta = new AdventureMeta();
    meta.roster = returnTestRoster(glossy);
    meta.vault = new PlayerRosterMeta[0];
    BaseSaver.putAdventure(meta);
    newScene = true;
  }

  public static PlayerRosterMeta[] returnTestRoster(Glossary glossy){
    PlayerRosterMeta monster1 = MonsterMeta.returnMonster(glossy.GetMonsterMain("Glaivesect").meta, 4);
    //PlayerRosterMeta monster2 = MonsterMeta.returnMonster(glossy.GetMonsterMain("Octam").meta, 4);
    return new PlayerRosterMeta[] { monster1 };
  }
}
