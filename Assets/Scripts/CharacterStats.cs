using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CharacterStats
{
    [SerializeField]public string Name{get;set;}
    public static int MaxHealth = 100;
    public int CurrentHealth {get; set;}
    public int Kills {get;set;}
    public int Deaths {get;set;}
    public Gold Gold {get;set;}
    public int MaxStat = 20;
    // GenderArgument should be "Male" or "Female"
    [SerializeField]public string Gender{get; set;}
    public int StatSum{get; set;}
    [SerializeField]public Dictionary<string, Stat> StatTable;
    public List<Phase> PhaseTable;
    public List<int> TowerTable;
    public int CurrentTower;
    private int MaxTowerHealth = 100;
    private int PhaseNum = 3;
    // amount to change a stat by
    // used to call ModStat since only one variable is passed into ModStat which is the index
    public int Amount{get;set;}

    private static int BaseDamageMin = 50;
    private static int BaseDamageMax = 101;
    
    // Stat order in respect to number(enum)
    // 0 happiness
    // 1 aggression
    // 2 focus
    // 3 decisions
    // 4 positioning
    // 5 mechanics

    public enum StatName
    {
        Happiness,
        Aggression,
        Focus,
        Decisions,
        Positioning,
        Mechanics
    }
    public enum TacticName
    {
        Farm,
        Poke,
        Engage,
    }
    
    
    
    // constructor, default
    public CharacterStats()
    {
        Name = "";
        Initialize();
    }
    
    // constructor, BuildNew Instructions
    public CharacterStats(string name = "", string gender = "Male", int NumberOfStats = 6, int StartingProficiency = -1)
    {
        Initialize();
        BuildNew(name, gender, NumberOfStats, StartingProficiency);
    }

    //Puts AssignGender, RandomStats, RandomTactic together 
    public void BuildNew(string name = "", string gender = "Male", int NumberOfStats = 6, int StartingProficiency = -1)
    {
        Name = name;
        Gender = gender;
        RandomStats(NumberOfStats);
        RandomTactic(StartingProficiency);
    }

    // initialize all the tables for CharacterStats
    private void Initialize()
    {
        CurrentHealth = MaxHealth;
        StatTable = new Dictionary<string, Stat>();
        StatTable.Add("Happiness", new Stat());
        StatTable.Add("Aggression", new Stat());
        StatTable.Add("Focus", new Stat());
        StatTable.Add("Decisions", new Stat());
        StatTable.Add("Positioning", new Stat());
        StatTable.Add("Mechanics", new Stat());
        StatSum = 6;
        PhaseTable = new List<Phase>();
        PhaseTable.Add(new Phase("Early Game"));
        PhaseTable.Add(new Phase("Mid Game"));
        PhaseTable.Add(new Phase("Late Game"));
        TowerTable = new List<int>();
        TowerTable.Add(MaxTowerHealth);
        TowerTable.Add(MaxTowerHealth);
        TowerTable.Add(MaxTowerHealth*2);
        CurrentTower = 0;
        Gold = new Gold();
    }

    // Random Generators for creating random characters
    private void RandomStats(int NumberOfStats = 6)
    {
        System.Random Generator  = new System.Random();
        // randomly assign the stats Number of Stats Times
        for(int i = 0; i < NumberOfStats; i++)
        {
            int index = Generator.Next(0,6);
            ModStat(((StatName)index).ToString(), 1);
        }
    }
 
    // Randomly chooses the tactic for each phase
    private void RandomTactic(int StartingProficiency = -1)
    {
        System.Random Generator  = new System.Random();
        PhaseTable[0].ChangeTactic(((TacticName)Generator.Next(0,3)).ToString());
        PhaseTable[1].ChangeTactic(((TacticName)Generator.Next(0,3)).ToString());
        PhaseTable[2].ChangeTactic(((TacticName)Generator.Next(0,3)).ToString());
        // if StartingProficiency is -1, then randomly generate it.
        if (StartingProficiency == -1)
        {
            ChangeTacticProficiency(0, PhaseTable[0].CurrentTactic, Generator.Next(0,101));
            ChangeTacticProficiency(1, PhaseTable[1].CurrentTactic, Generator.Next(0,101));
            ChangeTacticProficiency(2, PhaseTable[2].CurrentTactic, Generator.Next(0,101));
        }
        else
        {
            ChangeTacticProficiency(0, PhaseTable[0].CurrentTactic, StartingProficiency);
            ChangeTacticProficiency(1, PhaseTable[1].CurrentTactic, StartingProficiency);
            ChangeTacticProficiency(2, PhaseTable[2].CurrentTactic, StartingProficiency);
        }
    }

    public int GetPhaseNum()
    {
        return PhaseNum;
    }

    public void ChangeTacticProficiency(int PhaseName, string TacticName, int Proficiency)
    {
        PhaseTable[PhaseName].ChangeTacticProficiency(TacticName, Proficiency);
    }
    
    // decreases current health by the damage parameter
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log(Name + " takes " + damage + " damage.");
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    // changes experience for the player's stats, modified by the happiness mod.
    private void ChangeStatExperience(string StatName, int amount)
    {
        // calculate base, 50% of all amount
        int baseXP = amount/2;
        // happiness is other 50%
        int happinessBonus = amount*StatTable["Happiness"].Value/MaxStat;
        StatTable[StatName].ChangeExperience(baseXP+happinessBonus);
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
            Debug.Log(Name + " cannot have less than 0 stat");
        }
        else if (StatTable[StatName].Value > MaxStat)
        {
            StatSum -= StatTable[StatName].Value - MaxStat;
            StatTable[StatName] -= StatTable[StatName].Value - MaxStat;
            Debug.Log(Name + " cannot have stats more than " + MaxStat);
        }
    }

    //returns string of highest value stat
    public string GetHighestStat()
    {
        int maxValue = 0;
        string statName = "";
        foreach(KeyValuePair<string, Stat> entry in StatTable)
        {
            if (entry.Value.Value > maxValue)
            {
                statName = entry.Key;
                maxValue = entry.Value.Value;
            }
        }
        return statName;
    }

    //return datetime, overwritten 
    public virtual System.DateTime GetDate()
    {
        return new System.DateTime(2023,8,4);
    }
    
    public void TrainStat(string StatName, int Experience)
    {
        StatTable[StatName].ChangeExperience(Experience);
    }

    public void TrainTactic(int Experience)
    {
        ChangeTacticProficiency(0, PhaseTable[0].CurrentTactic, Experience);
        ChangeTacticProficiency(1, PhaseTable[1].CurrentTactic, Experience);
        ChangeTacticProficiency(2, PhaseTable[2].CurrentTactic, Experience);
    }

    public void KilledEnemy(int Kills = 1)
    {
        Gold.KillToGold(Kills);
    }

    public bool Dead()
    {
        return CurrentHealth <= 0 ? true : false;
    }

    public void Die()
    {
        // Increase Deaths
        Deaths += 1;
        //Die in some way
        Debug.Log(Name + " died.");
    }

    // Sets all variables needed for a battle
    public void SetBattle()
    {
        CurrentHealth = MaxHealth;
        Gold.Reset();
        Kills = 0;
        Deaths = 0;
        TowerTable[0] = MaxTowerHealth;
        TowerTable[1] = MaxTowerHealth;
        TowerTable[2] = MaxTowerHealth*2; 
        CurrentTower = 0;
    }

    // Determine Distance used for combat, returns int between 0-3
    public int Distance(int EnemyPosition, int CurrentPosition = 0, int CurrentPhase = 1)
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
    public int GetCharacterPosition(int CurrentPhase = 1)
    {
        string Tactic = PhaseTable[CurrentPhase].CurrentTactic;
        int TacticProficiency = PhaseTable[CurrentPhase][Tactic].Proficiency;
        // calculate what the value of the roll is based off of player stats.
        int PlayerPosition = PhaseTable[CurrentPhase].GetTacticPosition(StatTable);
        return PlayerPosition;
    }

    // returns integer between 1 and 3
    public int GetCharacterDecision(int EnemyPosition, int CurrentPosition = 0, int CurrentPhase = 1)
    {
        if (CurrentPosition == 0)
        {
            CurrentPosition = GetCharacterPosition(CurrentPhase);
        }
        // How does character make decision based of position
        System.Random Generator = new System.Random();
        int DecisionRoll = Generator.Next(1,101);
        int distance = Distance(EnemyPosition, CurrentPosition, CurrentPhase);
        if(DecisionRoll + StatTable["Decisions"].Value >= 25)
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
    // returns amount of farm
    public int Farm(int AttackedDamage, int CurrentPosition = 0, int CurrentPhase = 1)
    {
        System.Random Generator = new System.Random();
        int FarmPenalty = 0;
        int FarmScore = 0;
        // if character is attacked, reduce farm by 0->50%
        if(AttackedDamage > 0)
        {
            // either 50% penalty or AttackDamage if damage less than 50.
            FarmPenalty = Math.Min(AttackedDamage,50);
        }

        for(int i = 0; i < Generator.Next(5,13);i++)
        {
            if(Generator.Next(1,101) + StatTable["Mechanics"].Value >= 30)
            {
                FarmScore++;
            }
        }
        // Debug.Log("FarmScore: " + (FarmScore).ToString());
        // Debug.Log("FarmPenalty Default: " + FarmPenalty.ToString());
        // Debug.Log("FarmPenalty Actual: " + ((100-FarmPenalty)/100).ToString());
        // Debug.Log("Farm Actual: " + ((int)(FarmScore*((100-FarmPenalty)/100))).ToString());
        ResolveFarm((int)(FarmScore*((100-FarmPenalty)/100)));
        return (int)(FarmScore*((100-FarmPenalty)/100)); 
    }

    private void ResolveFarm(int NewFarm)
    {
        Gold.FarmToGold(NewFarm); 
    }

    // player deals poking damage
    public int Poke(int EnemyPosition, int CurrentPosition = 0, int CurrentPhase = 1)
    {
        System.Random Generator = new System.Random();
        int DistancePenalty = Distance(EnemyPosition, CurrentPosition, CurrentPhase) > 2 ? 1 : 0;
        int PokeDamage = 0;
        // damage is half of full damage from an engage
        for(int i = 0; i < (int)(Generator.Next(Gold+BaseDamageMin, Gold+BaseDamageMax)*.5); i++)
        {
            if(Generator.Next(1,101) + StatTable["Mechanics"].Value >= 30)
            {
                PokeDamage++;
            }
        }
        Debug.Log("Poke Damage Actual: " + (PokeDamage*(1-DistancePenalty/2)).ToString());
        // If Penalty is 1, 1-1/2 = .5, if penalty is 0, 1-0/2 = 1, full damage.
        return (int)(PokeDamage*(1-DistancePenalty/2));
    }

    // player deals Engage Damage
    public int Engage(int EnemyPosition, int CurrentPosition = 0, int CurrentPhase = 1)
    {
        System.Random Generator = new System.Random();
        int DistancePenalty = Distance(EnemyPosition, CurrentPosition, CurrentPhase) > 1 ? 1 : 0;
        int EngageDamage = 0;
        if(CurrentPosition == 0)
        {
            CurrentPosition = GetCharacterPosition(CurrentPhase);
        }
        // distance of 1 = standard, distance of 2+ = 50% reduction
        for(int i = 0; i < (int)(Generator.Next(Gold+BaseDamageMin, Gold+BaseDamageMax)); i++)
        {
            if(Generator.Next(1,101) + StatTable["Mechanics"].Value >= 30)
            {
                EngageDamage++;
            }
        }
        // If Penalty is 1, 1-1/2 = .5, if penalty is 0, 1-0/2 = 1, full damage.
        return (int)(EngageDamage*(1-DistancePenalty/2));
    }
    // Used for combat, occurs when enemy character Engage
    // Enemy Position for damage numbers, Defending an all.
    public int EngageDefence(int EnemyPosition, int CurrentPosition = 0, int CurrentPhase = 1)
    {
        System.Random Generator = new System.Random();
        int DecisionRoll = Generator.Next(1,101);
        int DamageRoll;
        // Defending against an Engage.
        // damage determined by decision
        // firstly decision to trade(50%), all in back(100% damage), or farm(20% damage).
        // decision is based off of distance between players, farm is distance of 3, trade is distance 2, Engage back is distance 1+.
        // distance determined by position 1,1 = distance 3, etc.
        int distance = Distance(EnemyPosition, CurrentPosition, CurrentPhase);
        if(DecisionRoll + StatTable["Decisions"].Value >= 30)
        {
            int DamageTotal = 0;
            switch(distance)
            {
                case 1:
                // distance is 1, all in back
                DamageRoll = (int)(Generator.Next(Gold+BaseDamageMin, Gold+BaseDamageMax));
                for(int i = 0; i < DamageRoll; i++)
                {   
                    if(Generator.Next(1,101) + StatTable["Mechanics"].Value >= 30)
                    {
                        DamageTotal++;
                    }
                }
                return DamageTotal;
                case 2:
                DamageRoll = (int)(Generator.Next(Gold+BaseDamageMin, Gold+BaseDamageMax)*.5);
                for(int i = 0; i < DamageRoll; i++)
                {   
                    if(Generator.Next(1,101) + StatTable["Mechanics"].Value >= 30)
                    {
                        DamageTotal++;
                    }
                }
                return DamageTotal;
                case 3:
                DamageRoll = (int)(Generator.Next(Gold+BaseDamageMin, Gold+BaseDamageMax)*.2);
                for(int i = 0; i < DamageRoll; i++)
                {   
                    if(Generator.Next(1,101) + StatTable["Mechanics"].Value >= 30)
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

    public int GetInitiative()
    {
        System.Random Generator = new System.Random();
        int val = StatTable["Focus"]*3+Generator.Next(1,101);
        Debug.Log($"{Name}'s Initiative is {val}");
        return val;
    }

    // damage is dealt through poke(default 0) and if zero then enemy is dead.
    // returns tower damage that is dealt to the tower.
    public int DealTowerDamage()
    {
        System.Random Generator = new System.Random();
        // deal 50+gold*2 to 100+gold*2 damage to the tower
        int DamageTotal = Generator.Next(25+Gold,51+Gold);
        int DamageRoll = 0;
        for(int i = 0; i < DamageTotal; i++)
        {
            if(StatTable["Mechanics"] + Generator.Next(1,101) >= 30)
            {
                DamageRoll++;
            }
        }
        return DamageRoll;
    }

    public void LoseTowerHealth(int DamageDealt)
    {
        TowerTable[CurrentTower] -= DamageDealt;
        if (TowerTable[CurrentTower] <= 0)
        {
            TowerTable[CurrentTower] = 0;
        }
    }
}
