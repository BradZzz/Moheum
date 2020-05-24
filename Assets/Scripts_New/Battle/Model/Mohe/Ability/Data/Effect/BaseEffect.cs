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
    [SerializeField] private PlayerSeat effectPlayer;

    public JewelID Jewel => jewel;

    public int MinAmt => minAmt;
    public int MaxAmt => maxAmt;
    public PlayerSeat EffectPlayer => effectPlayer;

    public abstract bool Execute(IRuntimeJewel TriggerJewel);
  }
}
