﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterActionController : MonoBehaviour {

  private GameObject txt;
  private GameObject buffWrap;
  private GameObject buffTxt;
  bool isMoving = false;

  public int buff;

  private void Awake()
  {
    foreach (Transform child in transform)
    {
      if (child.name.Equals("DmgText"))
      {
        txt = child.gameObject;
      }
      if (child.name.Equals("MonBuff"))
      {
        buffWrap = child.gameObject;
      }
    }
    buffTxt = buffWrap.transform.GetChild(0).gameObject;
    txt.SetActive(false);
    buffWrap.SetActive(false);
    buff = 0;
  }

  public void SetBuff(int buff){
    Debug.Log("SetBuff: " + buff.ToString());
    buffWrap.SetActive(true);
    iTween.ValueTo(buffWrap, iTween.Hash(
      "from", 0,
      "to", 1,
      "time", .5f,
      "onupdatetarget", gameObject,
      "onupdate", "FadeOverlayWrap"));
    this.buff = buff;
    buffTxt.GetComponent<Text>().text = this.buff.ToString();
  }

  public void RemoveBuff(){
    StartCoroutine(BreakBuff());
  }

  private IEnumerator BreakBuff(){
    if (buff > 0)
    {
      //iTween.ShakePosition(buffWrap, new Vector3(1, 0, 0), .5f);
      //yield return new WaitForSeconds(.5f);
      iTween.ValueTo(buffWrap, iTween.Hash(
        "from", 1,
        "to", 0,
        "time", .5f,
        "onupdatetarget", gameObject,
        "onupdate", "FadeOverlayWrap"));
      yield return new WaitForSeconds(.5f);

    }
    buff = 0;
    buffWrap.SetActive(false);
  }

  public void FadeOverlayWrap(float alpha)
  {
    Color fadeCol = buffWrap.GetComponent<Image>().color;
    fadeCol.a = alpha;
    buffWrap.GetComponent<Image>().color = fadeCol;
  }

  // Update is called once per frame
  public void CharacterHit (bool mRight) {
    StartCoroutine(isHit(mRight));
	}

  private IEnumerator isHit(bool mRight)
  {
    if (GameObject.Find("BoardManager") != null && !isMoving)
    {
      isMoving = true;
      Vector3 pos = transform.position;
      //Vector3 mPos = new Vector3(pos.x + (mRight ? 1 : -1), pos.y, pos.z);
      //iTween.MoveTo(gameObject, mPos, .4f);
      yield return new WaitForSeconds(.5f);
      iTween.ShakePosition(gameObject, new Vector3(1, 0, 0), .6f);
      //yield return new WaitForSeconds(.6f);
      //iTween.MoveTo(gameObject, pos, .8f);
      //yield return new WaitForSeconds(.8f);
      isMoving = false;
    }
    yield return null;
  }

  public void CharacterIsHitting(bool mRight)
  {
    StartCoroutine(isHittingCharacter(mRight));
  }

  private IEnumerator isHittingCharacter(bool mRight)
  {
    if (GameObject.Find("BoardManager") != null && !isMoving)
    {
      isMoving = true;
      Vector3 pos = transform.position;
      yield return new WaitForSeconds(.4f);
      Vector3 mPos = new Vector3(pos.x + (mRight ? .75f : -.75f), pos.y, pos.z);
      iTween.MoveTo(gameObject, mPos, .1f);
      yield return new WaitForSeconds(.1f);
      iTween.MoveTo(gameObject, pos, .8f);
      isMoving = false;
    }
    yield return null;
  }

  private GameObject getTxt(){
    foreach (Transform child in transform)
    {
      if (child.name.Equals("DmgText"))
      {
        return child.gameObject;
      }
    }
    return null;
  }

  public void showDamage(int dmg){
    StartCoroutine(floatDmg(dmg));
  }

  private IEnumerator floatDmg(int dmg)
  {
    /*
     * If damage over zero, play heart particles
     */
    if (dmg != 0)
    {
      Debug.Log("floatDmg");
      Color txtColor = dmg > 0 ? Color.red : Color.green;

      if (txtColor == Color.green) {
        BoardManager.instance.emitHearts();
      } else {
        BoardManager.instance.emitExplosion();
      }

      yield return new WaitForSeconds(.5f);

      Vector3 pos = txt.transform.position;
      GameObject temp = Instantiate(txt, pos, txt.transform.rotation, transform);

      temp.GetComponent<Text>().color = txtColor;
      temp.GetComponent<Text>().text = System.Math.Abs(dmg).ToString();

      yield return new WaitForSeconds(.5f);
      temp.SetActive(true);
      iTween.MoveTo(temp, new Vector3(pos.x + Random.Range(-1, 1), pos.y + 1.2f, pos.z), 1.8f);
      yield return new WaitForSeconds(1f);
      iTween.FadeTo(temp, .05f, .6f);
      yield return new WaitForSeconds(.6f);

      Destroy(temp);
    }
  }
}
