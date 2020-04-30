using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.Model.Jewel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Data;
using Battle.UI.Jewel;
using Battle.UI.Player;
using UnityEngine;

namespace Battle.UI.Board
{
  public class UiBoard : UIGemPile, IUiBoard
  {
    protected override void Awake()
    {
      base.Awake();
      //Controller = GetComponent<IUiPlayer>();

      //Draw Jewels here

      //TargetResolver = transform.parent.GetComponentInChildren<ITargetResolver>();
      //PlayerTeams = transform.parent.parent.GetComponentsInChildren<IUiPlayerTeam>();
      //foreach (var team in PlayerTeams)
      //{
      //  team.OnPileChanged += (characters, capitain) => EnableCards();
      //  team.OnCharacterSelected += (charac) => { if (charac.IsUser) DisableCards(); };
      //}
      //TargetResolver.OnTargetsResolve += (card) => EnableCards();
      //jewels = new List<IUiJewel>();
    }

    //private List<IUiJewel> jewels;

    //public List<IUiJewel> Jewels => jewels;

    //public IBoardData GetBoardData()
    //{
    //  return GameData.Instance.RuntimeGame.GameBoard.GetBoardData();
    //}
  }
}
