using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyManager
{
    public virtual int CurrentEnemyIndex{get;set;}
    public virtual CharacterStats GetCurrentEnemy()
    {
        Debug.Log($"EnemyManager GetCurrentEnemy() called: needs to be overwritten");
        return new CharacterStats();
    }
    public virtual void AddEnemy(CharacterStats NewEnemy)
    {
        Debug.Log($"EnemyManager AddEnemy() called: needs to be overwritten");
        return;
    }
    public virtual void NextEnemy()
    {
        Debug.Log($"EnemyManager NextEnemy() called: needs to be overwritten");
        return;
    }
}
