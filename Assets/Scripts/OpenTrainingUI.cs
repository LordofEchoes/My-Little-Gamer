using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenTrainingUI : MonoBehaviour
{
    [SerializeField] GameObject Manager;
    [SerializeField] DateSystem DS;
    [SerializeField] Button TrainingButton;
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
        if (DS.CanTrain())
        {
            TrainingButton.interactable = true;
        }
        else
        {
            TrainingButton.interactable = false;
        }
    }

}
