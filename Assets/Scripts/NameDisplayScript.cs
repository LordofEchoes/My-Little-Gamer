using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameDisplayScript : MonoBehaviour
{

    [SerializeField] GameObject nameObj;
    [SerializeField] CharacterStats nameScript;
    public TextMeshProUGUI ValueText;
    // Start is called before the first frame update
    public void OnEnable()
    {
        nameScript = nameObj.GetComponent<CharacterStats>();
        OnChange();
    }
    public void OnChange()
    {
        ValueText.text = nameScript.GetName();
    }

}
