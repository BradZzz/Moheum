using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public abstract class BaseEffect : ScriptableObject, IEffect
  {
    [SerializeField] private JewelID jewel;

    [SerializeField] private int minAmt;
    [SerializeField] private int maxAmt;
    [SerializeField] private PlayerEffectSeat effectPlayer;
    [SerializeField] private bool requiresPlayerClick;
    
    public JewelID Jewel => jewel;
    public int MinAmt => minAmt;
    public int MaxAmt => maxAmt;
    public PlayerEffectSeat EffectPlayer => effectPlayer;
    public bool RequiresPlayerClick => requiresPlayerClick;
    

    public abstract bool Execute(IRuntimeJewel TriggerJewel);
  }
}
