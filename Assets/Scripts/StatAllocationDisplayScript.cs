using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatAllocationDisplayScript : MonoBehaviour
{
    public int pointsToAllocate = 6;
    public int availiablePoints;
    public TextMeshProUGUI ValueText;

    // Call onChange, after eveyrthing is Awake
    void Start()
    {
        availiablePoints = pointsToAllocate;
        OnChange();
    }
    // returns the difference of the statsToAllocate vs charScript's number of stats.
    // called as a precaution to check if the StatAllocation is Zero then the player cannot allocate any more stats.
    // called whenever the Stat needs to be updated
    public void OnChange()
    {
        ValueText.text = availiablePoints.ToString();
    }
}
