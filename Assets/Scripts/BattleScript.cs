using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour
{
    [SerializeField] GameObject Manager;
    CharacterStats Player;
    [SerializeField] CharacterStats Enemy;
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("UniversalGameEngine");
        Player = Manager.GetComponent<PlayerStats>();
        // we will randomly generate a character.
        if (Enemy == null)
        {
            Enemy = new CharacterStats();
        }
    }

    public void SimulateBattle()
    {
        // initiate PlayerPosition/EnemyPosition
        int PlayerPosition, PlayerDecision, PlayerDamage;
        int EnemyPosition, EnemyDecision, EnemyDamage;
        // reset both players health to 100
        Player.CurrentHealth = 100;
        Enemy.CurrentHealth = 100;
        // reset gold and farm of players to 0
        Player.CreepScore = 0;
        Enemy.CreepScore = 0;
        Player.Gold = 0;
        Enemy.Gold = 0;
        string CurrentPhase = "Early Game";
        int TurnCounter = 0;
        while (Player.CurrentHealth >= 0 && Enemy.CurrentHealth >= 0)
        {
            PlayerPosition = Player.GetCharacterPosition(CurrentPhase);
            EnemyPosition = Enemy.GetCharacterPosition(CurrentPhase);
            // Both players make decisions
            PlayerDecision = Player.GetCharacterDecision(EnemyPosition, PlayerPosition, CurrentPhase);
            EnemyDecision = Enemy.GetCharacterDecision(PlayerPosition, EnemyPosition, CurrentPhase);
            // check what the decisions of the Player and Enemy, if both Engage then they fight, 
            // else if one engage, engage Defence Activated.
            switch(PlayerDecision)
            {
                // access what is the player's decision
                case 3:
                switch(EnemyDecision)
                {
                    case 3:
                    PlayerDamage = Player.Engage(EnemyPosition, PlayerPosition, CurrentPhase);
                    EnemyDamage = Enemy.Engage(PlayerPosition, EnemyPosition, CurrentPhase);
                    break;
                    case 2:
                    break;
                    case 1:
                    default:
                    PlayerDamage = Player.Engage(EnemyPosition, PlayerPosition, CurrentPhase);
                    EnemyDamage = Enemy.EngageDefence(PlayerPosition, EnemyPosition, CurrentPhase);
                    break;
                }
                break;
                case 2:
                switch(EnemyDecision)
                {
                    case 3:
                    PlayerDamage = Player.EngageDefence(EnemyPosition, PlayerPosition, CurrentPhase);
                    EnemyDamage = Enemy.Engage(PlayerPosition, EnemyPosition, CurrentPhase);
                    break;
                    case 2:
                    PlayerDamage = Player.Poke(EnemyPosition, PlayerPosition, CurrentPhase);
                    EnemyDamage = Enemy.Poke(PlayerPosition, EnemyPosition, CurrentPhase);
                    break;
                    case 1:
                    default:
                    PlayerDamage = Player.Poke(EnemyPosition, PlayerPosition, CurrentPhase);
                    EnemyDamage = Enemy.Farm(PlayerPosition, EnemyPosition, CurrentPhase);
                    break;
                }
                break;
                case 1:
                default:
                switch(EnemyDecision)
                {
                    case 3:
                    PlayerDamage = Player.EngageDefence(EnemyPosition, PlayerPosition, CurrentPhase);
                    EnemyDamage = Enemy.Engage(PlayerPosition, EnemyPosition, CurrentPhase);
                    break;
                    case 2:
                    PlayerDamage = Player.Farm(EnemyPosition, PlayerPosition, CurrentPhase);
                    EnemyDamage = Enemy.Poke(PlayerPosition, EnemyPosition, CurrentPhase);
                    break;
                    case 1:
                    default:
                    PlayerDamage = Player.Farm(EnemyPosition, PlayerPosition, CurrentPhase);
                    EnemyDamage = Enemy.Farm(PlayerPosition, EnemyPosition, CurrentPhase);
                    break;
                }
                break;
            }
            TurnCounter++;
            if(TurnCounter > 10 CurrentPhase != "Late Game")
            {   
                TurnCounter = 0;
                switch(CurrentPhase)
                {
                    case "Early Game":
                    CurrentPhase = "Mid Game";
                    break;
                    case "Mid Game":
                    CurrentPhase = "Late Game";
                    break;
                }
            }
        }
    }
}
