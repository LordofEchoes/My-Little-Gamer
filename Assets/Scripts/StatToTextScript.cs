using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatToTextScript : MonoBehaviour
{

    [SerializeField] GameObject Manager = null;
    [SerializeField] CharacterStats Character;
    public string key;
    public TextMeshProUGUI ValueText;
    [SerializeField] Stat _stat;
    // Function to initialize the statTable and ping onChange() once to correctly display the text. 
    // Do this at the Start after everyting is Awake
    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager");
            Character = Manager.GetComponent<GameManager>().GetPlayer();
        }
        catch(System.NullReferenceException err)
        {
            Character = new CharacterStats();
            Character.BuildNew();
            Debug.Log("StatToText Player bugged: " + err.Message);
        }
        OnChange();
    }
    //called whenever a change to the stats occurs and the display needs to be updated.
    public void OnChange()
    {
        if (key == "Gender")
        {
            ValueText.text = Character.Gender;
            return;
        }
        else if(Character.StatTable.ContainsKey(key))
        {
            _stat = Character.StatTable[key];
        }
        else
        {
            _stat = new Stat();
        }
        ValueText.text = _stat.Value.ToString();
    }
}
