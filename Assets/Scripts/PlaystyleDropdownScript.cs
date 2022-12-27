using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaystyleDropdownScript : MonoBehaviour
{
    private GameObject CharacterObj = null;
    private CharacterStats CharacterScript;
    public string PhaseName = "";
    public GameObject DropDownObject = null;
    private TMP_Dropdown m_Dropdown;
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
        m_Dropdown = DropDownObject.GetComponent<TMP_Dropdown>();
        string NewTactic = CharacterScript.PhaseTable[PhaseName].CurrentTactic;
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
        CharacterScript.PhaseTable[PhaseName].CurrentTactic = m_Dropdown.captionText.text;
        Debug.Log("Current Tactic is: "+ CharacterScript.PhaseTable[PhaseName].CurrentTactic);
    }
}
