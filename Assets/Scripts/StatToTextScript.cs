using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatToTextScript : MonoBehaviour
{

    [SerializeField] GameObject obj;
    [SerializeField] PlayerStats charScript;
    public int key;
    public TextMeshProUGUI ValueText;
    [SerializeField] Stat stat;
    // Function to initialize the statTable and ping onChange() once to correctly display the text. 
    // Do this at the Start after everyting is Awake
    void Start()
    {
        charScript = obj.GetComponent<PlayerStats>();
        OnChange();
    }
    //called whenever a change to the stats occurs and the display needs to be updated.
    public void OnChange()
    {
        if (key == 6)
        {
            ValueText.text = charScript.gender;
            return;
        }
        else if (key <= 5 && key >= 0)
        {
            stat = charScript.statTable[key];
        }
        else
        {
            stat = new Stat();
        }
        ValueText.text = stat.GetValue().ToString();
        // Debug.Log("Key Value: " + key);
        // Debug.Log("Stat Value: " + stat.GetValue().ToString());
    }
}
