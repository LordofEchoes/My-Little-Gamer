using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProficiencyDisplayScript : MonoBehaviour
{
    [SerializeField] GameObject CharacterObj = null;
    [SerializeField] CharacterStats Character;
    [SerializeField] int PhaseInt;
    private string TacticName = "";
    public TextMeshProUGUI DisplayText;

    // On start of the display script
    public void OnEnable()
    {
        try
        {
            CharacterObj = GameObject.Find("UniversalGameManager");
            Character = CharacterObj.GetComponent<GameManager>().GetPlayer();
        }
        catch(System.NullReferenceException err)
        {
            Character = new CharacterStats();
            Debug.Log("ProficiencyDisplay Player bugged: " + err.Message);
        }
        TacticName = Character.PhaseTable[PhaseInt].CurrentTactic;
        OnChange();
    }
    // Update proficiency when the dropdown is changed
    public void UpdateProficiency()
    {
        TacticName = Character.PhaseTable[PhaseInt].CurrentTactic;
        OnChange();
    }
    public void OnChange()
    {
        DisplayText.text = Character.PhaseTable[PhaseInt][TacticName].Proficiency.ToString() + "%";
    }

}
