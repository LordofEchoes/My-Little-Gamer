using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDayCycle : MonoBehaviour
{
    GameObject Manager;
    DateSystem DS;
    [SerializeField] GameObject DisplayObject;
    [SerializeField] List<Sprite> Icons;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager");
            DS = Manager.GetComponent<GameManager>().GetDateSystem();
        }
        catch (System.Exception err)
        {
            DS = new DateSystem();
            Debug.Log($"Check Day Night bugged: {err}");
            throw;
        }
        Icons.Add(Resources.Load<Sprite>("Sprites/Sun"));
        Icons.Add(Resources.Load<Sprite>("Sprites/Moon"));
        CheckIcon();
    }

    // Update is called once per frame, only called while active
    public void CheckIcon()
    {
        DisplayObject.transform.GetComponent<Image>().sprite = Icons[DS.GetCycle()];
    }
}
