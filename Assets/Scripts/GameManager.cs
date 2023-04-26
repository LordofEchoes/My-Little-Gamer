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
        EventManagerPath = Resources.Load<TextAsset>("Data/EventMetaData/MetaData");
        Events = new EventManager(EventManagerPath.text);
        DS = new DateSystem();
        FL = new FriendList();
        GenerateEvent();
        Player = new CharacterStats();
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
        Debug.Log(Events);
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

    public void GenerateEvent(int n = -1)
    {
        // generate event, receive list of potential friends
        List<string> friends = Events.GenerateEvent(n);
        // add friends to friends list
        foreach(var name in friends)
        {
            // name not in Friends List, add them in
            if(!FL.ContainsKey(name))
            {
                FL.AddFriend(name, $"Data/Friends/{name}");
            }
        }
    }

}
