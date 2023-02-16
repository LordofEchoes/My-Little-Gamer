using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    System.DateTime DateEncounter;
    int WeekEncounter;

    public EnemyStats(System.DateTime date, int week = 1, string name = "", string gender = "Male", int NumberOfStats = 6, int StartingProficiency = -1) : base(name, gender, NumberOfStats, StartingProficiency)
    {
        DateEncounter = date;
        WeekEncounter = week;
        // double check week and return Debug Message if wrong
        if ((date - new System.DateTime(2023,8,4)).Days/7+1 != WeekEncounter)
        {
            Debug.Log($"EnemyStat has the wrong week for the date that was entered. System entered {week}. Calculated result should be {(date - new System.DateTime(2023,8,4)).Days/7+1}");
        }
    }

    // Date to Encounter Enemy, override
    public override System.DateTime GetDate()
    {
        return DateEncounter;
    }
}
