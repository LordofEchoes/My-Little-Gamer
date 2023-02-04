using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameDisplayScript : MonoBehaviour
{

    [SerializeField] GameObject NameObject = null;
    [SerializeField] CharacterStats NameScript;
    [SerializeField] int CharacterInt;
    public TextMeshProUGUI ValueText;
    
    // Start is called before the first frame update
    public void OnEnable()
    {
        try
        {
            NameObject = GameObject.Find("UniversalGameManager");
            if(CharacterInt == 0)
            {
                NameScript = NameObject.GetComponent<GameManager>().GetPlayer();
            }
            else
            {
                NameScript = NameObject.GetComponent<GameManager>().GetEnemyManager().GetCurrentEnemy();
            }
        }
        catch(System.NullReferenceException err)
        {
            NameScript = new CharacterStats();
            Debug.Log("NamesDisplayScript Player bugged: " + err.Message);
        }
        OnChange();
    }
    public void OnChange()
    {
        ValueText.text = NameScript.Name;
    }

}
