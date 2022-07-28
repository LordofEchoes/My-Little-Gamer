using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllocationCheckScript : MonoBehaviour
{
    [SerializeField] GameObject alloObj;
    [SerializeField] GameObject playerObj;
    [SerializeField] StatAllocationDisplayScript alloScript;
    [SerializeField] PlayerStats playerScript;

    // Start is called before the first frame update
    // Takes the allocation and player objects and loads the scripts
    void Start()
    {
        alloScript = alloObj.GetComponent<StatAllocationDisplayScript>();
        playerScript = playerObj.GetComponent<PlayerStats>();
    }
    
    // Checks the allocation scripts and performs checks to see if there are enough allocation points
    // performs the player functions and allocation display functions if necessary.
    public void OnChange(int index)
    {
        if (alloScript.availiablePoints - playerScript.amount > alloScript.pointsToAllocate || alloScript.availiablePoints - playerScript.amount < 0)
        {
            Debug.Log("Cannot have less than 0 or more than 6 Avaliable Points");
            return;
        }
        else if (playerScript.statTable[index].GetValue() + playerScript.amount < 1)
        {
            Debug.Log("Cannot have less than 1 of a player Stat");
            return;
        }
        playerScript.ModStat(index, playerScript.amount);
        alloScript.availiablePoints -= playerScript.amount;
        alloScript.OnChange();
    }
}
