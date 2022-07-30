using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameContinueScript : MonoBehaviour
{
    // Used in the Prologue Scene to close the Scene
    [SerializeField] GameObject checkObj;
    [SerializeField] GameObject confirmObj;
    [SerializeField] GameObject failPopUpObj;
    private CharacterStats checkScript;
    private SceneFinishScript transitionScript;

    // Load the scripts from their respective obj.
    void Start()
    {
        checkScript = checkObj.GetComponent<CharacterStats>();
        transitionScript = transform.GetComponent<SceneFinishScript>();
    }
    //checks if the checkObj's Get Name equals to "" and then either closes the transitionScript or puts the failure object.
    public void CloseDialog()
    {
        if (checkScript.GetName() == "")
        {
            failPopUpObj.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            confirmObj.SetActive(true);
        }
    }

    private void ConfirmName()
    {
        transitionScript.NextScene();
    }
}
