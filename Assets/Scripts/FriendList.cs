using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

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
        PicturePath = picturePath;
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
            StatusSpritePath = StatusPath(level);
        }
        if(level > maxLevel)
        {
            level = maxLevel;
            XP = maxXP;
        }
        Debug.Log($"Name: {Name}\nCurrent Level: {level}");
    }

    // This method is using to store the sprite paths and used by friend to set the right sprite path
    public string StatusPath(int level = 0)
    {
        // change the paths here
        string SpritePath0 = "Images/Heart0";
        string SpritePath1 = "Images/Heart1";
        string SpritePath2 = "Images/Heart2";
        string SpritePath3 = "Images/Heart3";
        string SpritePath4 = "Images/Heart4";
        string SpritePath5 = "Images/Heart5";
        switch(level)
        {
            case 0:
            return SpritePath0;
            case 1:
            return SpritePath1;
            case 2:
            return SpritePath2;
            case 3:
            return SpritePath3;
            case 4:
            return SpritePath4;
            case 5:
            return SpritePath5;
            default:
            return SpritePath0;
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
