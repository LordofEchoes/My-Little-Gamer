using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaystyleDropdownScript : MonoBehaviour
{
    private GameObject CharacterObj = null;
    private CharacterStats Character;
    public int PhaseInt;
    public GameObject DropDownObject = null;
    private TMP_Dropdown m_Dropdown;
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
            Debug.Log("PlaystyleDropDown Player bugged: " + err.Message);
        }
        
        m_Dropdown = DropDownObject.GetComponent<TMP_Dropdown>();
        string NewTactic = Character.PhaseTable[PhaseInt].CurrentTactic;
        if (NewTactic == "Farm"){
            m_Dropdown.value = 0;
        }
        else if (NewTactic == "Poke"){
            m_Dropdown.value = 1;
        }
        else if (NewTactic == "Engage"){
            m_Dropdown.value = 2;
        }
        m_Dropdown.RefreshShownValue();
    }

    public void ChangeCurrentTactic()
    {
        Character.PhaseTable[PhaseInt].CurrentTactic = m_Dropdown.captionText.text;
        Debug.Log("Current Tactic is: "+ Character.PhaseTable[PhaseInt].CurrentTactic);
    }
}
