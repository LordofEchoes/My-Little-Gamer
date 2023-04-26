using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class Dialogue
{
    // ID is the identifying number that points to this Dialogue.
    public int ID;
    // ImagePath1, ImagePath2, Message all used to convey information to the user
    public string ImagePath1;
    public string ImagePath2;
    public string Message;
    // ResponsePath is the integer key that indicates how the System responses to the choice made by the user.
    public List<int> ResponsePath;
    // Option text should be 0-4 strings that indicates the responses of the user
    public List<string> ResponseText;
    // Reward is a List of a List. 
    // First List indicates the Reward for each Response, Second List is a "Tuple" of [Stat,Amount]
    // e.g. [[1,10],[0,20],[2,-10],[0,-10]]
    public List<List<int>> Reward;
}

[System.Serializable]
public class Event
{
    string name;
    int daycycle;
    public Dictionary<int,Dialogue> DialoguePoints = new Dictionary<int,Dialogue>();
    int currentIndex = 0;
    System.DateTime date;

    public Event()
    {
        date = new System.DateTime(2023,8,4);
    }

    // takes in a file path, reads each line and assigns the lines
    // file format:
    // name
    // day
    // month
    // year
    // int daycycle
    // TextPoints(multiple lines)
    // .
    // .
    // .
    public Event(string newPath, System.DateTime newDate)
    {
        //Using Text Asset:
        string path = newPath;
        date = newDate;
        TextAsset EventFile = Resources.Load<TextAsset>(path);
        string[] lines = EventFile.text.Split(System.Environment.NewLine);
        int lineNumber = 0;
        foreach(var line in lines)
        {
            switch(lineNumber)
            {
                case 0:
                name = line;
                break;
                case 1:
                daycycle = Int32.Parse(line);
                break;
                default:
                Dialogue DialogueObject = JsonConvert.DeserializeObject<Dialogue>(line);
                DialoguePoints.Add(DialogueObject.ID, DialogueObject);
                break;
            }
            lineNumber++;
        }
        ValidateEvent();
    }

    public override string ToString()
    {
        return $"Event name: {name}\t Dialogue's CurrentIndex: {currentIndex}\t DialoguePoint's Count: {DialoguePoints.Count}";
    }

    public void ValidateEvent()
    {
        Debug.Log($"Event Validation:\nName: {name}\tDayCycle: {daycycle}\tDialoguePointsCount: {DialoguePoints.Count}\tFirst Message: {DialoguePoints[0].Message}");
    }

    
    // reset dialogue text
    public void ResetEvent()
    {
        currentIndex = 0;
    }

    // Getter method for day cycle
    public int GetDayCycle()
    {
        return daycycle;
    }

    // Getter method for dateTime
    public System.DateTime GetDateTime()
    {
        return date;
    }

    // return the current text line and then increment the index to the next one.
    public Dialogue GetDialogue()
    {
        // check if dialogue works. else return empty dialogue
        if(currentIndex < DialoguePoints.Count)
        {
            return DialoguePoints[currentIndex];
        } 
        return new Dialogue();
    }

    // return current Dialogue Message
    public string GetText()
    {
        // Debug.Log($"Return Dialogue text: {GetDialogue().Message}");
        return GetDialogue().Message;
    }

    public bool IsLastText()
    {
        // Debug.Log($"CurrentIndex: {currentIndex}\t Dialogue Count: {DialoguePoints.Count}");
        return currentIndex >= (DialoguePoints.Count-1); 
    }
    
    //increment the next text, 
    public bool NextText()
    {
        currentIndex++;
        // currentIndex should never be greater than max Count, reset and return false
        if(IsLastText())
        {
            currentIndex = DialoguePoints.Count-1;
            return false;
        }
        return true;
    }

    // return image path1
    public string GetImagePath1()
    {
        return GetDialogue().ImagePath1;
    }

    // return image path2
    public string GetImagePath2()
    {
        return GetDialogue().ImagePath2;
    }

    // allows for looping through dialogue
    public IEnumerator GetEnumerator()
    {
        return DialoguePoints.GetEnumerator();
    }

    // display current dialogue
    public void DisplayCurrentText(Image image1, Image image2, TextMeshProUGUI textbox)
    {
        Debug.Log($"Dialogue Index: {currentIndex}");
        // Sprite sprite1 = Resources.Load<Sprite>("Images/PlayerDefault");
        Sprite sprite1 = Resources.Load<Sprite>(GetImagePath1());
        image1.GetComponent<Image>().sprite = sprite1;
        Sprite sprite2 = Resources.Load<Sprite>(GetImagePath2());
        image2.GetComponent<Image>().sprite = sprite2;
        textbox.text = GetText();
    }

}
