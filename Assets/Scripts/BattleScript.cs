using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Text class
[System.Serializable]
public class Message
{
    public string text;
    public TextMeshProUGUI TextObject;
}


public class BattleScript : MonoBehaviour
{
    [SerializeField] GameManager Manager;
    CharacterStats Player;
    CharacterStats Enemy;
    [SerializeField] GameObject PlayerStatusObject;
    [SerializeField] GameObject EnemyStatusObject;
    [SerializeField] GameObject PlayerTowerStatusObject;
    [SerializeField] GameObject EnemyTowerStatusObject;
    [SerializeField] TextMeshProUGUI PlayerNameText;
    [SerializeField] TextMeshProUGUI EnemyNameText;
    // used for text
    public int MaxMessages = 25;
    public GameObject ChatPanel, TextObject;
    List<Message> MessageList = new List<Message>();
    float TimeDelay = 1f;
    int CurrentPhase = 0;
    // 0 for the player, 1 for the enemy
    public int Winner;

    // adjectives for where the player positions(relative to your minions)
    public enum PositionText
    {
        behind = 1,
        amidst = 2,
        after = 3
    }
    // Tactic chosen based off of Decision
    public enum DecisionText
    {
        farm = 1,
        poke = 2,
        engage = 3
    }

    // used to send text
    public void SendMessageToChat(string text)
    {
        if (MessageList.Count >= MaxMessages)
        {
            Destroy(MessageList[0].TextObject.gameObject);
            MessageList.Remove(MessageList[0]);
        }
        Message NewMessage = new Message();
        NewMessage.text = text;
        GameObject NewText  = Instantiate(TextObject, ChatPanel.transform);
        NewMessage.TextObject = NewText.GetComponent<TextMeshProUGUI>();
        NewMessage.TextObject.text = NewMessage.text;
        MessageList.Add(NewMessage);
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager").GetComponent<GameManager>();
        }
        catch(System.NullReferenceException err)
        {
            Debug.Log("BattleScript Manager can't be found: " + err.Message);
        }
        try
        {
            
            Player = Manager.GetPlayer();
        }
        catch(System.NullReferenceException err)
        {
            Player = new CharacterStats();
            Player.BuildNew("Player Dummy");
            Debug.Log("BattleScript Player bugged: " + err.Message);
        }
        
