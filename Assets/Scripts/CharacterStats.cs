using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth {get;private set;}
    public int maxStat = 20;
    public string gender;
    public Stat happiness;
    public Stat aggression;
    public Stat focus;
    public Stat decisions;
    public Stat positioning;
    public Stat mechanics;

    public void Awake()
    {
        currentHealth = maxHealth;
        Stat happiness = new Stat();
        Stat aggression = new Stat();
        Stat focus = new Stat();
        Stat decisions = new Stat();
        Stat positioning = new Stat();
        Stat mechanics = new Stat();
    }
    public void assignGender(string str)
    {
        gender = str;
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

    public void modHappiness( int amount)
    {
        happiness += amount;
        if (happiness.GetValue() <= 0)
        {
            happiness += Math.Abs(happiness.GetValue()) + 1;
            Debug.Log(transform.name + " cannot have less than 0 happiness");
        }
        else if (happiness.GetValue() > maxStat)
        {
            happiness -= happiness - maxStat;
            Debug.Log(transform.name + " cannot have more than " + maxStat + " happiness");
        }
    }

    public void modAggression( int amount)
    {
        aggression += amount;
        if (aggression.GetValue() <= 0)
        {
            aggression += Math.Abs(aggression.GetValue()) + 1;
            Debug.Log(transform.name + " cannot have less than 0 aggression");
        }
        else if (aggression.GetValue() > maxStat)
        {
            aggression -= aggression - maxStat;
            Debug.Log(transform.name + " cannot have more than " + maxStat + " aggression");
        }
    }

    public void modFocus(int amount)
    {
        focus += amount;
        if (focus.GetValue() <= 0)
        {
            focus += Math.Abs(focus.GetValue()) + 1;
            Debug.Log(transform.name + " cannot have less than 0 focus");
        }
        else if (focus.GetValue() > maxStat)
        {
            focus -= focus - maxStat;
            Debug.Log(transform.name + " cannot have more than " + maxStat + " focus");
        }
    }

    public void modDecisions( int amount)
    {
        decisions += amount;
        if (decisions.GetValue() <= 0)
        {
            decisions += Math.Abs(decisions.GetValue()) + 1;
            Debug.Log(transform.name + " cannot have less than 0 decisions");
        }
        else if (decisions.GetValue() > maxStat)
        {
            decisions -= decisions - maxStat;
            Debug.Log(transform.name + " cannot have more than " + maxStat + " decisions");
        }
    }

    public void modPositioning( int amount)
    {
        positioning += amount;
        if (positioning.GetValue() <= 0)
        {
            positioning += Math.Abs(positioning.GetValue()) + 1;
            Debug.Log(transform.name + " cannot have less than 0 positioning");
        }
        else if (positioning.GetValue() > maxStat)
        {
            positioning -= positioning - maxStat;
            Debug.Log(transform.name + " cannot have more than " + maxStat + " positioning");
        }
    }

    public void modMechanics( int amount)
    {
        mechanics += amount;
        if (mechanics.GetValue() <= 0)
        {
            mechanics += Math.Abs(mechanics.GetValue()) + 1;
            Debug.Log(transform.name + " cannot have less than 0 mechanics");
        }
        else if (mechanics.GetValue() > maxStat)
        {
            mechanics -= mechanics - maxStat;
            Debug.Log(transform.name + " cannot have more than " + maxStat + " mechanics");
        }
    }


    public virtual void Die()
    {
        //Die in some way, overwritten
        Debug.Log(transform.name + " died.");
    }

}
