using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FriendButton : MonoBehaviour
{
    public void OnClick()
    {
        // Debug.Log($"Friend Button Name: {gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text}");
        GameObject Panel = GameObject.Find("FriendDisplay");
        if(Panel.transform.GetChild(0).gameObject.activeSelf == false)
        {
            Panel.transform.GetChild(0).gameObject.SetActive(true);
        }
        Panel.GetComponent<DisplayFriendManager>().DisplayInfo(gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
    }
}
