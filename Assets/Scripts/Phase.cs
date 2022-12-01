using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Phase
{
    public Dictionary<string,Tactic> TacticTable;
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
    public void ChangeTactic(string NewTactic)
    {
        CurrentTactic = NewTactic;
    }
    // change proficiency based on specific tactic
    public void ChangeTacticProficiency(string tacticName, int amount)
    {
        TacticTable[tacticName].ChangeProficiency(amount);
    }
}
