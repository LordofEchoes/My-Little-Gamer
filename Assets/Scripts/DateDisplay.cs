using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateDisplay : MonoBehaviour
{
    [SerializeField] GameObject Manager;
    [SerializeField] DateSystem DS;
    [SerializeField] TextMeshProUGUI DisplayText;
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
            Debug.Log("DateDisplay Date System bugged: " + err.Message);
        }
        OnChange();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText.text = DS.DateAsString();
    }

    void OnChange()
    {
        DisplayText.text = DS.DateAsString();
    }
}