        try
        {
            EnemyManager EM = Manager.GetEnemyManager();
            Debug.Log($"EM created");
            Enemy = EM.GetCurrentEnemy();
        }
        // we will randomly generate a character if there isn't one
        catch(System.NullReferenceException err)
        {
            Enemy = new CharacterStats();
            Enemy.BuildNew("Dummy");
            Debug.Log("BattleScript Enemy bugged: " + err.Message);
        }
        //assign Names to text boxes
        PlayerNameText.text = Player.Name;
        EnemyNameText.text = Enemy.Name;
        // Assign tower max health
        PlayerTowerStatusObject.transform.GetChild(1).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[0]);
        PlayerTowerStatusObject.transform.GetChild(3).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[1]);
        PlayerTowerStatusObject.transform.GetChild(5).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[2]);
        EnemyTowerStatusObject.transform.GetChild(1).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[0]);
        EnemyTowerStatusObject.transform.GetChild(3).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[1]);
        EnemyTowerStatusObject.transform.GetChild(5).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[2]);
    }
    // Updates the Status of the Player
    public void PlayerStatusUpdate()
    {
        try
        {
            PlayerStatusObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Player.CurrentHealth.ToString();
            PlayerStatusObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = Player.Gold.ToString();
            PlayerStatusObject.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = Player.Gold.GetCreepScore().ToString();
            PlayerStatusObject.transform.GetChild(11).GetComponent<TextMeshProUGUI>().text = Player.Kills.ToString();
            PlayerStatusObject.transform.GetChild(14).GetComponent<TextMeshProUGUI>().text = Player.Deaths.ToString();
            PlayerStatusObject.transform.GetChild(17).GetComponent<TextMeshProUGUI>().text = Player.PhaseTable[CurrentPhase].CurrentTactic;
            PlayerTowerStatusObject.transform.GetChild(1).GetComponent<HealthBar>().SetHealth(Player.TowerTable[0]);
            PlayerTowerStatusObject.transform.GetChild(3).GetComponent<HealthBar>().SetHealth(Player.TowerTable[1]);
            PlayerTowerStatusObject.transform.GetChild(5).GetComponent<HealthBar>().SetHealth(Player.TowerTable[2]);
        }
        catch(System.NullReferenceException err)
        {
            Debug.Log("Player Status bugged and cannot be updated: " + err.Message);
        }
    }
    // Updates the Status of the Enemy
    public void EnemyStatusUpdate()
    {
        try
        {
            EnemyStatusObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Enemy.CurrentHealth.ToString();
            EnemyStatusObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = Enemy.Gold.ToString();
            EnemyStatusObject.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = Enemy.Gold.GetCreepScore().ToString();
            EnemyStatusObject.transform.GetChild(11).GetComponent<TextMeshProUGUI>().text = Enemy.Kills.ToString();
            EnemyStatusObject.transform.GetChild(14).GetComponent<TextMeshProUGUI>().text = Enemy.Deaths.ToString();
            EnemyStatusObject.transform.GetChild(17).GetComponent<TextMeshProUGUI>().text = Enemy.PhaseTable[CurrentPhase].CurrentTactic;
            EnemyTowerStatusObject.transform.GetChild(1).GetComponent<HealthBar>().SetHealth(Enemy.TowerTable[0]);
            EnemyTowerStatusObject.transform.GetChild(3).GetComponent<HealthBar>().SetHealth(Enemy.TowerTable[1]);
            EnemyTowerStatusObject.transform.GetChild(5).GetComponent<HealthBar>().SetHealth(Enemy.TowerTable[2]);
        }
        catch(System.NullReferenceException err)
        {
            Debug.Log("Player Status bugged and cannot be updated: " + err.Message);
        }
        
    }

    public void StartBattle()
    {
        StartCoroutine(SimulateBattle());
    }

    //Wait For Seconds only works in the Coroutine that it is called.
    // (think of it as starts a thread/sends the instructions x seconds into the future)
    // Solution make this an IEnumerator, and make a Wrapper
    IEnumerator SimulateBattle()
    {
        
        // initiate Position, Decision, Damage, and String
        int PlayerPosition, PlayerDecision, PlayerDamage = 0;
        int EnemyPosition, EnemyDecision, EnemyDamage = 0;
        // reset both players health to 100
        // reset gold, farm, and k/d of players to 0
        Player.SetBattle();
        Enemy.SetBattle();
        CurrentPhase = 0;
        int TurnCounter = 0;
        // breakout for testing
        int BreakOut = 0;
        EnemyStatusUpdate();
        PlayerStatusUpdate();
        bool GameOver = false;
        // fight while Tower is still alive,
        while (GameOver == false && BreakOut < 100)
        {
            BreakOut++;
            // start the action
            PlayerPosition = Player.GetCharacterPosition(CurrentPhase);
            EnemyPosition = Enemy.GetCharacterPosition(CurrentPhase);
            Debug.Log(Player.Name + " positions " + (PositionText)PlayerPosition + " the minion wave.");
            Debug.Log(Enemy.Name + " positions " + (PositionText)EnemyPosition + " the minion wave.");
            yield return new WaitForSeconds(TimeDelay);
            SendMessageToChat(Player.Name + " positions " + (PositionText)PlayerPosition + " the minion wave.");
            yield return new WaitForSeconds(TimeDelay);
            SendMessageToChat(Enemy.Name + " positions " + (PositionText)EnemyPosition + " the minion wave.");
            // Both players make decisions
            PlayerDecision = Player.GetCharacterDecision(EnemyPosition, PlayerPosition, CurrentPhase);
            EnemyDecision = Enemy.GetCharacterDecision(PlayerPosition, EnemyPosition, CurrentPhase);
            // Determine Turn Order based on Focus
            if(Player.GetInitiative() > Enemy.GetInitiative())
            {
                // Player goes first
                yield return new WaitForSeconds(TimeDelay);
                switch(PlayerDecision)
                {
                    case 3:
                    SendMessageToChat(Player.Name + " engages on " + Enemy.Name + "!");
                    PlayerDamage = Player.Engage(EnemyPosition, PlayerPosition, CurrentPhase);
                    break;
                    case 2:
                    SendMessageToChat(Player.Name + " chooses to " + (DecisionText)PlayerDecision + ".");
                    PlayerDamage = Player.Poke(EnemyPosition, PlayerPosition, CurrentPhase);
                    break;
                    case 1:
                    default:
                    int farm = Player.Farm(0, PlayerPosition, CurrentPhase);
                    SendMessageToChat(Player.Name + " chooses to " + (DecisionText)PlayerDecision + ". " + Player.Name + " gets " + farm.ToString() + " farm.");
                    PlayerStatusUpdate();
                    PlayerDamage = 0;
                    break;
                }
                    // Enemy takes damage first
                if (PlayerDecision != 1)
                {
                    // If Player is poking, deal tower damage through poke
                    if(PlayerDecision == 2)
                    {
                        yield return new WaitForSeconds(TimeDelay);
                        EnemyTowerPoked(PlayerDamage);
                    }
                    yield return new WaitForSeconds(TimeDelay);
                    EnemyTakeDamage(PlayerDamage);
                }
                if(Enemy.Dead())
                {
                    // Enemy died give kill to player
                    Player.Kills += 1;
                    Player.KilledEnemy(1);
                    PlayerStatusUpdate();
                    // Enemy Died, Take Turret Damage
                    yield return new WaitForSeconds(TimeDelay);
                    EnemyTakeTowerDamage();
                    // check for enemy tower health.
                    if(Enemy.TowerTable[Enemy.CurrentTower] <= 0)
                    {
                        Enemy.CurrentTower++;
                        if(Enemy.CurrentTower > 2)
                        {
                            GameOver = true;
                            Enemy.CurrentTower = 2;
                        }
                        // Update CurrentPhase to match Player or Enemy Tower Phase
                        CurrentPhase = System.Math.Max(Player.CurrentTower, Enemy.CurrentTower);
                        EnemyStatusUpdate();
                    }
                }
                else
                {
                    // Enemy didn't die and gets to go
                    yield return new WaitForSeconds(TimeDelay);
                    if(PlayerDecision == 3 && EnemyDecision != 3)
                    {
                        // Enemy engage defence activated
                        SendMessageToChat(Enemy.Name + " defends against the engage!");
                        EnemyDamage = Enemy.EngageDefence(PlayerPosition, EnemyPosition, CurrentPhase);
                    }
                    else
                    {
                        switch(EnemyDecision)
                        {
                            case 3:
                            SendMessageToChat(Enemy.Name + " engages on " + Player.Name + "!");
                            EnemyDamage = Enemy.Engage(PlayerPosition, EnemyPosition, CurrentPhase);
                            break;
                            case 2:
                            SendMessageToChat(Enemy.Name + " chooses to " + (DecisionText)EnemyDecision + ".");
                            EnemyDamage = Enemy.Poke(PlayerPosition, EnemyPosition, CurrentPhase);
                            break;
                            case 1:
                            default:
                            int farm = Enemy.Farm(PlayerDamage, EnemyPosition, CurrentPhase);
                            SendMessageToChat(Enemy.Name + " chooses to " + (DecisionText)EnemyDecision + ". " + Enemy.Name + " gets " + farm.ToString() + " farm.");
                            EnemyDamage = 0;
                            EnemyStatusUpdate();
                            break;
                        }
                    }
                    // Enemy is not dead, Enemy deals damage back
                    if(EnemyDecision != 1)
                    {
                        // If Enemy is poking, deal tower damage through poke
                        if(EnemyDecision == 2)
                        {
                            yield return new WaitForSeconds(TimeDelay);
                            PlayerTowerPoked(EnemyDamage);
                        }
                        yield return new WaitForSeconds(TimeDelay);
                        PlayerTakeDamage(EnemyDamage);
                        //check if player died and if enemy deals turret damage
                        if(Player.Dead())
                        {
                            // Player died, give a kill to the Enemy
                            Enemy.Kills += 1;
                            Enemy.KilledEnemy(1);
                            EnemyStatusUpdate();
                            yield return new WaitForSeconds(TimeDelay);
                            PlayerTakeTowerDamage();
                            // check for player tower health.
                            if(Player.TowerTable[Player.CurrentTower] <= 0)
                            {
                                Player.CurrentTower++;
                                if(Player.CurrentTower > 2)
                                {
                                    GameOver = true;
                                    Player.CurrentTower = 2;
                                }
                                // Update CurrentPhase to match Player or Enemy Tower Phase
                                CurrentPhase = System.Math.Max(Player.CurrentTower, Enemy.CurrentTower);
                                PlayerStatusUpdate();
                            }
                        }
                    }
                }
            }
            else
            {
                // Enemy goes first
                yield return new WaitForSeconds(TimeDelay);
                switch(EnemyDecision)
                {
                    case 3:
                    SendMessageToChat(Enemy.Name + " engages on " + Player.Name + "!");
                    EnemyDamage = Enemy.Engage(PlayerPosition, EnemyPosition, CurrentPhase);
                    break;
                    case 2:
                    SendMessageToChat(Player.Name + " chooses to " + (DecisionText)EnemyDecision + ".");
                    EnemyDamage = Enemy.Poke(PlayerPosition, EnemyPosition, CurrentPhase);
                    break;
                    case 1:
                    default:
                    int farm = Enemy.Farm(0, EnemyPosition, CurrentPhase);
                    SendMessageToChat(Enemy.Name + " chooses to " + (DecisionText)EnemyDecision + ". " + Enemy.Name + " gets " + farm.ToString() + " farm.");
                    EnemyDamage = 0;
                    EnemyStatusUpdate();
                    break;
                }
                // player takes damage first
                if(EnemyDecision != 1)
                {
                    // If Player is poking, deal tower damage through poke
                    if(EnemyDecision == 2)
                    {
                        yield return new WaitForSeconds(TimeDelay);
                        PlayerTowerPoked(EnemyDamage);
                    }
                    
                    yield return new WaitForSeconds(TimeDelay);
                    PlayerTakeDamage(EnemyDamage);
                }
                if(Player.Dead())
                {
                    // Player dead, give kill to enemy
                    Enemy.Kills += 1;
                    Enemy.KilledEnemy(1);
                    EnemyStatusUpdate();
                    yield return new WaitForSeconds(TimeDelay);
                    // Enemy died and deals no damage, player deals tower damage 
                    PlayerTakeTowerDamage();
                    // check for player tower health.
                    if(Player.TowerTable[Player.CurrentTower] <= 0)
                    {
                        Player.CurrentTower++;
                        if(Player.CurrentTower > 2)
                        {
                            GameOver = true;
                            Player.CurrentTower = 2;
                        }
                        // Update CurrentPhase to match Player or Enemy Tower Phase
                        CurrentPhase = System.Math.Max(Player.CurrentTower, Enemy.CurrentTower);
                        PlayerStatusUpdate();
                    }
                }
                else
                {
                    // Player didn't die and gets to go
                    yield return new WaitForSeconds(TimeDelay);
                    if(EnemyDecision == 3 && PlayerDecision != 3)
                    {
                        // Enemy engage defence activated
                        SendMessageToChat(Player.Name + " defends against the engage!");
                        PlayerDamage = Enemy.EngageDefence(PlayerPosition, EnemyPosition, CurrentPhase);
                    }
                    else
                    {
                        switch(PlayerDecision)
                        {
                            case 3:
                            SendMessageToChat(Player.Name + " engages on " + Enemy.Name + "!");
                            PlayerDamage = Player.Engage(EnemyPosition, PlayerPosition, CurrentPhase);
                            break;
                            case 2:
                            SendMessageToChat(Player.Name + " chooses to " + (DecisionText)PlayerDecision + ".");
                            PlayerDamage = Player.Poke(EnemyPosition, PlayerPosition, CurrentPhase);
                            break;
                            case 1:
                            default:
                            int farm = Player.Farm(EnemyDamage, EnemyPosition, CurrentPhase);
                            SendMessageToChat(Player.Name + " chooses to " + (DecisionText)PlayerDecision + ". " + Player.Name + " gets " + farm.ToString() + " farm.");
                            PlayerDamage = 0;
                            PlayerStatusUpdate();
                            break;
                        }
                    }
                    //Player deals damage back
                    if(PlayerDamage != 1)
                    {
                        // If Player is poking, deal tower damage through poke
                        if(PlayerDecision == 2)
                        {
                            yield return new WaitForSeconds(TimeDelay);
                            EnemyTowerPoked(PlayerDamage);
                        }
                        yield return new WaitForSeconds(TimeDelay);
                        EnemyTakeDamage(PlayerDamage);
                        //check if enemy died and player deals turret damage
                        if(Enemy.Dead())
                        {
                            Player.Kills += 1;
                            Player.KilledEnemy(1);
                            PlayerStatusUpdate();
                            yield return new WaitForSeconds(TimeDelay);
                            EnemyTakeTowerDamage();
                            // check for enemy tower health.
                            if(Enemy.TowerTable[Enemy.CurrentTower] <= 0)
                            {
                                Enemy.CurrentTower++;
                                if(Enemy.CurrentTower > 2)
                                {
                                    GameOver = true;
                                    Enemy.CurrentTower = 2;
                                }
                                // Update CurrentPhase to match Player or Enemy Tower Phase
                                CurrentPhase = System.Math.Max(Player.CurrentTower, Enemy.CurrentTower);
                                EnemyStatusUpdate();
                            }
                        }
                    }   
                }
            }
            // increment TurnCounter  
            TurnCounter++;
            if(Player.Dead())
            {
                Player.CurrentHealth = CharacterStats.MaxHealth;
                PlayerStatusUpdate();
            }
            if(Enemy.Dead())
            {
                Enemy.CurrentHealth = CharacterStats.MaxHealth;
                EnemyStatusUpdate();
            }
        }
        // Game Over, show post match thread, close Dialog and bring up BattleOverScene.
        try 
        {
            Winner = Enemy.CurrentTower > 2 ? 0 : 1;
            // close current screen
            gameObject.GetComponent<TransitionTextScript>().CloseDialog();
            // brings up winner screen
        
        }
        catch(System.NullReferenceException err)
        {
            Winner = 0;
            Debug.Log($"Battle script can't transition {err.Message}");
        }
    }

    public int GetWinner()
    {
        return Winner;
    }

    private void PlayerTakeDamage(int EnemyDamage)
    {
        // resolve damage dealt to the player
        Player.TakeDamage(EnemyDamage); 
        // Update Text on health status and on Battle Text.
        SendMessageToChat(Enemy.Name + " deals " + EnemyDamage.ToString() + " damage to " + Player.Name + ".");
        PlayerStatusUpdate();
    }

    private void PlayerTowerPoked(int EnemyDamage)
    {
        // if the Enemy uses Poke damage, then the Turrets are damaged as well.(1-1 ratio)
        Player.LoseTowerHealth(EnemyDamage);
        SendMessageToChat(Enemy.Name + " deals " + EnemyDamage.ToString() + " damage to " + Player.Name + "'s tower through Poke.");
        PlayerStatusUpdate();
    }

    private void PlayerTakeTowerDamage()
    {
        // Player is Dead, Enemy deals Turret Damage
        int TowerDamage = Enemy.DealTowerDamage();
        Player.LoseTowerHealth(TowerDamage);
        SendMessageToChat(Enemy.Name + " deals " + TowerDamage.ToString() + " damage to " + Player.Name + "'s tower after " + Player.Name + " has died.");
        PlayerStatusUpdate();
    }
    private void EnemyTakeDamage(int PlayerDamage)
    {
        // resolve damage dealt to the Enemy
        Enemy.TakeDamage(PlayerDamage);
        // Update Text on health status and on Battle Text.
        SendMessageToChat(Player.Name + " deals " + PlayerDamage.ToString() + " damage to " + Enemy.Name + ".");
        EnemyStatusUpdate();
    }

    private void EnemyTowerPoked(int PlayerDamage)
    {
        // if the Player uses Poke damage, then the Turrets are damaged as well.(1-1 ratio)
        Enemy.LoseTowerHealth(PlayerDamage);
        SendMessageToChat(Player.Name + " deals " + PlayerDamage.ToString() + " damage to " + Enemy.Name + "'s tower through Poke.");
        EnemyStatusUpdate();
    }

    private void EnemyTakeTowerDamage()
    {
        // Enemy is Dead, Player deals Turret Damage
        int TowerDamage = Player.DealTowerDamage();
        Enemy.LoseTowerHealth(TowerDamage);
        SendMessageToChat(Player.Name + " deals " + TowerDamage.ToString() + " damage to " + Enemy.Name + "'s tower after " + Enemy.Name + " has died.");
        EnemyStatusUpdate();
    }
}
