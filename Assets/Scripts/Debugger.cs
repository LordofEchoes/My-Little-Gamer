using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    // create a gameManager if One Cannot be found
    void Awake()
    {
        if(!GameObject.Find("UniversalGameManager"))
        {
            // error Here!
            GameObject GM = new GameObject("UniversalGameManager");
            GM.AddComponent<GameManager>();
        }   
    }
}
 