using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat
{
    [SerializeField]
    // Value is the value of the stat
    public int Value{get; set;}
    // Experienceis the xp of the stat, 1000 Experiencewill level it up.
    public int Experience{get; set;}
    private int ExperienceLimit = 1000;


    // default constructor for Stat Class(default is 1)
    public Stat()
    {
        Value = 1;
    }
    // pass in one value constructor for Stat Class
    public Stat(int amount)
    {
        Value = amount ;
        Experience = 0;
    }
    // Two variable contructor for the Stat Class
    public Stat(int amount, int ExperienceAmount)
    {
        Value = amount;
        Experience = ExperienceAmount;
    }

    // Checke helper function
    private bool CheckExperience(int amount)
    {
        if (Experience + amount >= ExperienceLimit)
        {
            Experience = Experience + amount - ExperienceLimit;
            return true;
        }
        else
        {
            Experience = amount;
            return false;
        }
    }

    // change ExperienceFunction, automatically updates Stats;
    public void ChangeExperience(int amount)
    {
        if(CheckExperience(amount))
        {
            Value ++;
        }
    }


    // Operand functions for Stat
    public static Stat operator+(Stat statA, Stat statB)
    {
        return new Stat(statA.Value + statB.Value);
    }

    public static Stat operator+(Stat statA, int amount)
    {
        return new Stat(statA.Value + amount);
    }

    public static Stat operator-(Stat statA, Stat statB)
    {
        return new Stat(statA.Value - statB.Value);
    }

    public static Stat operator-(Stat statA, int amount)
    {
        return new Stat(statA.Value - amount);
    }
}
