using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    
    
    public static PlayerStats instance;

    public override void Die()
    {
        Debug.Log("Player has died. Game over.");
    }
    

    // changes experience for the player's stats, modified by the happiness mod.
    private void ChangeStatExperience(string StatName, int amount)
    {
        // calculate base, 50% of all amount
        int baseXP = amount/2;
        // happiness is other 50%
        int happinessBonus = amount*StatTable["Happiness"].Value/MaxStat;
        StatTable[StatName].ChangeExperience(baseXP+happinessBonus);
    }


}
