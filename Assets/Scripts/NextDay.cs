using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextDay : MonoBehaviour
{
    [SerializeField] GameObject Manager;
    [SerializeField] DateSystem DS;
    [SerializeField] Button NextDayButton;
    [SerializeField] GameObject BattleScreen;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager");
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
        // if week is true then we want to 
        if(DS.CheckWeek())
        {
            NextDayButton.interactable = true;
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next Week";
        }
        else if (DS.CheckDay())
        {
            NextDayButton.interactable = true;
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next Day";
        }
        else if (NextDayButton.interactable == true && DS.CheckDay() == false)
        {
            NextDayButton.interactable = false;
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next Day";
        }
        if(DS.CheckBattle())
        {
            BattleScreen.SetActive(true);
        }
        else
        {
            BattleScreen.SetActive(false);
        }
    }
}
