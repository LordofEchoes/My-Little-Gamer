using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    // Start is called before the first frame update
    // Object is the Object that will be popped up
    // entry and exit Direction is the 4 nautical directionals they enter/exit from:
    // 1: North
    // 2: South
    // 3: West
    // 4: East
    // Speed is the Speed of the object
    // delay var is the delay before the object appears.
    public GameObject Object;
    public int EntryDirection;
    public int ExitDirection;
    public int NewLocation;
    public float Speed;
    public float DelayVar;
    /*
    Script moves Object into the user's screen and then down when closeDialog is called.
    script uses entry and exit direction to determine direction
    */
    public void OnEnable()
    {
        switch(EntryDirection)
        {
            case 1:
                Object.transform.localPosition = new Vector2(0, Screen.height);
                Object.transform.LeanMoveLocalY(NewLocation,Speed).setEaseOutExpo().delay = DelayVar;      
                break;
            case 2:
                Object.transform.localPosition = new Vector2(0, -Screen.height);
                Object.transform.LeanMoveLocalY(NewLocation,Speed).setEaseOutExpo().delay = DelayVar;      
                break;
            case 3:
                Object.transform.localPosition = new Vector2(-Screen.width, 0);
                Object.transform.LeanMoveLocalX(NewLocation,Speed).setEaseOutExpo().delay = DelayVar;      
                break;
            case 4:
                Object.transform.localPosition = new Vector2(Screen.width, 0);
                Object.transform.LeanMoveLocalX(NewLocation,Speed).setEaseOutExpo().delay = DelayVar;      
                break;
        }
          
    }

    public void CloseDialog()
    {
        switch(ExitDirection)
        {
            case 1:
                Object.transform.LeanMoveLocalY(Screen.height,Speed).setEaseInExpo().setOnComplete(OnComplete);
                break;
            case 2:
                Object.transform.LeanMoveLocalY(-Screen.height,Speed).setEaseInExpo().setOnComplete(OnComplete);
                break;
            case 3:
                Object.transform.LeanMoveLocalX(-Screen.width,Speed).setEaseInExpo().setOnComplete(OnComplete);
                break;
            case 4:
                Object.transform.LeanMoveLocalX(Screen.width,Speed).setEaseInExpo().setOnComplete(OnComplete);
                break;
        }
        
    }

    public virtual void OnComplete()
    {
        Object.SetActive(false);
    }
}
