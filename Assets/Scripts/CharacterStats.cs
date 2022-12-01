using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterStats:MonoBehaviour
{
    public string Name{get;set;}
    public int MaxHealth = 100;
    public int CurrentHealth {get;private set;}
    public int MaxStat = 20;
    public int StatNum = 6;
    public string Gender{get; set;}
    public int StatSum{get; private set;}
    public Dictionary<string, Stat> StatTable;
    public Dictionary<string, Phase> PhaseTable;
    private int PhaseNum = 3;
    // amount to change a stat by
    // used to call ModStat since only one variable is passed into ModStat which is the index
    public int Amount{get;set;}
    
    // Stat order in respect to index
    // 0 happiness
    // 1 aggression
    // 2 focus
    // 3 decisions
    // 4 positioning
    // 5 mechanics

    public int GetPhaseNum()
    {
        return PhaseNum;
    }
    
    // Initializer used when the object awakens in Unity.
    public void Awake()
    {
        CurrentHealth = MaxHealth;
        Name = "";
        StatTable = new Dictionary<string, Stat>();
        StatTable.Add("Happiness", new Stat());
        StatTable.Add("Aggression", new Stat());
        StatTable.Add("Focus", new Stat());
        StatTable.Add("Decisions", new Stat());
        StatTable.Add("Positioning", new Stat());
        StatTable.Add("Mechanics", new Stat());
        StatSum = 6;
        PhaseTable = new Dictionary<string, Phase>();
        PhaseTable.Add("Early Game", new Phase("Early Game",50));
        PhaseTable.Add("Mid Game", new Phase("Mid Game"));
        PhaseTable.Add("Late Game", new Phase("Late Game"));
        ChangeTacticProficiency("Mid Game","Poke", 100);
        PhaseTable["Mid Game"].ChangeTactic("Poke");
    }
    
    public void ChangeTacticProficiency(string PhaseName, string TacticName, int Proficiency)
    {
        PhaseTable[PhaseName].ChangeTacticProficiency(TacticName, Proficiency);
    }

    // Assigns the Gender to the class
    public void AssignGender(string GenderArgument)
    {
        Gender = GenderArgument;
    }
    // decreases current health by the damage parameter
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    // changes the stat corresponding to the index parameter within the StatTable by the amount parameter 
    // adjusts itself for max and min values(MaxStat and 0)
    public void ModStat(string StatName, int amount)
    {
        StatSum += amount;
        StatTable[StatName] += amount;
        if (StatTable[StatName].Value <= 0)
        {
            StatSum += Math.Abs(StatTable[StatName].Value)+1;
            StatTable[StatName] += Math.Abs(StatTable[StatName].Value) + 1;
            Debug.Log(transform.name + " cannot have less than 0 stat");
        }
        else if (StatTable[StatName].Value > MaxStat)
        {
            StatSum -= StatTable[StatName].Value - MaxStat;
            StatTable[StatName] -= StatTable[StatName].Value - MaxStat;
            Debug.Log(transform.name + " cannot have stats more than " + MaxStat);
        }
    }

    public void TrainStat(string StatName, int Experience)
    {
        StatTable[StatName].ChangeExperience(Experience);
    }

    public void TrainTactic(int Experience)
    {
        ChangeTacticProficiency("Early Game", PhaseTable["Early Game"].CurrentTactic, Experience);
        ChangeTacticProficiency("Mid Game", PhaseTable["Mid Game"].CurrentTactic, Experience);
        ChangeTacticProficiency("Late Game", PhaseTable["Late Game"].CurrentTactic, Experience);
    }

    public virtual void Die()
    {
        //Die in some way, overwritten
        Debug.Log(gameObject.name + " died.");
    }


}
