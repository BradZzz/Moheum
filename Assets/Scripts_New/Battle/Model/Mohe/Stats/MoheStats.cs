using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class MoheStats : IMoheStats
  {
    public MoheStats(int Health, int Envy, int Wrath, int Greed, int Gluttony, int Pride, int Lust, int Sloth)
    {
      health = Health;
      envy = Envy;
      wrath = Wrath;
      greed = Greed;
      gluttony = Gluttony;
      pride = Pride;
      lust = Lust;
      sloth = Sloth;
    }

    public int Envy => envy;
    public int Wrath => wrath;
    public int Greed => greed;
    public int Gluttony => gluttony;
    public int Pride => pride;
    public int Lust => lust;
    public int Sloth => sloth;

    public int Health => health;

    [SerializeField]
    private int envy;
    [SerializeField]
    private int wrath;
    [SerializeField]
    private int greed;
    [SerializeField]
    private int gluttony;
    [SerializeField]
    private int pride;
    [SerializeField]
    private int lust;
    [SerializeField]
    private int sloth;
    [SerializeField]
    private int health;
  }
}
