using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProficiencyDisplayScript : MonoBehaviour
{
    [SerializeField] GameObject CharacterObj = null;
    [SerializeField] CharacterStats CharacterScript;
    [SerializeField] string PhaseName = "";
    private string TacticName = "";
    public TextMeshProUGUI DisplayText;

    // On start of the display script
    public void OnEnable()
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
        TacticName = CharacterScript.PhaseTable[PhaseName].CurrentTactic;
        OnChange();
    }
    // Update proficiency when the dropdown is changed
    public void UpdateProficiency()
    {
        TacticName = CharacterScript.PhaseTable[PhaseName].CurrentTactic;
        OnChange();
    }
    public void OnChange()
    {
        DisplayText.text = CharacterScript.PhaseTable[PhaseName].TacticTable[TacticName].Proficiency.ToString() + "%";
    }

}
