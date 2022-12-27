using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextDay : MonoBehaviour
{
    [SerializeField] GameObject Manager;
    private PlayerStats Player;
    [SerializeField] DateSystem DS;
    [SerializeField] Button NextDayButton;

    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("UniversalGameManager");
        Player = Manager.GetComponent<PlayerStats>();
        DS = Manager.GetComponent<DateSystem>();
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
}
