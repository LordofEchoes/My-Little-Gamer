using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextDay : MonoBehaviour
{
    [SerializeField] GameObject Manager;
    private CharacterStats Player;
    [SerializeField] DateSystem DS;
    [SerializeField] Button NextDayButton;
    [SerializeField] GameObject BattleScreen;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager");
            Player = Manager.GetComponent<GameManager>().GetPlayer();
            
        }
        catch(System.NullReferenceException err)
        {
            Player = new CharacterStats();
            Player.BuildNew();
            DS = new DateSystem();
            Debug.Log("NextDay Player bugged: " + err.Message);
        }

        try
        {
            DS = Manager.GetComponent<GameManager>().GetDateSystem();
        }
        catch(System.NullReferenceException err)
        {
            DS = new DateSystem();
            Debug.Log("NextDay DateSystem bugged: " + err.Message);
        }
        
    }

    // Update is called once per frame
    // check if the next day button should be set active or deactivated.
    void Update()
    {
        if (NextDayButton.interactable == false && DS.CheckNextDay())
        {
            NextDayButton.interactable = true;
        }
        else if (NextDayButton.interactable == true && DS.CheckNextDay() == false)
        {
            NextDayButton.interactable = false;
        }
        
    }

    public void ProgressDay()
    {
        DS.ProgressDay();
        if(DS.CheckWeek())
        {
            BattleScreen.SetActive(true);
        }
        else
        {
            BattleScreen.SetActive(false);
        }
    }
}
