using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayEnemyDate : MonoBehaviour
{

    [SerializeField] GameObject Manager;
    [SerializeField] System.DateTime DT;
    [SerializeField] TextMeshProUGUI DisplayText;

    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager");
            DT = Manager.GetComponent<GameManager>().GetEnemyManager().GetCurrentEnemy().GetDate();
        }
        catch(System.NullReferenceException err)
        {
            DT = new System.DateTime(2023,8,4);
            Debug.Log("DisplayEnemyDate Date bugged: " + err.Message);
        }
        OnChange();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText.text = DT.ToString("MM/dd/yyyy");
    }

    void OnChange()
    {
        DisplayText.text = DT.ToString("MM/dd/yyyy");
    }
}
