using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayFriendList : MonoBehaviour
{
    [SerializeField] GameObject Manager;
    [SerializeField] FriendList FL;
    [SerializeField] GameObject Panel,FriendObject;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager");
            FL = Manager.GetComponent<GameManager>().GetFriendList();
        }
        catch(System.NullReferenceException err)
        {
            FL = new FriendList();
            Debug.Log("DisplayFriendList bugged: " + err.Message);
        }
        DisplayList();
    }

    // creates and displays the friends within friendlist.
    public void DisplayList()
    {
        // issue, either delete or check for all the new friends.
        foreach(Transform obj in Panel.transform)
        {
            GameObject.Destroy(obj);
        }
        foreach(Friend f in FL)
        {
            GameObject NewFriend = Instantiate(FriendObject, Panel.transform);
            NewFriend.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = f.Name;
            NewFriend.transform.GetChild(1).GetComponent<Image>().sprite = f.StatusSprite;
        }
    }
}
