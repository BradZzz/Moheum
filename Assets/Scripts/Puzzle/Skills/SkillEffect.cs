using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect {
  public Effect effect;
  public int amount;

  public virtual string effectString(){
    return "Nothing";
  }

  static string abbreviationGem(TileMeta.GemType gem){
    switch(gem){
    case TileMeta.GemType.Blue:
        return "<color=#59C4F0>(B)</color>";
    case TileMeta.GemType.Fight:
        return "<color=#E1A2A2>(F)</color>";
    case TileMeta.GemType.Gold:
        return "<color=#FFD52C>(Y)</color>";
    case TileMeta.GemType.Green:
        return "<color=#9FCE31>(G)</color>";
    case TileMeta.GemType.Purple:
        return "<color=#845994>(P)</color>";
    case TileMeta.GemType.Red:
        return "<color=#F23737>(R)</color>";
    case TileMeta.GemType.Silver:
        return "<color=#CCCCCC>(S)</color>";
    }
    return "";
  }

  public static Color ColorGem(TileMeta.GemType gem)
  {
    switch (gem)
    {
      case TileMeta.GemType.Blue:
        return new Color(.29f,.75f,.94f);
      case TileMeta.GemType.Fight:
        return Color.magenta;
      case TileMeta.GemType.Gold:
        return new Color(.8f, 0, 1f);
      case TileMeta.GemType.Green:
        return new Color(.808f, .192f, 1f);
      case TileMeta.GemType.Purple:
        return new Color(.349f, .58f, 1f);
      case TileMeta.GemType.Red:
        return new Color(.216f, .216f, 1f);
      case TileMeta.GemType.Silver:
        return new Color(.816f, .816f, 1f);
    }
    return Color.white;
  }

  static string abbreviationEffect(Effect effect){
    switch(effect){
    case Effect.Change:
      return "Cng";
    case Effect.ChangeSome:
      return "Cng";
    case Effect.Damage:
      return "Dmg";
    case Effect.Destroy:
      return "Dest";
    case Effect.DestroySome:
      return "Dest";
    case Effect.Heal:
      return "Heal";
    case Effect.Poison:
      return "Pois";
    case Effect.Poke:
      return "Poke";
    case Effect.Reset:
      return "Reset";
    case Effect.Sabotage:
      return "Sabo";
    case Effect.Slice:
      return "Slice";
    case Effect.xTurn:
      return "xTurn";
    }
    return "";
  }

  //Changes gem from one color to another
  public class ChangeSkill : SkillEffect {
    public TileMeta.GemType from;
    public TileMeta.GemType to;
    public ChangeSkill(){
      effect = Effect.Change;
    }
    public override string effectString(){
      return abbreviationEffect(effect) + " " + abbreviationGem(from) + " to " + abbreviationGem(to);
    }
  }

  public class ChangeSomeSkill : SkillEffect {
    public TileMeta.GemType from;
    public TileMeta.GemType to;
    public int lower;
    public int upper;
    public ChangeSomeSkill(){
      effect = Effect.ChangeSome;
    }
    public override string effectString(){
      return abbreviationEffect(effect) + " " + lower.ToString()+ "-" + upper.ToString() + " " + abbreviationGem(from) + " to " + abbreviationGem(to);
    }
  }

  //Damages opposing monster
  public class DamageSkill : SkillEffect {
    public DamageSkill(){
      effect = Effect.Damage;
    }
    public override string effectString(){
      return abbreviationEffect(effect) + " " + amount.ToString();
    }
  }

  //Destroys all gems of a specific color
  public class DestroySkill : SkillEffect {
    public TileMeta.GemType toRemove;
    public DestroySkill(){
      effect = Effect.Destroy;
    }
    public override string effectString(){
      return abbreviationEffect(effect) + " " + abbreviationGem(toRemove);
    }
  }

  //Destroys some gems of the defined type
  public class DestroySomeSkill : SkillEffect {
    public TileMeta.GemType toRemove;
    public int from;
    public int to;
    public DestroySomeSkill(){
      effect = Effect.DestroySome;
    }
    public override string effectString(){
      return abbreviationEffect(effect) + " " + from.ToString()+ "-" + to.ToString() + " " + abbreviationGem(toRemove);
    }
  }

  //Heals monster
  public class HealSkill : SkillEffect {
    public HealSkill(){
      effect = Effect.Heal;
    }
    public override string effectString(){
      return abbreviationEffect(effect) + " " + amount.ToString();
    }
  }

  //Poisons opposing monster
  public class PoisonSkill : SkillEffect {
    public int turns;
    public PoisonSkill(){
      effect = Effect.Poison;
    }
    public override string effectString(){
      return abbreviationEffect(effect) + " " + amount.ToString() + " for " + turns.ToString() + "turns";
    }
  }

  //Poke tile of certain color to destroy in radius
  public class PokeSkill : SkillEffect
  {
    public TileMeta.GemType toRemove;
    public PokeSkill()
    {
      effect = Effect.Poke;
    }
    public override string effectString()
    {
      return abbreviationEffect(effect) + " " + abbreviationGem(toRemove) + " to " + abbreviationEffect(Effect.Destroy) + " in " + amount.ToString();
    }
  }

  //Resets the board
  public class ResetSkill : SkillEffect {
    public ResetSkill(){
      effect = Effect.Reset;
    }
    public override string effectString(){
      return abbreviationEffect(effect) + " board";
    }
  }

  //Poisons opposing monster
  public class SabotageSkill : SkillEffect
  {
    public SabotageSkill()
    {
      effect = Effect.Sabotage;
    }
    public override string effectString()
    {
      return abbreviationEffect(effect) + " for " + amount.ToString();
    }
  }

  //Poisons opposing monster
  public class SliceSkill : SkillEffect
  {
    public TileMeta.GemType toRemove;
    public SliceSkill()
    {
      effect = Effect.Slice;
    }
    public override string effectString()
    {
      return abbreviationEffect(effect) + " " + abbreviationGem(toRemove) + " in " + amount.ToString();
    }
  }

  //Take extra turn
  public class xTurnSkill : SkillEffect {
    public xTurnSkill(){
      effect = Effect.xTurn;
    }
    public override string effectString(){
      return abbreviationEffect(effect) + " +" + amount.ToString();
    }
  }
    
  public enum Effect {
    Change, ChangeSome, Damage, Destroy, DestroySome, Heal, Poke, Poison, Reset, Sabotage, Slice, xTurn, None
  }

  /*
   * Change, ChangeSome, Damage, Destroy, DestroySome, Heal, Poke, Poison, Reset, Slice, xTurn, None
   * 
   * Change(Done): Change all gems from one color to another
   * ChangeSome(Done): Change some gems from one color to another
   * Damage(Done): Damage enemy player
   * Destroy(Done): Destroy all gems of a certain color
   * DestroySome(Done): Destroy some gems of a certain color
   * Heal(Done): Heal monster
   * Poke(Done): Destroys next clicked gem in a radius
   * Poison: Poisons enemy monster for a certain number of turns
   * Reset(Done): Resets the board
   * Sabotage(Done): Resets enemy monster's skills
   * Slice: Destroys the next gem in a +
   * xTurn(Done): Take extra turn
   */
}
