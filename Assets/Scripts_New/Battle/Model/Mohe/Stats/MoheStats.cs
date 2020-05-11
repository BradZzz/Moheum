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

    private int envy;
    private int wrath;
    private int greed;
    private int gluttony;
    private int pride;
    private int lust;
    private int sloth;

    private int health;
  }
}
