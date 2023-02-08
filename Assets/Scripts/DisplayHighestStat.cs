using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHighestStat : MonoBehaviour
{

    GameObject Manager;
    CharacterStats Character;
    [SerializeField] int CharacterInt = 0;
    [SerializeField] TextMeshProUGUI DisplayText;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager");
            if(CharacterInt == 0)
            {
                Character = Manager.GetComponent<GameManager>().GetPlayer();
            }
            else
            {
                Character = Manager.GetComponent<GameManager>().GetEnemyManager().GetCurrentEnemy();
            }
        }
        catch(System.NullReferenceException err)
        {
            Character = new CharacterStats();
            Debug.Log("DisplayHighestStat Character bugged: " + err.Message);
        }
        OnEnable();
    }

    void OnEnable()
    {
        DisplayText.text = Character.GetHighestStat();
    }
}
