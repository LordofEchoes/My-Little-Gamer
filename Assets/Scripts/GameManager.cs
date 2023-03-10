using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager script holds the EnemyManager
    [SerializeField] EnemyManager EM;
    [SerializeField] EventManager Events;
    [SerializeField] DateSystem DS;
    [SerializeField] CharacterStats Player;
    [SerializeField] FriendList FL;
    [SerializeField] TextAsset EventManagerPath;
    public int Tutorial {get;set;}
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        EM = new EnemyManagerBasic();
        Events = new EventManager(EventManagerPath.text);
        DS = new DateSystem();
        FL = new FriendList();
        FL.AddFriend("Wes",0);
        FL.AddFriend("Jim",1);
        FL.AddFriend("Bob",2);
        FL.AddFriend("Sal",3);
        FL.AddFriend("Pal",4);
        FL.AddFriend("Amy",5);
        Player = new CharacterStats();
        Player.ChangeTacticProficiency(1,"Poke", 100);
        Player.PhaseTable[1].ChangeTactic("Poke");
    }

    public DateSystem GetDateSystem()
    {
        return DS;
    }

    public EnemyManager GetEnemyManager()
    {
        // Debug.Log($"Return EnemyManager");
        return EM;
    }

    public CharacterStats GetPlayer()
    {
        return Player;
    }

    public FriendList GetFriendList()
    {
        return FL;
    }

    public EventManager GetEventManager()
    {
        return Events;
    }
    // gender should be "Male" or "Female"
    public void SetGender(string gender)
    {
        Player.Gender = gender;
        Debug.Log($"Gender Assigned to {Player.Name}: {Player.Gender}");
    }

    public void SetName(string value)
    {
        Debug.Log($"Name Assigned to {Player.Name}: {value}");
        Player.Name = value;
    }

    public void SetAmount(int amount)
    {
        Player.Amount = amount;
    }

}
