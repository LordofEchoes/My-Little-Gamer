using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllocationCheckScript : MonoBehaviour
{
    [SerializeField] GameObject AlloObject;
    [SerializeField] GameObject PlayerObj;
    [SerializeField] GameObject ErrorDisplayObject;
    private AllocationDisplayScript AlloScript;
    private CharacterStats Player;
    public int PointsToAllocate;
    public int AvailiablePoints;
    public TextMeshProUGUI ValueText;

    //Awake make avaliable points so display can allocate it.
    void Awake()
    {
        AvailiablePoints = PointsToAllocate;
    }

    // Start is called before the first frame update
    // Takes the allocation and player objects and loads the scripts
    void Start()
    {
        AlloScript = AlloObject.GetComponent<AllocationDisplayScript>();
        Player = PlayerObj.GetComponent<GameManager>().GetPlayer();
    }
    
    // Checks the allocation scripts and performs checks to see if there are enough allocation points
    // performs the player functions and allocation display functions if necessary.
    // uses get Player Stats GetAmount() to get the amount that the player script is changing by
    public void OnChange(string StatName)
    {
        if (AvailiablePoints - Player.Amount > PointsToAllocate)
        {
            ValueText.text = ("Cannot have more than " + PointsToAllocate.ToString() + " availiable points.");
            ErrorDisplayObject.SetActive(true);
            StartCoroutine(WaitForError());
            return;
        }
        else if (AvailiablePoints - Player.Amount  < 0)
        {
            ValueText.text = ("Cannot have less than 0 availiable points.");
            ErrorDisplayObject.SetActive(true);
            StartCoroutine(WaitForError());
            return;
        }
        else if (Player.StatTable[StatName].Value + Player.Amount < 1)
        {
            ValueText.text = ("Cannot have less than 1 of a player stat.");
            ErrorDisplayObject.SetActive(true);
            StartCoroutine(WaitForError());
            return;
        }
        Player.ModStat(StatName, Player.Amount);
        AvailiablePoints -= Player.Amount;
        AlloScript.OnChange();
    }

    IEnumerator WaitForError()
    {
        yield return new WaitForSeconds (2);
        ErrorDisplayObject.GetComponent<PopUpScript>().CloseDialog();
    }

}
