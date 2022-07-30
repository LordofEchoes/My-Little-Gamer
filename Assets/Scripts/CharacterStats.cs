using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterStats : MonoBehaviour
{
    [SerializeField] string characterName;
    public int maxHealth = 100;
    public int currentHealth {get;private set;}
    public int maxStat = 20;
    public int statNum = 6;
    public string gender;
    public int statSum;
    public Stat[] statTable;

    // Stat order in respect to index
    // 0 happiness
    // 1 aggression
    // 2 focus
    // 3 decisions
    // 4 positioning
    // 5 mechanics

    // Setter and Getter for chracter Name variable
    public void SetName(string name)
    {
        characterName = name;
    }

    public string GetName()
    {
        return characterName;
    }
    
    public int GetStatSum()
    {
        return statSum;
    }

    // Initializer used when the object awakens in Unity.
    public void Awake()
    {
        currentHealth = maxHealth;
        SetName("");
        for (int i = 0; i < statNum; i++)
        {
            statTable[i] = new Stat();
        }
        statSum = 6;
    }

    // Assigns the gender to the class
    public void AssignGender(string str)
    {
        gender = str;
    }
    // decreases current health by the damage parameter
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // changes the stat corresponding to the index parameter within the statTable by the amount parameter 
    // adjusts itself for max and min values(maxStat and 0)
    public void ModStat(int index, int amount)
    {
        statSum += amount;
        statTable[index] += amount;
        if (statTable[index].GetValue() <= 0)
        {
            statSum += Math.Abs(statTable[index].GetValue())+1;
            statTable[index] += Math.Abs(statTable[index].GetValue()) + 1;
            Debug.Log(transform.name + " cannot have less than 0 happiness");
        }
        else if (statTable[index].GetValue() > maxStat)
        {
            statSum -= statTable[index].GetValue() - maxStat;
            statTable[index] -= statTable[index].GetValue() - maxStat;
            Debug.Log(transform.name + " cannot have stats more than " + maxStat);
        }
    }


    public virtual void Die()
    {
        //Die in some way, overwritten
        Debug.Log(gameObject.name + " died.");
    }

    public virtual int GetAmount()
    {
        return 0;
    }
    public virtual void SetAmount(int amount)
    {

    }


}
