using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // amount to change a stat by
    public int amount = 0;

    public void SetAmount(int value)
    {
        amount = value;
    }
    
}
