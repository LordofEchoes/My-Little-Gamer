using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TacticDropdownDisplay : MonoBehaviour
{
    // This is the Player Stats
    [SerializeField] GameObject CharacterObj = null;
    [SerializeField] CharacterStats Character;
    public TextMeshProUGUI ValueText;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            CharacterObj = GameObject.Find("UniversalGameManager");
            Character = CharacterObj.GetComponent<GameManager>().GetPlayer();
        }
        catch(System.NullReferenceException err)
        {
            Character = new CharacterStats();
            Character.BuildNew();
            Debug.Log("TacticDropdown Player bugged: " + err.Message);
        }
        
    }
    // Occurs when change.
    public void OnChange()
    {
        ValueText.text = Character.Name;
    }
}
