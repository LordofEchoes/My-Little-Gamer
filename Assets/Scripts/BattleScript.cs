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
        // Make sure players are set for battle
        Player.SetBattle();
        Enemy.SetBattle();
        // Assign tower max health
        PlayerTowerStatusObject.transform.GetChild(1).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[0]);
        PlayerTowerStatusObject.transform.GetChild(3).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[1]);
        PlayerTowerStatusObject.transform.GetChild(5).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[2]);
        EnemyTowerStatusObject.transform.GetChild(1).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[0]);
        EnemyTowerStatusObject.transform.GetChild(3).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[1]);
        EnemyTowerStatusObject.transform.GetChild(5).GetComponent<HealthBar>().SetMaxHealth(Player.TowerTable[2]);
    }

    // Updates the Status of a Character
    public void StatusUpdate(CharacterStats Character, GameObject StatusObject, GameObject TowerStatusObject)
    {
        try
        {
            StatusObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Character.CurrentHealth.ToString();
            StatusObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = Character.Gold.ToString();
            StatusObject.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = Character.Gold.GetCreepScore().ToString();
            StatusObject.transform.GetChild(11).GetComponent<TextMeshProUGUI>().text = Character.Kills.ToString();
            StatusObject.transform.GetChild(14).GetComponent<TextMeshProUGUI>().text = Character.Deaths.ToString();
            StatusObject.transform.GetChild(17).GetComponent<TextMeshProUGUI>().text = Character.PhaseTable[CurrentPhase].CurrentTactic;
            TowerStatusObject.transform.GetChild(1).GetComponent<HealthBar>().SetHealth(Character.TowerTable[0]);
            TowerStatusObject.transform.GetChild(3).GetComponent<HealthBar>().SetHealth(Character.TowerTable[1]);
            TowerStatusObject.transform.GetChild(5).GetComponent<HealthBar>().SetHealth(Character.TowerTable[2]);
        }
        catch(System.NullReferenceException err)
        {
            Debug.Log("Status bugged and cannot be updated: " + err.Message);
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
        CharacterStats CharacterA, CharacterB;
        GameObject StatusObjectA, TowerStatusObjectA;
        GameObject StatusObjectB, TowerStatusObjectB;
        // initiate Position, Decision, Damage, and String
        int PositionA, DecisionA, DamageA = 0;
        int PositionB, DecisionB, DamageB = 0;
        // reset both players health to 100
        // reset gold, farm, and k/d of players to 0
        Player.SetBattle();
        Enemy.SetBattle();
        CurrentPhase = 0;
        int TurnCounter = 0;
        // breakout for testing
        int BreakOut = 0;
        StatusUpdate(Enemy, EnemyStatusObject, EnemyTowerStatusObject);
        StatusUpdate(Player, PlayerStatusObject, PlayerTowerStatusObject);
        bool GameOver = false;
        // fight while Tower is still alive,
        while (GameOver == false && BreakOut < 100)
        {
            // Determine Turn Order based on Focus
            // CharacterA is whoever goes first.
            if(Player.GetInitiative() > Enemy.GetInitiative())
            {
                CharacterA = Player;
                StatusObjectA = PlayerStatusObject;
                TowerStatusObjectA = PlayerTowerStatusObject;
                CharacterB = Enemy;
                StatusObjectB = EnemyStatusObject;
                TowerStatusObjectB = EnemyTowerStatusObject;
                
            }
            else
            {
                CharacterA = Enemy;
                StatusObjectA = EnemyStatusObject;
                TowerStatusObjectA = EnemyTowerStatusObject;
                CharacterB = Player;
                StatusObjectB = PlayerStatusObject;
                TowerStatusObjectB = PlayerTowerStatusObject;
            }
            BreakOut++;
            // start the action
            PositionA = CharacterA.GetCharacterPosition(CurrentPhase);
            PositionB = CharacterB.GetCharacterPosition(CurrentPhase);
            Debug.Log(CharacterA.Name + " positions " + (PositionText)PositionA + " the minion wave.");
            Debug.Log(CharacterB.Name + " positions " + (PositionText)PositionB + " the minion wave.");
            yield return new WaitForSeconds(TimeDelay);
            SendMessageToChat(CharacterA.Name + " positions " + (PositionText)PositionA + " the minion wave.");
            yield return new WaitForSeconds(TimeDelay);
            SendMessageToChat(CharacterB.Name + " positions " + (PositionText)PositionB + " the minion wave.");
            // Both players make decisions
            DecisionA = CharacterA.GetCharacterDecision(PositionB, PositionA, CurrentPhase);
            DecisionB = CharacterB.GetCharacterDecision(PositionA, PositionB, CurrentPhase);
            // CharacterA goes first
            yield return new WaitForSeconds(TimeDelay);
            switch(DecisionA)
            {
                case 3:
                SendMessageToChat(CharacterA.Name + " engages on " + CharacterB.Name + "!");
                DamageA = CharacterA.Engage(PositionB, PositionA, CurrentPhase);
                break;
                case 2:
                SendMessageToChat(CharacterA.Name + " chooses to " + (DecisionText)DecisionA + ".");
                DamageA = CharacterA.Poke(PositionB, PositionA, CurrentPhase);
                break;
                case 1:
                default:
                int farm = CharacterA.Farm(0, PositionA, CurrentPhase);
                SendMessageToChat(CharacterA.Name + " chooses to " + (DecisionText)DecisionA + ". " + CharacterA.Name + " gets " + farm.ToString() + " farm.");
                StatusUpdate(CharacterA,StatusObjectA,TowerStatusObjectA);
                DamageA = 0;
                break;
            }
                // Enemy takes damage first
            if (DecisionA != 1)
            {
                // If Player is poking, deal tower damage through poke
                if(DecisionA == 2)
                {
                    yield return new WaitForSeconds(TimeDelay);
                    TowerPoked(CharacterB,CharacterA,DamageA,StatusObjectB,TowerStatusObjectB);
                }
                yield return new WaitForSeconds(TimeDelay);
                TakeDamage(CharacterB,CharacterA,DamageA,StatusObjectB,TowerStatusObjectB);
            }
            if(CharacterB.Dead())
            {
                // Enemy died give kill to player
                CharacterA.Kills += 1;
                CharacterA.KilledEnemy(1);
                StatusUpdate(CharacterA,StatusObjectA,TowerStatusObjectA);
                StatusUpdate(CharacterB,StatusObjectB,TowerStatusObjectB);
                // Enemy Died, Take Turret Damage
                yield return new WaitForSeconds(TimeDelay);
                TakeTowerDamage(CharacterB,CharacterA,DamageA,StatusObjectB,TowerStatusObjectB);
                // check for enemy tower health.
                if(CharacterB.TowerTable[CharacterB.CurrentTower] <= 0)
                {
                    CharacterB.CurrentTower++;
                    if(CharacterB.CurrentTower > 2)
                    {
                        GameOver = true;
                        CharacterB.CurrentTower = 2;
                        Winner = CharacterB == Player ? 1 : 0;
                        
                    }
                    // Update CurrentPhase to match Player or Enemy Tower Phase
                    CurrentPhase = System.Math.Max(CharacterA.CurrentTower, CharacterB.CurrentTower);
                    StatusUpdate(CharacterB,StatusObjectB,TowerStatusObjectB);
                }
            }
            else
            {
                // Enemy didn't die and gets to go
                yield return new WaitForSeconds(TimeDelay);
                if(DecisionA == 3 && DecisionB != 3)
                {
                    // Enemy engage defence activated
                    SendMessageToChat(CharacterB.Name + " defends against the engage!");
                    DamageB = CharacterB.EngageDefence(PositionA, PositionB, CurrentPhase);
                }
                else
                {
                    switch(DecisionB)
                    {
                        case 3:
                        SendMessageToChat(CharacterB.Name + " engages on " + CharacterA.Name + "!");
                        DamageB = CharacterB.Engage(PositionA, PositionB, CurrentPhase);
                        break;
                        case 2:
                        SendMessageToChat(CharacterB.Name + " chooses to " + (DecisionText)DecisionB + ".");
                        DamageB = CharacterB.Poke(PositionA, PositionB, CurrentPhase);
                        break;
                        case 1:
                        default:
                        int farm = CharacterB.Farm(DamageA, PositionB, CurrentPhase);
                        SendMessageToChat(CharacterB.Name + " chooses to " + (DecisionText)DecisionB + ". " + CharacterB.Name + " gets " + farm.ToString() + " farm.");
                        DamageB = 0;
                        StatusUpdate(CharacterB,StatusObjectB,TowerStatusObjectB);
                        break;
                    }
                }
                // Enemy is not dead, Enemy deals damage back
                if(DecisionB != 1)
                {
                    // If Enemy is poking, deal tower damage through poke
                    if(DecisionB == 2)
                    {
                        yield return new WaitForSeconds(TimeDelay);
                        TowerPoked(CharacterA,CharacterB,DamageB,StatusObjectA,TowerStatusObjectA);
                    }
                    yield return new WaitForSeconds(TimeDelay);
                    TakeDamage(CharacterA,CharacterB,DamageB,StatusObjectA,TowerStatusObjectA);
                    //check if player died and if enemy deals turret damage
                    if(CharacterA.Dead())
                    {
                        // Player died, give a kill to the Enemy
                        CharacterB.Kills += 1;
                        CharacterB.KilledEnemy(1);
                        StatusUpdate(CharacterB,StatusObjectB,TowerStatusObjectB);
                        StatusUpdate(CharacterA,StatusObjectA,TowerStatusObjectA);
                        yield return new WaitForSeconds(TimeDelay);
                        TakeTowerDamage(CharacterA,CharacterB,DamageB,StatusObjectA,TowerStatusObjectA);
                        // check for player tower health.
                        if(CharacterA.TowerTable[CharacterA.CurrentTower] <= 0)
                        {
                            CharacterA.CurrentTower++;
                            if(CharacterA.CurrentTower > 2)
                            {
                                GameOver = true;
                                CharacterA.CurrentTower = 2;
                                Winner = CharacterA == Player ? 1 : 0;
                            }
                            // Update CurrentPhase to match Player or Enemy Tower Phase
                            CurrentPhase = System.Math.Max(CharacterA.CurrentTower, CharacterB.CurrentTower);
                            StatusUpdate(CharacterA,StatusObjectA,TowerStatusObjectA);
                        }
                    }
                }
            }
            // increment TurnCounter  
            TurnCounter++;
            if(CharacterA.Dead())
            {
                CharacterA.CurrentHealth = CharacterStats.MaxHealth;
                StatusUpdate(CharacterA,StatusObjectA,TowerStatusObjectA);
            }
            if(CharacterB.Dead())
            {
                CharacterB.CurrentHealth = CharacterStats.MaxHealth;
                StatusUpdate(CharacterB,StatusObjectB,TowerStatusObjectB);
            }
        }
        // Game Over, show post match thread, close Dialog and bring up BattleOverScene. Reset Player's
        try 
        {
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

    private void TakeDamage(CharacterStats Receiver, CharacterStats Dealer, int Damage, GameObject StatusObject, GameObject TowerStatusObject)
    {
        // resolve damage dealt to the player
        Receiver.TakeDamage(Damage); 
        // Update Text on health status and on Battle Text.
        SendMessageToChat(Dealer.Name + " deals " + Damage.ToString() + " damage to " + Receiver.Name + ".");
        StatusUpdate(Receiver, StatusObject, TowerStatusObject);
    }

    private void TowerPoked(CharacterStats Receiver, CharacterStats Dealer, int Damage, GameObject StatusObject, GameObject TowerStatusObject)
    {
        // if the Enemy uses Poke damage, then the Turrets are damaged as well.(1-1 ratio)
        Receiver.LoseTowerHealth(Damage);
        SendMessageToChat(Dealer.Name + " deals " + Damage.ToString() + " damage to " + Receiver.Name + "'s tower through Poke.");
        StatusUpdate(Receiver, StatusObject, TowerStatusObject);
    }

    private void TakeTowerDamage(CharacterStats Receiver, CharacterStats Dealer, int Damage, GameObject StatusObject, GameObject TowerStatusObject)
    {
        // Player is Dead, Enemy deals Turret Damage
        int TowerDamage = Dealer.DealTowerDamage();
        Receiver.LoseTowerHealth(TowerDamage);
        SendMessageToChat(Dealer.Name + " deals " + TowerDamage.ToString() + " damage to " + Receiver.Name + "'s tower after " + Receiver.Name + " has died.");
        StatusUpdate(Receiver, StatusObject, TowerStatusObject);
    }
}
