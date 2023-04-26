using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tutorial Scene moved to the start of the game. 
public class TutorialScreen : MonoBehaviour
{
    GameManager Manager;
    [SerializeField] GameObject TS;
    // Override is the screen that gets replaced by the tutorial, the end of the tutorial returns to the Override.      
    // [SerializeField] GameObject Override;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager").GetComponent<GameManager>();
        }
        catch (System.Exception)
        {
            Debug.Log("TutorialScreen not working");
            throw;
        }
        CheckTutorial();
    }

    // Update only called once the parent is active, thus TS is the object that we will activate and deactivate.
    // Not completely necessary to use Update, only when the scene first appears.
    void Update()
    {
        
    }

    public void CheckTutorial()
    {
        if(Manager.Tutorial == 0)
        {
            Debug.Log($"Tutorial Screen on: {Manager.Tutorial}");
            TS.SetActive(true);
            // if(Override != null)
            // {
            //     Override.SetActive(false);
            // }
        }
        else
        {
            Debug.Log($"Tutorial Screen off: {Manager.Tutorial}");
            TS.SetActive(false);
            // if(Override != null)
            // {
            //     Override.SetActive(true);
            // }
        }
    }
    public void TutorialFinish()
    {
        Manager.Tutorial = 1;
    }
}
