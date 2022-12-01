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
        Manager = GameObject.Find("UniversalGameManager");
        DS = Manager.GetComponent<DateSystem>();
        OnChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnChange()
    {
        DisplayText.text = DS.DateAsString();
    }
}
