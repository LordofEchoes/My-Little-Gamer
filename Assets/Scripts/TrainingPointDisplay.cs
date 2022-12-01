using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingPointDisplay : MonoBehaviour
{
    [SerializeField] GameObject TrainingPointObject;
    [SerializeField] string TableKey;
    private TrainingAllocation TrainingScript;
    [SerializeField] TextMeshProUGUI PointDisplay;

    // Start is called before the first frame update
    void Start()
    {
        TrainingScript = TrainingPointObject.GetComponent<TrainingAllocation>();
        OnChange();
    }

    void OnDisable()
    {
        OnChange();
        Debug.Log(TableKey + " Display has been disabled.");
    }

    public void OnChange()
    {
        PointDisplay.text = TrainingScript.TrainingPointsTable[TableKey].ToString();
    }

    public void ChangePoint(int Value)
    {
        TrainingScript.ChangePoint(TableKey,Value);
        OnChange();
    }
}
