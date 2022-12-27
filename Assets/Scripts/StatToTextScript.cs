using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatToTextScript : MonoBehaviour
{

    [SerializeField] GameObject Manager = null;
    [SerializeField] CharacterStats CharScript;
    public string key;
    public TextMeshProUGUI ValueText;
    [SerializeField] Stat _stat;
    // Function to initialize the statTable and ping onChange() once to correctly display the text. 
    // Do this at the Start after everyting is Awake
    void Start()
    {
        if (Manager != null)
        {
            CharScript = Manager.GetComponent<CharacterStats>();
        }
        else
        {
            Manager = GameObject.Find("UniversalGameManager");
            CharScript = Manager.GetComponent<CharacterStats>();
        }
        OnChange();
    }
    //called whenever a change to the stats occurs and the display needs to be updated.
    public void OnChange()
    {
        if (key == "Gender")
        {
            ValueText.text = CharScript.Gender;
            return;
        }
        else if(CharScript.StatTable.ContainsKey(key))
        {
            _stat = CharScript.StatTable[key];
        }
        else
        {
            _stat = new Stat();
        }
        ValueText.text = _stat.Value.ToString();
    }
}
