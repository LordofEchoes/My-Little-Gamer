using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    // default constructor for Stat Class(default is 1)
    public Stat()
    {
        SetValue(1);
    }
    // pass in one value constructor for Stat Class
    public Stat(int amount)
    {
        SetValue(amount);
    }

    public int GetValue()
    {
        return baseValue;
    }

    private void SetValue(int amount)
    {
        baseValue = amount;
    }

    public static Stat operator+(Stat statA, Stat statB)
    {
        return new Stat(statA.GetValue() + statB.GetValue());
    }

    public static Stat operator+(Stat statA, int amount)
    {
        return new Stat(statA.GetValue() + amount);
    }

    public static Stat operator-(Stat statA, Stat statB)
    {
        return new Stat(statA.GetValue() - statB.GetValue());
    }

    public static Stat operator-(Stat statA, int amount)
    {
        return new Stat(statA.GetValue() - amount);
    }
}
