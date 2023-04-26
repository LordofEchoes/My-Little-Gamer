using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
// class used to store meta data
public class EventMetaData
{
    public string Name;
    public string path;
    public string sequelpath;
    public List<string> People;

    public override string ToString()
    {
        return $"\nName: {Name}\nPath: {path}\nSequelPath: {sequelpath}";
    }
}


[System.Serializable]
public class EventManager
{
    public List<Event> EventTable;
    public List<EventMetaData> MetaDataTable;
    string dataBaseText;
    int EventNumber = 0;
    // constructors for the Event Manager
    public EventManager()
    {
        EventTable = new List<Event>();
        MetaDataTable = new List<EventMetaData>();
        // Debug.Log("Event Manager default manager called, no database path");
    }

    public EventManager(string pathtext)
    {
        dataBaseText = pathtext;
        EventTable = new List<Event>();
        MetaDataTable = new List<EventMetaData>();
        BuildMetaDataTable();
        VerifyMetaDataTable();
        VerifyEventTable();
    }

    public override string ToString()
    {
        return $"MetaDataTable's Count{MetaDataTable.Count}\tEventTable's Count{EventTable.Count}";
    }

    // builds the metadata path table for each event.(smaller than each event)
    private void BuildMetaDataTable()
    {
        // Use TextAsset within the editor to assign the file to decode.
        MetaDataTable = JsonConvert.DeserializeObject<List<EventMetaData>>(dataBaseText);
        // if(File.Exists(database_path))
        // {
        //     // Debug.Log($"database found at path: {database_path}");
        //     using (StreamReader sr = File.OpenText(database_path))
        //     {
        //         string json = sr.ReadToEnd();
        //         MetaDataTable = JsonConvert.DeserializeObject<List<EventMetaData>>(json);
        //     }
        // }
        // else
        // {
        //     Debug.Log($"database path not found: {database_path}\nApp datapath: {Application.persistentDataPath}");
        // }
    }

    public void VerifyMetaDataTable()
    {
        for(int i = 0; i < MetaDataTable.Count;i++)
        {
            Debug.Log($"MetaDataTable index {i}: {MetaDataTable[i]}");
        }
    }

    public void VerifyEventTable()
    {
        for(int i = 0; i < EventTable.Count;i++)
        {
            Debug.Log($"EventTable index {i}: {EventTable[i]}\n");
        }
    }
    // Generate Event, return list of names that are friends
    public List<string> GenerateEvent(int num = -1)
    {
        // If No possible events just quit
        if(MetaDataTable.Count == 0)
        {
            Debug.Log($"No Events could be generated");
            return new List<string>();
        }
        // Delete value from the path table whenever generator picks, this way no duplicate events
        System.Random Generator = new System.Random();
        int Roll = num != -1 ? num : Generator.Next(0,MetaDataTable.Count);
        Debug.Log($"EventManager Event Generator Roll: {Roll}");
        // Events happen everyweek?
        System.DateTime newDate = new System.DateTime(2023,8,4).AddDays(7*EventTable.Count);
        EventTable.Add(new Event(MetaDataTable[Roll].path, newDate));
        List<string> ret = MetaDataTable[Roll].People;
        MetaDataTable.RemoveAt(Roll);
        return ret;
    }
 
    // return Current event
    public Event GetEvent()
    {
        // VerifyEventTable();
        // Debug.Log($"Event Number: {EventNumber}\t EventTable Count: {EventTable.Count}");
        if(EventNumber < EventTable.Count)
        {
            return EventTable[EventNumber];
        }
        return new Event();
    }

    // return current event's datetime
    public System.DateTime GetEventDateTime()
    {
        return EventTable[EventNumber].GetDateTime();
    }

    
}
