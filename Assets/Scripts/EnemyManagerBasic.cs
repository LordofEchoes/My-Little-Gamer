using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This is a class that stores a table for the GameManager. It tracks all of the Enemies.
// should use MySQL later;
[System.Serializable]
public class EnemyManagerBasic : EnemyManager
{
    public List<CharacterStats> EnemyTable;
    public override int CurrentEnemyIndex{get;set;}
    private int NumberOfEnemies = 0;
    // Current Week should be identical to enemy index
    private int CurrentWeek;

    // Start is called before the first frame update
    public EnemyManagerBasic()
    {
        System.Random Generator = new System.Random();
        EnemyTable = new List<CharacterStats>();
        AddEnemy(new EnemyStats(new System.DateTime(2023,8,4),1,"Dummy1", "Male", 6));
        AddEnemy(new EnemyStats(new System.DateTime(2023,8,11),2,"Dummy2", "Female", Generator.Next(6,11)));
        AddEnemy(new EnemyStats(new System.DateTime(2023,8,18),3,"Dummy3", "Male", Generator.Next(6,11)));
        AddEnemy(new EnemyStats(new System.DateTime(2023,8,25),4,"Dummy4", "Female", Generator.Next(6,11)));
        AddEnemy(new EnemyStats(new System.DateTime(2023,9,2),5,"Dummy5", "Male", Generator.Next(6,11)));
        CurrentEnemyIndex = 0;
        CurrentWeek = CurrentEnemyIndex;
    }

    // Adds enemy to the table, override from EnemyManager
    public override void AddEnemy(CharacterStats NewEnemy)
    {
        EnemyTable.Add(NewEnemy);
        NumberOfEnemies += 1;
    }

    // Returns the current enemy that the player should be fighting against
    public override CharacterStats GetCurrentEnemy()
    {
        Debug.Log($"EnemyTable Count: {EnemyTable.Count}\nCurrentIndex: {CurrentEnemyIndex}");
        if(EnemyTable.Count > CurrentEnemyIndex)
        {
            return EnemyTable[CurrentEnemyIndex];
        }
        Debug.Log($"Index Out of Bounds, generating new Enemy");
        return new CharacterStats();
    }

    // moves the index to the next enemy
    public override void NextEnemy()
    {
        CurrentEnemyIndex += 1;
        CurrentWeek += 1;
    }
}
