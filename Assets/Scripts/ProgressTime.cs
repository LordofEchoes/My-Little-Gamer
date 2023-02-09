using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTime : MonoBehaviour
{

    [SerializeField] GameObject Manager;
    [SerializeField] DateSystem DS;
    [SerializeField] EnemyManager EM;

    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager");
            EM = Manager.GetComponent<GameManager>().GetEnemyManager();
        }
        catch(System.NullReferenceException err)
        {
            EM = new EnemyManagerBasic();
            Debug.Log("ProgressTime EnemyManager bugged: " + err.Message);
        }
        try
        {
            Manager = GameObject.Find("UniversalGameManager");
            DS = Manager.GetComponent<GameManager>().GetDateSystem();
        }
        catch(System.NullReferenceException err)
        {
            DS = new DateSystem();
            Debug.Log("ProgressTime DateSystem bugged: " + err.Message);
        }
    }

    public void PT()
    {
        DS.ProgressTime();
    }

    public void PTPostMatch()
    {
        EM.NextEnemy();
    }
}
