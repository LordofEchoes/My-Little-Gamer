using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameDisplayScript : MonoBehaviour
{

    [SerializeField] GameObject NameObject = null;
    [SerializeField] CharacterStats NameScript;
    public TextMeshProUGUI ValueText;
    // Start is called before the first frame update
    public void OnEnable()
    {
        if (NameObject != null)
        {
            NameScript = NameObject.GetComponent<CharacterStats>();
        }
        else
        {
            NameObject = GameObject.Find("UniversalGameManager");
            NameScript = NameObject.GetComponent<CharacterStats>();
        }
        OnChange();
    }
    public void OnChange()
    {
        ValueText.text = NameScript.Name;
    }

}
