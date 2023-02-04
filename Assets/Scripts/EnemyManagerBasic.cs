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
    // Start is called before the first frame update
    public EnemyManagerBasic()
    {
        System.Random Generator = new System.Random();
        EnemyTable = new List<CharacterStats>();
        AddEnemy(new CharacterStats("Dummy1", "Male", 6));
        AddEnemy(new CharacterStats("Dummy2", "Female", Generator.Next(6,11)));
        AddEnemy(new CharacterStats("Dummy3", "Male", Generator.Next(6,11)));
        AddEnemy(new CharacterStats("Dummy4", "Female", Generator.Next(6,11)));
        AddEnemy(new CharacterStats("Dummy5", "Male", Generator.Next(6,11)));
        CurrentEnemyIndex = 0;
    }

    public override void AddEnemy(CharacterStats NewEnemy)
    {
        EnemyTable.Add(NewEnemy);
        NumberOfEnemies += 1;
    }

    public override CharacterStats GetCurrentEnemy()
    {
        Debug.Log($"EnemyTable Count: {EnemyTable.Count}\nCurrentIndex: {CurrentEnemyIndex}");
        if(EnemyTable.Count > CurrentEnemyIndex)
        {
            return EnemyTable[CurrentEnemyIndex];
        }
        return new CharacterStats();
    }

    public override void NextEnemy()
    {
        CurrentEnemyIndex += 1;
    }
}
