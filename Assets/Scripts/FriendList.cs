using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

// This class is used to store the status' paths for the 6 sprite animations.
public class StatusPath
{
    private static string SpritePath0 = "Images/Heart0";
    private static string SpritePath1 = "Images/Heart1";
    private static string SpritePath2 = "Images/Heart2";
    private static string SpritePath3 = "Images/Heart3";
    private static string SpritePath4 = "Images/Heart4";
    private static string SpritePath5 = "Images/Heart5";

    public string path;

    public StatusPath(int level = 0)
    {
        switch(level)
        {
            case 0:
            path = SpritePath0;
            break;
            case 1:
            path = SpritePath1;
            break;
            case 2:
            path = SpritePath2;
            break;
            case 3:
            path = SpritePath3;
            break;
            case 4:
            path = SpritePath4;
            break;
            case 5:
            path = SpritePath5;
            break;
            default:
            path = SpritePath0;
            break;
        }
    }
}

// Text class
[System.Serializable]
public class Friend
{
    public int ID;
    public int level;
    public string Name;
    public string Background;
    public string PicturePath;
    public string StatusSpritePath;
    private int XP;
    private static int maxXP = 1000;
    private static int maxLevel = 5;

    public Friend(string newName, int newLevel = 0, string picturePath = "Images/None")
    {
        Name = newName;
        level = newLevel;
    }

    public override string ToString()
    {
        return Name;  
    }

    public void AddXP(int newXP)
    {
        XP += newXP;
        if(XP >= maxXP)
        {
            level++;
            XP /= maxXP;
        }
        if(level > maxLevel)
        {
            level = maxLevel;
            XP = maxXP;
        }
    }

}


// friendlist stores the friend 
[System.Serializable]
public class FriendList
{
    public int MaxFriends = 25;
    SortedDictionary<string, Friend> FL = new SortedDictionary<string,Friend>();

    // Add a friend using the name and path
    public void AddFriend(string name, string path)
    {
        // Debug.Log($"Adding name {name} and path {path} to FriendsList");
        FL.Add(name, JsonConvert.DeserializeObject<Friend>(Resources.Load<TextAsset>(path).text));
    }
    
    //Add a friend
    public void AddFriend(string newName, int newLevel = 0, string picturePath = "Images/None")
    {
        if (FL.Count >= MaxFriends)
        {
            Debug.Log("FriendList Count over the limit!");
        }
        Friend NewFriend = new Friend(newName, newLevel);
        FL.Add(newName, NewFriend);
    }

    public IEnumerator<KeyValuePair<string,Friend>> GetEnumerator()
    {
        return FL.GetEnumerator();
    }

    public bool ContainsKey(string Key)
    {
        return FL.ContainsKey(Key);
    }
}
