using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class Event
{
    string Name;
    System.DateTime Date;
    List<string> TextPoints;
    int currentIndex = 0;

    public Event(string newPath)
    {
        string path = newPath;
        if(File.Exists(path))
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                List<Event> variables = JsonConvert.DeserializeObject<List<Event>>(json);
            }
        }
    }

    public void ResetEvent()
    {
        currentIndex = 0;
    }

    // return the current text line and then increment the index to the next one.
    public string getText()
    {
        return TextPoints[currentIndex++];
    }

    public IEnumerator<string> GetEnumerator()
    {
        return TextPoints.GetEnumerator();
    }
}
