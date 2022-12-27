using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tactic
{
    // Variables:
    private int _Proficiency;
    // getter/Setter for Proficiency
    public int Proficiency
    {
        get {return (int)System.Math.Truncate((double)_Proficiency/10);}
        set {_Proficiency = value;}
    }
    public int maxProficiency = 1000;
    public string Name {get; set;}
    // Constructors for Tactic
    public Tactic()
    {
        _Proficiency = 0;
        Name = "No Name";
    }

    public Tactic(string PlayStyleName)
    {
        _Proficiency = 0;
        Name = PlayStyleName;
    }

    public Tactic( string PlayStyleName, int startingProficiency)
    {
        _Proficiency = startingProficiency;
        Name = PlayStyleName;
    }

    // change proficiency value
    public void ChangeProficiency(int amount)
    {
        if(_Proficiency + amount > maxProficiency)
        {
            _Proficiency = maxProficiency;
        }
        else if(_Proficiency + amount < 0)
        {
            _Proficiency = 0;
        }
        else
        {
            _Proficiency += amount;
        }
    }

}
