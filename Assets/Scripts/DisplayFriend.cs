using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayFriend : MonoBehaviour
{
    
    private GameObject Manager;
    private FriendList FL;
    private Friend friend;
    [SerializeField] GameObject Panel;
    // Panel Arrangement:
    // Child 0: Name
    // Child 1: Image
    // Child 2: Status
    // Child 3: 


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
            Debug.Log("DisplayFriendbugged, friendlist not found: " + err.Message);
        }
        
    }

    public void DisplayInfo(string FriendName)
    {
        // find the friend from the friendlist
        foreach(Friend f in FL)
        {
            if(f.Name == FriendName)
            {
                friend = f;
                // Debug.Log($"DisplayFriend's friend has been found");
                break;
            }
        }
        // display friend on the panel
        Panel.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = friend.Name;
        Panel.transform.GetChild(1).GetComponent<Image>().sprite = friend.picture;
    }

}
