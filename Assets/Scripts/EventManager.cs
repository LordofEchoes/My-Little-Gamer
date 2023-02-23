using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class EventMetaData
{
    public string Name;
    public string path;
    public string sequelpath;
}


[System.Serializable]
public class EventManager : MonoBehaviour
{
    public List<Event> EventTable;
    public List<EventMetaData> MetaDataTable;
    string database_path;
    int EventNumber = 0;

    public EventManager(string newPath)
    {
        database_path = newPath;
        EventTable = new List<Event>();
        MetaDataTable = new List<EventMetaData>();
        BuildPathTable();
    }

    private void BuildPathTable()
    {
        if(File.Exists(database_path))
        {
            using (StreamReader sr = new StreamReader(database_path))
            {
                string json = sr.ReadToEnd();
                List<EventMetaData> MetaDataTable = JsonConvert.DeserializeObject<List<EventMetaData>>(json);
            }
        }
    }

    public void GenerateEvent()
    {
        // Delete value from the path table whenever generator picks, this way no duplicate events
        System.Random Generator = new System.Random();
        int Roll = Generator.Next(0,MetaDataTable.Count); 
        EventTable.Add(new Event(MetaDataTable[Roll].path));
        MetaDataTable.RemoveAt(Roll);
    }

    // Increments
    public Event GetEvent()
    {
        return EventTable[EventNumber++];
    }
}
