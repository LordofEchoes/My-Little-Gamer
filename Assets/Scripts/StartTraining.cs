using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartTraining : MonoBehaviour
{
    [SerializeField] GameObject Manager;
    private PlayerStats Player;
    public int TotalStatContainer = 6;
    [SerializeField] GameObject TrainingPointsObject;
    [SerializeField] TrainingAllocation TrainingPointsScript;
    [SerializeField] DateSystem DS;
    [SerializeField] GameObject DisplayGameObject;
    [SerializeField] TextMeshProUGUI[] StatText;

    void Start()
    {
        Manager = GameObject.Find("UniversalGameManager");
        Player = Manager.GetComponent<PlayerStats>();
        DS = Manager.GetComponent<DateSystem>();
        TrainingPointsScript = TrainingPointsObject.GetComponent<TrainingAllocation>();
    }

    void OnDisable()
    {
        for(int i = 0; i < TotalStatContainer; i++)
        {
            StatText[i].text = "+0";
        }
        Debug.Log("Stats have been disabled to zero.");
        DisplayGameObject.SetActive(false);
        Debug.Log("Training Stats display have been deactivated");
    }

    public void Train()
    {
        /*
        Experience: random number between 50 and 100. half is guaranteed. happiness modifier gives ratio of modifier over max happiness.
        Training Points: starts as a multiplier of .5. Adds 1 for each multiplier.
        Total Experience is split between categories
        */
        int[] StatDifference = new int[6];
        // train Mechanics: trains solely on Mechanics(80%) and Aggression(20%)
        double MechanicsTraining = TrainingPointsScript.TrainingPointsTable["Mechanics"] + .5;
        // train Tactics: trains tactic proficiency(60%) + Decision(20%) + Positioning(20%) stat
        double TacticsTraining = TrainingPointsScript.TrainingPointsTable["Tactics"] + .5;
        // train Knowledge: trains Focus(30%), Decisions(30%), Positioning(30%), and Aggression(10%)
        double KnowledgeTraining = TrainingPointsScript.TrainingPointsTable["Knowledge"]+ .5;
        // Happiness Modifier
        int HappinessModifier = Player.StatTable["Happiness"].Value;
        // Start Mechanics Training
        double TotalMechanicsExperience = new System.Random().Next(50,100)*MechanicsTraining;
        double GivenMechanicsExperience = TotalMechanicsExperience/2 + HappinessModifier*TotalMechanicsExperience/2/Player.MaxStat;
        Player.TrainStat("Mechanics", (int)System.Math.Truncate(GivenMechanicsExperience*.8));
        StatDifference[5] = (int)System.Math.Truncate(GivenMechanicsExperience*.8);
        Player.TrainStat("Aggression", (int)System.Math.Truncate(GivenMechanicsExperience*.2));
        StatDifference[1] = (int)System.Math.Truncate(GivenMechanicsExperience*.2);
        // Start Tactic Training
        double TotalTacticExperience = new System.Random().Next(50,100)*TacticsTraining;
        double GivenTacticExperience = TotalTacticExperience/2 + HappinessModifier*TotalTacticExperience/2/Player.MaxStat;
        Player.TrainTactic((int)System.Math.Truncate(GivenTacticExperience*.6));
        StatDifference[0] = (int)System.Math.Truncate(GivenTacticExperience*.6);
        Player.TrainStat("Decisions", (int)System.Math.Truncate(GivenTacticExperience*.2));
        StatDifference[3] = (int)System.Math.Truncate(GivenTacticExperience*.2);
        Player.TrainStat("Positioning", (int)System.Math.Truncate(GivenTacticExperience*.2));
        StatDifference[4] = (int)System.Math.Truncate(GivenTacticExperience*.2);
        // Start Knowledge Training
        double TotalKnowledgeExperience = new System.Random().Next(50,100)*KnowledgeTraining;
        double GivenKnowledgeExperience = TotalKnowledgeExperience/2 + HappinessModifier*TotalKnowledgeExperience/2/Player.MaxStat;
        Player.TrainStat("Focus", (int)System.Math.Truncate(GivenKnowledgeExperience*.3));
        StatDifference[2] = (int)System.Math.Truncate(GivenKnowledgeExperience*.3);
        Player.TrainStat("Decisions", (int)System.Math.Truncate(GivenKnowledgeExperience*.3));
        StatDifference[3] += (int)System.Math.Truncate(GivenKnowledgeExperience*.3);
        Player.TrainStat("Positioning", (int)System.Math.Truncate(GivenKnowledgeExperience*.3));
        StatDifference[4] += (int)System.Math.Truncate(GivenKnowledgeExperience*.3);
        Player.TrainStat("Aggression", (int)System.Math.Truncate(GivenKnowledgeExperience*.1));
        StatDifference[1] += (int)System.Math.Truncate(GivenKnowledgeExperience*.1);
        DisplayStats(StatDifference);
        DS.ProgressDay();
    }

    public void DisplayStats(int[] StatDifference)
    {
        DisplayGameObject.SetActive(true);
        for(int i = 0; i < TotalStatContainer; i++)
        {
            StatText[i].text = "+" + StatDifference[i].ToString();
            Debug.Log("Stat " + i.ToString() + " increased by " + StatDifference[i].ToString());
        }
    }
}