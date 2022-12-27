using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterStats:MonoBehaviour
{
    public string Name{get;set;}
    public int MaxHealth = 100;
    public int CurrentHealth {get; set;}
    public int CreepScore {get;set;}
    public int Gold {get;set;}
    public int MaxStat = 20;
    public int StatNum = 6;
    public string Gender{get; set;}
    public int StatSum{get; set;}
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

    // Determine Distance used for combat
    public int Distance(int EnemyPosition, int CurrentPosition = 0, string CurrentPhase = "Early Game")
    {
        if(CurrentPosition == 0)
        {
            CurrentPosition = GetCharacterPosition(CurrentPhase);
        }
        int distance = Math.Abs(Math.Abs(EnemyPosition-5) - CurrentPosition);
        return distance;
    }

    // Used for combat, this is where the player is positioned.
    // Value is based off of the current player's tactic preference, positioning, aggression
    // return values:
    // 1: safest
    // 2: medium, in range to trade
    // 3: aggressive(on or past the minion wave)
    public int GetCharacterPosition(string CurrentPhase = "Early Game")
    {
        string Tactic = PhaseTable[CurrentPhase].CurrentTactic;
        int TacticProficiency = PhaseTable[CurrentPhase].TacticTable[Tactic].Proficiency;
        System.Random Generator = new System.Random();
        int TacticRoll = Generator.Next(1,100);
        // calculate what the value of the roll is based off of player stats.
        int PlayerPosition = PhaseTable[CurrentPhase].GetTacticPosition(TacticRoll, StatTable);
        return PlayerPosition;
    }

    public int GetCharacterDecision(int EnemyPosition, int CurrentPosition = 0, string CurrentPhase = "Early Game")
    {
        if (CurrentPosition == 0)
        {
            CurrentPosition = GetCharacterPosition(CurrentPhase);
        }
        // How does character make decision based of position
        System.Random Generator = new System.Random();
        int DecisionRoll = Generator.Next(1,100);
        int distance = Distance(EnemyPosition, CurrentPosition, CurrentPhase);
        if(DecisionRoll + StatTable["Decisions"].Value <= 70)
        {
            switch(distance)
            {
                case 1: return 1;
                case 2: return 2;
                case 3: return 3;
                default: return 1;
            }
            // 1 is Farm
            // 2 is Poke
            // 3 is Engage
        }
        // randomly generate a decision.
        else
        {
            return Generator.Next(1,3);
        }
    }

    // player should max at 10 CS per turn
    public int Farm(int AttackedDamage, int CurrentPosition = 0, string CurrentPhase = "Early Game")
    {
        System.Random Generator = new System.Random();
        int FarmPenalty = 0;
        int FarmScore = 0;
        // if character is attacked, reduce farm by 0->50%
        if(AttackedDamage > 0)
        {
            FarmPenalty = Generator.Next(1,50)+(int)(AttackedDamage/5);
        }

        for(int i = 0; i < Generator.Next(1,10);i++)
        {
            if(Generator.Next(1,100) + StatTable["Mechanics"].Value <= 70)
            {
                FarmScore++;
            }
        }
        return (int)(FarmScore*(1-FarmPenalty));
    }

    // player deals poking damage
    public int Poke(int EnemyPosition, int CurrentPosition = 0, string CurrentPhase = "Early Game")
    {
        System.Random Generator = new System.Random();
        int DistancePenalty = Distance(EnemyPosition, CurrentPosition, CurrentPhase) > 2 ? 1 : 0;
        int PokeDamage = 0;
        // damage is half of full damage from an engage
        for(int i = 0; i < (int)(Generator.Next(Gold +1, Gold+10)*.5); i++)
        {
            if(Generator.Next(1,100) + StatTable["Mechanics"].Value <= 70)
            {
                PokeDamage++;
            }
        }
        // If Penalty is 1, 1-1/2 = .5, if penalty is 0, 1-0/2 = 1, full damage.
        return (int)(PokeDamage*(1-DistancePenalty/2));
    }

    // player deals Engage Damage
    public int Engage(int EnemyPosition, int CurrentPosition = 0, string CurrentPhase = "Early Game")
    {
        System.Random Generator = new System.Random();
        int DistancePenalty = Distance(EnemyPosition, CurrentPosition, CurrentPhase) > 1 ? 1 : 0;
        int EngageDamage = 0;
        if(CurrentPosition == 0)
        {
            CurrentPosition = GetCharacterPosition(CurrentPhase);
        }
        // distance of 1 = standard, distance of 2+ = 50% reduction
        for(int i = 0; i < (int)(Generator.Next(Gold/10 +1, Gold/10+10)); i++)
        {
            if(Generator.Next(1,100) + StatTable["Mechanics"].Value <= 70)
            {
                EngageDamage++;
            }
        }
        // If Penalty is 1, 1-1/2 = .5, if penalty is 0, 1-0/2 = 1, full damage.
        return (int)(EngageDamage*(1-DistancePenalty/2));
    }
    // Used for combat, occurs when enemy character Engage
    // Enemy Position for damage numbers, Defending an all.
    public int EngageDefence(int EnemyPosition, int CurrentPosition = 0, string CurrentPhase = "Early Game")
    {
        System.Random Generator = new System.Random();
        int DecisionRoll = Generator.Next(1,100);
        int DamageRoll;
        // Defending against an Engage.
        // damage determined by decision
        // firstly decision to trade(50%), all in back(100% damage), or farm(20% damage).
        // decision is based off of distance between players, farm is distance of 3, trade is distance 2, Engage back is distance 1+.
        // distance determined by position 1,1 = distance 3, etc.
        int distance = Distance(EnemyPosition, CurrentPosition, CurrentPhase);
        if(DecisionRoll + StatTable["Decisions"].Value <= 70)
        {
            int DamageTotal = 0;
            switch(distance)
            {
                case 1:
                // distance is 1, all in back
                DamageRoll = (int)(Generator.Next((int)(Gold/10)+1, (int)(Gold/10)+10));
                for(int i = 0; i < DamageRoll; i++)
                {   
                    if(Generator.Next(1,100) + StatTable["Mechanics"].Value <= 70)
                    {
                        DamageTotal++;
                    }
                }
                return DamageTotal;
                case 2:
                DamageRoll = (int)(Generator.Next((int)Gold/10 +1, (int)Gold/10+10)*.5);
                for(int i = 0; i < DamageRoll; i++)
                {   
                    if(Generator.Next(1,100) + StatTable["Mechanics"].Value <= 70)
                    {
                        DamageTotal++;
                    }
                }
                return DamageTotal;
                case 3:
                DamageRoll = (int)(Generator.Next((int)Gold/10+1, (int)Gold/10+10)*.2);
                for(int i = 0; i < DamageRoll; i++)
                {   
                    if(Generator.Next(1,100) + StatTable["Mechanics"].Value <= 70)
                    {
                        DamageTotal++;
                    }
                }
                return DamageTotal;
                default:
                return 0;
            }
        }
        // didn't choose "correct" decision, default to standard decision
        else
        {
            return 0;
        }
    }


}
