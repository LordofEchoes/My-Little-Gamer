using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameContinueScript : MonoBehaviour
{
    // Used in the Prologue Scene to close the Scene
    [SerializeField] GameObject CheckObject;
    [SerializeField] GameObject ConfirmObject;
    [SerializeField] GameObject FailPopUpObject;
    private CharacterStats Player;
    private SceneFinishScript TransitionScript;

    // Load the scripts from their respective obj.
    void Start()
    {
        try
        {
            Player = CheckObject.GetComponent<GameManager>().GetPlayer();
        }
        catch(System.NullReferenceException err)
        {
            Player = new CharacterStats();
            Player.BuildNew();
            Player.Name = "Player Dummy";
            Debug.Log("NameContinueScript Player bugged: " + err.Message);
        }

        try
        {
            TransitionScript = transform.GetComponent<SceneFinishScript>();
        }
        catch(System.NullReferenceException err)
        {
            Debug.Log("NameContinueScript TransitionScript bugged(no fix): " + err.Message);
        }
    }
    //checks if the CheckObject's Get Name equals to "" and then either closes the TransitionScript or puts the failure object.
    public void CloseDialog()
    {
        if (Player.Name == "")
        {
            FailPopUpObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            ConfirmObject.SetActive(true);
        }
    }

    private void ConfirmName()
    {
        TransitionScript.NextScene();
    }
}
