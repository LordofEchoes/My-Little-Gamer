using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager script holds the EnemyManager
    [SerializeField] EnemyManager EM;
    [SerializeField] DateSystem DS;
    [SerializeField] CharacterStats Player;
    [SerializeField] FriendList FL;
    public int Tutorial {get;set;}
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        EM = new EnemyManagerBasic();
        DS = new DateSystem();
        FL = new FriendList();
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
