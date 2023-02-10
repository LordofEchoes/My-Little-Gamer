using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle
{
    public List<string> Cycles;
    private static int CycleLimit = 1;
    public int CurrentCycle{get;set;}

    public DayCycle()
    {
        Cycles = new List<string>();
        Cycles.Add("Morning");
        Cycles.Add("Evening");
        CurrentCycle = 0;
    }

    public override string ToString()
    {
        return Cycles[CurrentCycle];
    }

    public int GetCycle()
    {
        return CurrentCycle;
    }

    public bool CheckIncrement()
    {
        return CurrentCycle + 1 > CycleLimit;
    }

    public void IncrementCycle()
    {
        if(CurrentCycle+1 > CycleLimit)
        {
            CurrentCycle = 0;
        }
        else
        {
            CurrentCycle++;
        }
    }
}
