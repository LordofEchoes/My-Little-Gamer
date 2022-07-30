using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    public int entryDirection;
    public int exitDirection;
    public int newLocation;
    public float speed;
    public float delayVar;
    /*
    Script moves obj into the user's screen and then down when closeDialog is called.
    script uses entry and exit direction to determine direction
    */
    public void OnEnable()
    {
        switch(entryDirection)
        {
            case 1:
                obj.transform.localPosition = new Vector2(0, Screen.height);
                obj.transform.LeanMoveLocalY(newLocation,speed).setEaseOutExpo().delay = delayVar;      
                break;
            case 2:
                obj.transform.localPosition = new Vector2(0, -Screen.height);
                obj.transform.LeanMoveLocalY(newLocation,speed).setEaseOutExpo().delay = delayVar;      
                break;
            case 3:
                obj.transform.localPosition = new Vector2(-Screen.width, 0);
                obj.transform.LeanMoveLocalX(newLocation,speed).setEaseOutExpo().delay = delayVar;      
                break;
            case 4:
                obj.transform.localPosition = new Vector2(Screen.width, 0);
                obj.transform.LeanMoveLocalX(newLocation,speed).setEaseOutExpo().delay = delayVar;      
                break;
        }
          
    }

    public void CloseDialog()
    {
        switch(exitDirection)
        {
            case 1:
                obj.transform.LeanMoveLocalY(Screen.height,speed).setEaseInExpo().setOnComplete(OnComplete);
                break;
            case 2:
                obj.transform.LeanMoveLocalY(-Screen.height,speed).setEaseInExpo().setOnComplete(OnComplete);
                break;
            case 3:
                obj.transform.LeanMoveLocalX(-Screen.width,speed).setEaseInExpo().setOnComplete(OnComplete);
                break;
            case 4:
                obj.transform.LeanMoveLocalX(Screen.width,speed).setEaseInExpo().setOnComplete(OnComplete);
                break;
        }
        
    }

    public virtual void OnComplete()
    {
        obj.SetActive(false);
    }
}
