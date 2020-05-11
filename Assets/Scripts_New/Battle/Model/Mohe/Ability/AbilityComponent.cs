using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class AbilityComponent : IAbilityComponent
  {
    public AbilityComponent(JewelID JewelType, int Needs)
    {
      jewelType = JewelType;
      needs = Needs;
    }

    public JewelID JewelType => jewelType;
    public int Needs => needs;

    private JewelID jewelType;
    private int needs;
  }
}
