using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TacticDropdownDisplay : MonoBehaviour
{
    // This is the Player Stats
    [SerializeField] GameObject CharacterObj = null;
    [SerializeField] CharacterStats CharacterScript;
    public TextMeshProUGUI ValueText;
    // Start is called before the first frame update
    void Start()
    {
        if (CharacterObj != null)
        {
            CharacterScript = CharacterObj.GetComponent<CharacterStats>();
        }
        else
        {
            CharacterObj = GameObject.Find("UniversalGameManager");
            CharacterScript = CharacterObj.GetComponent<CharacterStats>();
        }
        
    }
    // Occurs when change.
    public void OnChange()
    {
        ValueText.text = CharacterScript.Name;
    }
}
