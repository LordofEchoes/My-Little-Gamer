using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayFriendManager : MonoBehaviour
{
    
    private GameObject Manager;
    private FriendList FL;
    private Friend currentFriend;
    private string currentAction;
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
        foreach(KeyValuePair<string,Friend> f in FL)
        {
            if(f.Key == FriendName)
            {
                // FriendName found, select it as the current Friend
                SelectFriend(f.Value);
                // Debug.Log($"DisplayFriend's friend has been found");
                break;
            }
        }
        // display friend on the panel
        Panel.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentFriend.Name;
        Panel.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(currentFriend.PicturePath); 
        Panel.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentFriend.Background;
        Debug.Log($"DisplayManager ImagePath:{currentFriend.PicturePath}");
    }

    public void SelectFriend(Friend newFriend)
    {
        currentFriend = newFriend;
    }
    
    public Friend GetFriend()
    {
        return currentFriend;
    }

    public void SelectAction(string newAction)
    {
        currentAction = newAction;
    }
}
 