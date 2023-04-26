using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayEvent : MonoBehaviour
{
    GameManager Manager;
    DateSystem DS;
    Event E;
    [SerializeField] EventManager EM;
    [SerializeField] TextMeshProUGUI DisplayText;
    [SerializeField] GameObject DisplayScreen;
    [SerializeField] GameObject GameScreen;
    [SerializeField] Image Person1;
    [SerializeField] Image Person2;
    [SerializeField] GameObject EndButton;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Manager = GameObject.Find("UniversalGameManager").GetComponent<GameManager>();
            DS =  Manager.GetDateSystem();
            EM = Manager.GetEventManager();
            E = EM.GetEvent();
        }
        catch(System.NullReferenceException err)
        {
            EM = new EventManager();
            Debug.Log("DisplayEvent Manager bugged: " + err.Message);
        }
        CheckDate(true);
    }

    // display text, adjust end button text and action accordingly when Next is clicked.
    public void OnChange()
    { 
        Debug.Log("Onchange clicked");
        //if the event is on the last text, go next screen and breakout
        if(E.IsLastText())
        {
            EndButton.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "Next";
            gameObject.GetComponent<TransitionTextScript>().CloseDialog();
            return;
        }
        // get the Next text, if the next text is the last one, change the text.
        else if(!E.NextText())
        {
            EndButton.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "End";
        }
        // display the text
        UpdateEventText();
    }
    
    // updates the text/image display
    void UpdateEventText()
    {
        // Debug.Log($"Event Display Requested");
        E.DisplayCurrentText(Person1, Person2, DisplayText);
    }

    // checks and activates the display screen as necessary
    public void CheckDate(bool check = false)
    {
        if(check)
        {
            if(DS.GetDateTime() == EM.GetEventDateTime() && DS.GetCycle() == E.GetDayCycle())
            {
                DisplayScreen.SetActive(true);
                GameScreen.SetActive(false);
                UpdateEventText();
            }
            else
            {
                DisplayScreen.SetActive(false);
                GameScreen.SetActive(true);
            }
        }
        else
        {
            DisplayScreen.SetActive(true);
            UpdateEventText();
        }
    }
}
