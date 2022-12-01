using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllocationDisplayScript : MonoBehaviour
{
    [SerializeField] GameObject AlloObject;
    private AllocationCheckScript AlloScript;
    public TextMeshProUGUI ValueText;

    // Call onChange, after everything is Awake
    void Start()
    {
        AlloScript = AlloObject.GetComponent<AllocationCheckScript>();
        OnChange();
    }

    // returns the difference of the statsToAllocate vs charScript's number of stats.
    // called as a precaution to check if the StatAllocation is Zero then the player cannot allocate any more stats.
    // called whenever the Stat needs to be updated
    public void OnChange()
    {
        ValueText.text = AlloScript.AvailiablePoints.ToString();
    }
}
