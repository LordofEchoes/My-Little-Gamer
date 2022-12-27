using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingAllocation : MonoBehaviour
{
    public int TrainingPointTotal = 3;
    public int PointCount{get;set;}
    public Dictionary<string, int> TrainingPointsTable;
    public TextMeshProUGUI PointDisplay;
    [SerializeField] GameObject ErrorObject;
    [SerializeField] TextMeshProUGUI ErrorText;

    void Awake()
    {
        TrainingPointsTable = new Dictionary<string,int>();
        TrainingPointsTable.Add("Mechanics", 0);
        TrainingPointsTable.Add("Tactics", 0);
        TrainingPointsTable.Add("Knowledge", 0);
        PointCount = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        PointDisplay.text = TrainingPointTotal.ToString();
    }

    void OnDisable()
    {
        TrainingPointsTable["Mechanics"] = 0;
        TrainingPointsTable["Tactics"] = 0;
        TrainingPointsTable["Knowledge"] = 0;
        PointCount = 0;
        PointDisplay.text = TrainingPointTotal.ToString();
        Debug.Log("TrainingPointsTable has been Disabled.");
    }

    void OnChange()
    {
        PointDisplay.text = (TrainingPointTotal - PointCount).ToString();
    }

    public void ChangePoint(string TableKey, int Value)
    {
        if (TrainingPointsTable[TableKey] + Value < 0)
        {
            // remaining points is less than 0. Raise error message
            ErrorText.text = "Training Points can't be less than 0.";
            ErrorObject.SetActive(true);
            StartCoroutine(WaitForError());
        }
        else if(PointCount+Value > TrainingPointTotal)
        {
            // remaining points is greater than point total. Raise error message
            ErrorText.text = "Training Points can't be greater than " + TrainingPointTotal.ToString() + ".";
            ErrorObject.SetActive(true);
            StartCoroutine(WaitForError());
        }
        else
        {
            // Things are okay, proceed with changes.
            TrainingPointsTable[TableKey] += Value;
            PointCount += Value;
            OnChange();
        }
    }

    IEnumerator WaitForError()
    {
        yield return new WaitForSeconds(2);
        ErrorObject.GetComponent<PopUpScript>().CloseDialog();
    }
}
