using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle
{
    public Dictionary<int,string> Cycles;
    public int CycleLimit{get;set;}
    public int CurrentCycle{get;set;}

    public DayCycle()
    {
        CycleLimit = 2;
        Cycles = new Dictionary<int,string>();
        Cycles.Add(1,"Morning");
        Cycles.Add(2,"Evening");
        CurrentCycle = 1;
    }

    public override string ToString()
    {
        return Cycles[CurrentCycle];
    }

    public bool CheckIncrement()
    {
        return CurrentCycle + 1 > CycleLimit ? true : false;
    }

    public void IncrementCycle()
    {
        if(CurrentCycle > CycleLimit)
        {
            CurrentCycle = 1;
        }
        else
        {
            CurrentCycle++;
        }
    }
}
