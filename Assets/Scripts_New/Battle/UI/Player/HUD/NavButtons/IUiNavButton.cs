﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public interface IUiNavButton
  {
    Image Image { get; }
    NavID NavID { get; }
  }
}