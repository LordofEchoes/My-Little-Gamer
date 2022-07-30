using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatContinueScript : MonoBehaviour
{
    [SerializeField] GameObject checkObj;
    [SerializeField] GameObject failPopUpObj;
    private AllocationDisplayScript checkScript;
    private TransitionTextScript transitionScript;

    // Load the scripts from their respective obj.
    void Start()
    {
        checkScript = checkObj.GetComponent<AllocationDisplayScript>();
        transitionScript = transform.GetComponent<TransitionTextScript>();
    }
    //checks if the checkObj's avaliablePoints equals to 0 and then either closes the transitionScript or puts the failure object.
    public void CloseDialog()
    {
        if (checkScript.availiablePoints == 0)
        {
            transitionScript.CloseDialog();
        }
        else
        {
            failPopUpObj.SetActive(true);
        }
    }
}
