using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class Gold
{
    // value is traded as # of CS to reach one Value then  # of Gold for 1 Value
    public int gold;
    public int CSToNextGold;
    private int TotalCreepScore;
    private int TotalKills;
    // CS needed to trade in for gold
    private static int CSPerValue = 10;
    // gold for each time value is reached
    private static int GoldPerValue = 25;
    // gold for each kill
    private static int GoldPerKill = 100;

    public static implicit operator int(Gold g) => g.gold;
    public static implicit operator Gold(int i) => new Gold(i);

    public override string ToString()
    {
        return gold.ToString();
    }

    public int GetCreepScore()
    {
        return TotalCreepScore;
    }

    public int GetKills()
    {
        return TotalKills;
    }

    public Gold(int StartingGold = 0, int StartingCSToNextGold = 0, int StartingCreepScore = 0)
    {
        gold = StartingGold;
        TotalCreepScore = StartingCreepScore;
        AddCS(StartingCSToNextGold);
    }

    //Only adds creep score to the total wihtout changing gold
    public void AddCS(int CreepScore = 1)
    {
        CSToNextGold += CreepScore;
        CSToNextGold = CSToNextGold%CSPerValue;
        TotalCreepScore += CreepScore;
    }

    public void FarmToGold(int CreepScore = 1)
    {
        CSToNextGold += CreepScore;
        TotalCreepScore += CreepScore;
        int Value = (int)Math.Floor((float)CSToNextGold/CSPerValue);
        // Debug.Log("Gold's Value : " + Value.ToString());
        gold += (int)Math.Floor((float)Value*GoldPerValue);
        CSToNextGold = CSToNextGold%10;
        
    }

    public void KillToGold(int NumberOfKills = 1)
    {
        TotalKills += NumberOfKills;
        gold += GoldPerKill;
    }

    public void Reset()
    {
        gold = 0;
        CSToNextGold = 0;
    }
}
