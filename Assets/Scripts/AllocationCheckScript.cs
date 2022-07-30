using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllocationCheckScript : MonoBehaviour
{
    [SerializeField] GameObject alloObj;
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject errorDisplayObj;
    private AllocationDisplayScript alloScript;
    private CharacterStats playerScript;
    public TextMeshProUGUI ValueText;

    // Start is called before the first frame update
    // Takes the allocation and player objects and loads the scripts
    void Start()
    {
        alloScript = alloObj.GetComponent<AllocationDisplayScript>();
        playerScript = playerObj.GetComponent<CharacterStats>();
    }
    
    // Checks the allocation scripts and performs checks to see if there are enough allocation points
    // performs the player functions and allocation display functions if necessary.
    public void OnChange(int index)
    {
        if (alloScript.availiablePoints - playerScript.GetAmount()  > alloScript.pointsToAllocate || alloScript.availiablePoints - playerScript.GetAmount()  < 0)
        {
            ValueText.text = ("Cannot have less than 0 availiable points");
            errorDisplayObj.SetActive(true);
            StartCoroutine(WaitForError());
            return;
        }
        else if (playerScript.statTable[index].GetValue() + playerScript.GetAmount() < 1)
        {
            ValueText.text = ("Cannot have less than 1 of a player stat");
            errorDisplayObj.SetActive(true);
            StartCoroutine(WaitForError());
            
            return;
        }
        playerScript.ModStat(index, playerScript.GetAmount());
        alloScript.availiablePoints -= playerScript.GetAmount();
        alloScript.OnChange();
    }

    IEnumerator WaitForError()
    {
        yield return new WaitForSeconds (2);
        errorDisplayObj.GetComponent<PopUpScript>().CloseDialog();
    }

}
