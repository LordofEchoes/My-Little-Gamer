using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Phase uses a Dictionary but should act like one too.
[System.Serializable]
public class Phase
{
    private Dictionary<string,Tactic> TacticTable;
    public int Count {get;}
    public string Name {get; set;}
    public string CurrentTactic{get; set;}


    // default constructor
    public Phase()
    {
        TacticTable.Add("Farm", new Tactic("Farm"));
        TacticTable.Add("Poke", new Tactic("Poke"));
        TacticTable.Add("Engage", new Tactic("Engage"));
        CurrentTactic = "Farm";
        Count = TacticTable.Count;
        Name = "No Name";
    }

    public Phase(string PhaseName)
    {
        TacticTable = new Dictionary<string, Tactic>();
        TacticTable.Add("Farm", new Tactic("Farm"));
        TacticTable.Add("Poke", new Tactic("Poke"));
        TacticTable.Add("Engage", new Tactic("Engage"));
        CurrentTactic = "Farm";
        Count = TacticTable.Count;
        Name = PhaseName;
    }

    public Phase(string PhaseName, int proficiency)
    {
        TacticTable = new Dictionary<string,Tactic>();
        TacticTable.Add("Farm", new Tactic("Farm", proficiency));
        TacticTable.Add("Poke", new Tactic("Poke", proficiency));
        TacticTable.Add("Engage", new Tactic("Engage", proficiency));
        CurrentTactic = "Farm";
        Count = TacticTable.Count;
        Name = PhaseName;
    }

    public Tactic this[string TacticName]
    {
        get{return TacticTable[TacticName];}
        set{TacticTable[TacticName] = value;}
    }

    public void ChangeTactic(string NewTactic)
    {
        CurrentTactic = NewTactic;
    }

    // change proficiency based on specific tactic
    public void ChangeTacticProficiency(string tacticName, int amount)
    {
        TacticTable[tacticName].ChangeProficiency(amount);
    }

    public int GetTacticPosition(Dictionary<string, Stat> StatTable)
    {
        System.Random Generator = new System.Random();
        int Roll = Generator.Next(1,101);
        // that player looks for position 1 = 70 + Positioning Modifier. 
        if(Roll + StatTable["Positioning"].Value >= 40)
        {
            switch(CurrentTactic)
            {
                case "Farm":
                return 1;
                case "Poke":
                return 2;
                case "Engage":
                return 3;
            }
        }
        else if (Roll + StatTable["Positioning"].Value > 20)
        {
            switch(CurrentTactic)
            {
                case "Farm":
                return 2;
                case "Poke":
                return 1;
                case "Engage":
                return 1;
            }
        }
        else
        {
            switch(CurrentTactic)
            {
                case "Farm":
                return 3;
                case "Poke":
                return 3;
                case "Engage":
                return 2;
            }
        }
        // something went wrong
        return 0;
    }
}
