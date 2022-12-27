using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatContinueScript : MonoBehaviour
{
    [SerializeField] GameObject CheckObject;
    [SerializeField] GameObject FailPopUpObject;
    private AllocationCheckScript CheckScript;
    private TransitionTextScript TransitionScript;

    // Load the scripts from their respective obj.
    void Start()
    {
        CheckScript = CheckObject.GetComponent<AllocationCheckScript>();
        TransitionScript = transform.GetComponent<TransitionTextScript>();
    }
    //checks if the CheckObject's avaliablePoints equals to 0 and then either closes the TransitionScript or puts the failure object.
    public void CloseDialog()
    {
        if (CheckScript.AvailiablePoints == 0)
        {
            TransitionScript.CloseDialog();
        }
        else
        {
            FailPopUpObject.SetActive(true);
        }
    }
}
