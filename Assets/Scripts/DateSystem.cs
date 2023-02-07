using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DateSystem
{
    System.DateTime DT;
    DayCycle DayCycle;
    static Dictionary<int, string> MonthName;

    //constructor default
    public DateSystem()
    {
        DT = new System.DateTime(2023,2,4);
        DayCycle = new DayCycle();
        MonthName = new Dictionary<int, string>();
        MonthName.Add(1,"Jan");
        MonthName.Add(2,"Feb");
        MonthName.Add(3,"Mar");
        MonthName.Add(4,"Apr");
        MonthName.Add(5,"May");
        MonthName.Add(6,"Jun");
        MonthName.Add(7,"Jul");
        MonthName.Add(8,"Aug");
        MonthName.Add(9,"Sep");
        MonthName.Add(10,"Oct");
        MonthName.Add(11,"Nov");
        MonthName.Add(12,"Dec");
    }

    public int Year{
        get {return DT.Year;}
    }
    public int Month{
        get {return DT.Month;}
    }
    public int Day{
        get {return DT.Day;}
    }

    public string DateAsString()
    {
        string days = DT.ToString("dd");
        string months =  MonthName[System.Convert.ToInt32(DT.ToString("MM"))];
        string years = DT.ToString("yyyy");
        return months + " " + days + ", " + years;
    }
    public string MonthYearAsString()
    {
        string months =  MonthName[System.Convert.ToInt32(DT.ToString("MM"))];
        string years = DT.ToString("yyyy");
        return months + " " + years;
    }
    public string CycleAsString()
    {
        return DayCycle.ToString();
    }

    public int GetCycle()
    {
        return DayCycle.GetCycle();
    }

    public void ProgressDay()
    {
        if(DayCycle.CheckIncrement())
        {
            DT = DT.AddDays(1);
        }
        DayCycle.IncrementCycle();
    }   

    public void ProgressWeek()
    {
        if (CheckWeek())
        {
            DT = DT.AddDays(6);
        }
    }

    public int GetDayOfWeek()
    {
        return (int)DT.DayOfWeek;
    }

    public bool CheckWeek()
    {
        Debug.Log($"DayOfTheWeek: {(int)DT.DayOfWeek}");
        return (int)DT.DayOfWeek == 0 && DayCycle.CheckIncrement()? true : false;
    }

    public bool CheckNextDay()
    {
        return DayCycle.CheckIncrement() ? true : false;

    }

}
