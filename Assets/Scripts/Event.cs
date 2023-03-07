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
    public string imagepath1;
    public string imagepath2;
    public string message;
}

[System.Serializable]
public class Event
{
    string Name;
    int DayCycle;
    public List<Dialogue> DialoguePoints = new List<Dialogue>();
    int currentIndex = 0;
    System.DateTime Date;

    // takes in a file path, reads each line and assigns the lines
    // file format:
    // Name
    // day
    // month
    // year
    // int daycycle
    // TextPoints(multiple lines)
    // .
    // .
    // .
    public Event(string newPath)
    {
        // Debug.Log("Event Constructor Called");
        string path = newPath;
        if(File.Exists(path))
        {
            var lines = File.ReadLines(path);
            int lineNumber = 0;
            foreach (var line in lines)
            {
                // Debug.Log($"parsed line is: {line}");
                switch(lineNumber)
                {
                    case 0:
                    Name = line;
                    break;
                    case 1:
                    try
                    {
                        int[] numbers = System.Array.ConvertAll<string,int>(line.Split(','),int.Parse);
                        Date = new System.DateTime(numbers[0],numbers[1],numbers[2]);
                    }
                    catch (System.Exception)
                    {
                        Debug.Log($"Event Date line parse failed, can't convert file to date");
                        throw;
                    }
                    break;
                    case 2:
                    DayCycle = Int32.Parse(line);
                    break;
                    default:
                    DialoguePoints.Add(JsonConvert.DeserializeObject<Dialogue>(line));
                    break;
                }
                lineNumber++;
            }
        }
        else
        {
            Debug.Log($"Event path not found: {path}\nCurrent Directory: {Directory.GetCurrentDirectory()}");
        }
        Date = new System.DateTime(2023,8,4); 
        ValidateEvent();
    }

    public void ValidateEvent()
    {
        Debug.Log($"Name:{Name}\tDayCycle:{DayCycle}\tDialoguePointsCount:{DialoguePoints.Count}\tfirst Message:{DialoguePoints[0].message}");
    }

    // Getter method for day cycle
    public int GetDayCycle()
    {
        return DayCycle;
    }

    // Getter method for DateTime
    public System.DateTime GetDateTime()
    {
        return Date;
    }
    // reset dialogue text
    public void ResetEvent()
    {
        currentIndex = 0;
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

    // return current Dialogue message
    public string GetText()
    {
        return GetDialogue().message;
    }

    public bool IsLastText()
    {
        return currentIndex == DialoguePoints.Count-1;
    }

    public bool NextText()
    {
        // currentIndex should never be greater than max Count, reset and return false
        currentIndex++;
        return !IsLastText();
    }

    // return image path1
    public string GetImagePath1()
    {
        if(File.Exists("Assets/Resources/"+GetDialogue().imagepath1+".png"))
        {
            return GetDialogue().imagepath1;
        }
        Debug.Log($"Event GetImagePath1 not found: {"Assets/Resources/"+GetDialogue().imagepath1+".png"}");
        return "";
    }

    // return image path2
    public string GetImagePath2()
    {
        if(File.Exists("Assets/Resources/"+GetDialogue().imagepath2+".png"))
        {
            return GetDialogue().imagepath2;
        }
        Debug.Log($"Event GetImagePath2 not found: {"Assets/Resources/"+GetDialogue().imagepath2+".png"}");
        return "";
    }

    // allows for looping through dialogue
    public IEnumerator<Dialogue> GetEnumerator()
    {
        return DialoguePoints.GetEnumerator();
    }

    // display current dialogue
    public void DisplayCurrentText(Image image1, Image image2, TextMeshProUGUI textbox)
    {
        // Sprite sprite1 = Resources.Load<Sprite>("Images/PlayerDefault");
        Sprite sprite1 = Resources.Load<Sprite>(GetImagePath1());
        image1.GetComponent<Image>().sprite = sprite1;
        Sprite sprite2 = Resources.Load<Sprite>(GetImagePath2());
        image2.GetComponent<Image>().sprite = sprite2;
        textbox.text = GetText();
    }

}
