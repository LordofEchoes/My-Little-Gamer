using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckActionUI : MonoBehaviour
{
    private GameObject Manager;
    private DateSystem DS;
    [SerializeField] GameObject ActionUIParent;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager");
            DS = Manager.GetComponent<GameManager>().GetDateSystem();
        }
        catch (System.NullReferenceException err)
        {
            DS = new DateSystem();
            Debug.Log("OpenTrainingUi DateSystem bugged: " + err.Message);
        }
        
    }

    void Update()
    {
        // can train if cycle is morning
        if (DS.GetCycle() == 0 && DS.GetDayOfWeek() != 0)
        {
            ActionUIParent.transform.GetChild(0).GetComponent<Button>().interactable = true;
            ActionUIParent.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }
        else
        {
            ActionUIParent.transform.GetChild(0).GetComponent<Button>().interactable = false;
            ActionUIParent.transform.GetChild(1).GetComponent<Button>().interactable = false;
        }
    }

}
