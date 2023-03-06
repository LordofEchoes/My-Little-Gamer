using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Text class
[System.Serializable]
public class Friend
{
    public int level;
    private int XP;
    public string Name;
    public Sprite StatusSprite;
    private static int maxXP = 1000;
    private static int maxLevel = 5;
    
    public Friend(string newName, int newLevel)
    {
        Name = newName;
        level = newLevel;
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

[System.Serializable]
public class FriendList
{
    public int MaxFriends = 25;
    List<Friend> FL = new List<Friend>();   
    // Start is called before the first frame update
    public void AddFriend(string newName, int newLevel = 0)
    {
        if (FL.Count >= MaxFriends)
        {
            Debug.Log("FriendList Count over the limit!");
        }
        Friend NewFriend = new Friend(newName, newLevel);
        FL.Add(NewFriend);
    }

    public IEnumerator<Friend> GetEnumerator()
    {
        return FL.GetEnumerator();
    }
}
