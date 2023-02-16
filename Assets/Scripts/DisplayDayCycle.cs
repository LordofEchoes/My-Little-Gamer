using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDayCycle : MonoBehaviour
{
    GameObject Manager;
    DateSystem DS;
    public int CycleNumber;
    [SerializeField] GameObject Icon;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager");
            DS = Manager.GetComponent<GameManager>().GetDateSystem();
        }
        catch (System.Exception err)
        {
            DS = new DateSystem();
            Debug.Log($"Check Day Night bugged: {err}");
            throw;
        }
        
    }

    // Update is called once per frame, only called while active
    void Update()
    {
        if(DS.GetCycle() == CycleNumber)
        {
            Icon.SetActive(true);
        }
        else
        {
            Icon.SetActive(false);
        }
    }
}
