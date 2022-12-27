using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalendarDisplay : MonoBehaviour
{
    private GameObject Manager;
    private DateSystem DS;
    [SerializeField] GameObject Calendar;
    private Dictionary<System.DayOfWeek,int> DayOfWeekTable;
    [SerializeField] TextMeshProUGUI DisplayText;

    void Awake()
    {
        DayOfWeekTable = new Dictionary<System.DayOfWeek,int>();
        DayOfWeekTable.Add(System.DayOfWeek.Sunday,0);
        DayOfWeekTable.Add(System.DayOfWeek.Monday,1);
        DayOfWeekTable.Add(System.DayOfWeek.Tuesday,2);
        DayOfWeekTable.Add(System.DayOfWeek.Wednesday,3);
        DayOfWeekTable.Add(System.DayOfWeek.Thursday,4);
        DayOfWeekTable.Add(System.DayOfWeek.Friday,5);
        DayOfWeekTable.Add(System.DayOfWeek.Saturday,6);

    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Calendar Display Started");
        Manager = GameObject.Find("UniversalGameManager");
        DS = Manager.GetComponent<DateSystem>();
        UpdateCalendar();
        DisplayMonthYear();
    }

    // Update is called once per frame
    public void UpdateCalendar()
    {
        System.DateTime StartDateTime = new System.DateTime(DS.Year, DS.Month, 1);
        int StartingOffset = DayOfWeekTable[StartDateTime.DayOfWeek];
        // go through each row and assign the values
        for(int RowIndex = 1; RowIndex < 7; RowIndex++)
        {
            GameObject CurrentRow = Calendar.transform.GetChild(RowIndex).gameObject;
            // for the first row we want to add the offset
            for(int ColumnIndex = 0; ColumnIndex < 7; ColumnIndex++)
            {
                GameObject Image = CurrentRow.transform.GetChild(ColumnIndex).gameObject;
                TextMeshProUGUI DayText = Image.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                if(RowIndex == 1)
                {
                    if(ColumnIndex < StartingOffset)
                    {
                        // not a valid day, 
                        DayText.text = "";
                    }
                    else
                    {
                        // change the text to the day
                        DayText.text = StartDateTime.Day.ToString();
                        StartDateTime = StartDateTime.AddDays(1);
                    }
                }
                else
                {
                    // change the text to the day
                    if(StartDateTime.Month == DS.Month)
                    {
                        DayText.text = StartDateTime.Day.ToString();
                        StartDateTime = StartDateTime.AddDays(1);
                    }
                    else
                    {
                        DayText.text = "";
                    }
                    
                }
                Debug.Log(StartDateTime.Day.ToString());
            }
        }
        
    }

    public void DisplayMonthYear()
    {
        if (DisplayText != null)
        {
            DisplayText.text = DS.MonthYearAsString();
        }
        else
        {
            DisplayText.text = "Jan 2022";
        }
    }
}
