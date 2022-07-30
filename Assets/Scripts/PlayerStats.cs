using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // amount to change a stat by
    [SerializeField] int amount = 0;

    public override void SetAmount(int value)
    {
        amount = value;
    }
    
    public override int GetAmount()
    {
        return amount;
    }
}
