using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth {get;private set;}

    public Stat Happiness;
    public Stat Aggression;
    public Stat Focus;
    public Stat Decisions;
    public Stat Positioning;
    public Stat Mechanics;

    public void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Die in some way, overwritten
        Debug.Log(transform.name + " died.");
    }

}
