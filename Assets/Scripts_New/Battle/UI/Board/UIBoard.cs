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
  public class UIBoard : UIGemPile, IUiBoard
  {
    public Transform Transform => transform;

    protected override void Awake() => base.Awake();
  }
}
