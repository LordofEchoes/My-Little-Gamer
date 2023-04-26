 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;

// helper class used for JsonConvert, convert json into lists of Dialogue.
public class FriendText
{
    public List<Dialogue> GreetingText;
    public List<Dialogue> TalkText;
}

public class DisplayFriendInteraction : MonoBehaviour
{
    DateSystem DS;
    [SerializeField] GameObject FriendDisplayManagerObject;
    private DisplayFriendManager DisplayManager;
    private Friend currentFriend;
    private FriendText Text;
    private List<Dialogue> DialoguePoints;
    private int currentIndex;
    private int xpReward;


    // Start is called before the first frame update
    // establish the current friend, load the text dialogue of all the options.
    void OnEnable()
    {
        DS = GameObject.Find("UniversalGameManager").GetComponent<GameManager>().GetDateSystem();
        DisplayManager = FriendDisplayManagerObject.GetComponent<DisplayFriendManager>();
        currentFriend = DisplayManager.GetFriend();
        // load all the text into text
        loadText();
        // load the greeting text
        loadGreetingText();
    }

    public void loadText()
    {
        string path = $"Data/Friends/{currentFriend}Text";
        TextAsset AllText = Resources.Load<TextAsset>(path);
        Text = JsonConvert.DeserializeObject<FriendText>(AllText.text);
    }

    public void loadGreetingText()
    {
        DialoguePoints = Text.GreetingText;
        // Next Button is deactivated
        gameObject.transform.GetChild(4).GetComponent<Button>().interactable = false;
        currentIndex = 0;
        xpReward = 0;
        // display it
        DisplayText();
    }

    public void loadTalkText()
    {
        DialoguePoints = Text.TalkText;
        // Activate the Next Button
        gameObject.transform.GetChild(4).GetComponent<Button>().interactable = true;
        currentIndex = 0;
        xpReward = 1000;
        // display it
        DisplayText();
    }

    public void DisplayText()
    {
        gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(DialoguePoints[currentIndex].ImagePath1);
        gameObject.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DialoguePoints[currentIndex].Message;
        gameObject.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>(DialoguePoints[currentIndex].ImagePath2);
    }

    // On Change should check for responses
    public void OnChange()
    {
        // If the text is at the end, end.
        if(IsLastText())
        {
            gameObject.transform.GetChild(4).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next";
            // End result stills needs to be written here. 
            currentFriend.AddXP(xpReward);
            gameObject.GetComponent<TransitionTextScript>().CloseDialog();
            gameObject.transform.GetChild(4).GetComponent<Button>().interactable = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            DS.ProgressDay();
            return;
        }
        // increment to next text, if it is second to last, change next button to end.
        else if(!NextText())
        {
            gameObject.transform.GetChild(4).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "End";
        }
        // Display the text
        DisplayText();
    }

    // returns true if currentIndex >DialoguePoints.Count - 1, false otherwise, increments currentIndex
    public bool NextText(int path = 0)
    {
        currentIndex = DialoguePoints[currentIndex].ResponsePath.Count == 0 ? (++currentIndex >= DialoguePoints.Count - 1 ? DialoguePoints.Count-1: currentIndex) : path;
        return !IsLastText(); 
    }

    // returns true if the dialogue event is in the last text
    public bool IsLastText()
    {
        return currentIndex >= (DialoguePoints.Count-1); 
    }
}
