﻿using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.MoheModel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Data;
using Battle.UI.RuntimeBoard.Mechanics;
using UnityEngine;

namespace Battle.Model.RuntimeBoard
{
  public interface IRuntimeBoard
  {
    BoardData GetBoardData();
    List<BaseBoardMechanics> GetMechanics();

    Func<IRuntimeJewel,bool> OnInvokeActionEffect { get; set; }
    Action OnCleanAbility { get; set; }
  }
}
