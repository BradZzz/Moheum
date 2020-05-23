using System;
using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using UnityEngine;

public interface IUiAtkActionButton : ISelectAtkActionButton, IResetAtkActionButtons
{
    Action<bool> OnToggle { get; set; }
}
