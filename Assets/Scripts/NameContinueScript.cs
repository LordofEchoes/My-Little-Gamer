using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameContinueScript : MonoBehaviour
{
    // Used in the Prologue Scene to close the Scene
    [SerializeField] GameObject CheckObject;
    [SerializeField] GameObject ConfirmObject;
    [SerializeField] GameObject FailPopUpObject;
    private CharacterStats CheckScript;
    private SceneFinishScript TransitionScript;

    // Load the scripts from their respective obj.
    void Start()
    {
        CheckScript = CheckObject.GetComponent<CharacterStats>();
        TransitionScript = transform.GetComponent<SceneFinishScript>();
    }
    //checks if the CheckObject's Get Name equals to "" and then either closes the TransitionScript or puts the failure object.
    public void CloseDialog()
    {
        if (CheckScript.Name == "")
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
