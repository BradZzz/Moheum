using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using Battle.UI.Jewel.Component;
using UnityEngine;

namespace Battle.UI.Jewel.Listener
{
  public interface IUiJewelClickListener
  {
    void Execute(IRuntimeJewel data);
  }
}
