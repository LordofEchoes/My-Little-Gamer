using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLeagueMatch : MonoBehaviour
{
    [SerializeField] GameObject Manager;
    [SerializeField] DateSystem DS;
    [SerializeField] GameObject MatchObject;
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("UniversalGameManager");
        DS = Manager.GetComponent<DateSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DS.CanTrain())
        {
            MatchObject.SetActive(true);
        }
        else
        {
            MatchObject.SetActive(false);
        }
    }
}
