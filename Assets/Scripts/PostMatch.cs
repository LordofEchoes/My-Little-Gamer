using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PostMatch : MonoBehaviour
{
    [SerializeField] GameObject BattleScreen;
    [SerializeField] GameObject WinnerStar;
    [SerializeField] GameObject StatusObject;
    // Panel == 0 means player, Panel == 1 is the Enemy, set in the Editor
    public int CharacterPanel;
    private int Winner;
    private CharacterStats Character;

    void Start()
    {
        try
        {
            Character = CharacterPanel == 0 ? GameObject.Find("UniversalGameManager").GetComponent<GameManager>().GetPlayer() : GameObject.Find("UniversalGameManager").GetComponent<GameManager>().GetEnemyManager().GetCurrentEnemy();
        }
        catch(System.NullReferenceException err)
        {
            Debug.Log($"PostMatch Error, cannot get player or enemy stats: {err.Message}");
        }
        StatusUpdate();
    }
    
    public void StatusUpdate()
    {
        StatusObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Character.Gold.ToString();
        StatusObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = Character.Gold.GetCreepScore().ToString();
        StatusObject.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = Character.Kills.ToString();
        StatusObject.transform.GetChild(11).GetComponent<TextMeshProUGUI>().text = Character.Deaths.ToString();
    }

    public void OnEnable()
    {
        try
        {
            Winner = BattleScreen.GetComponent<BattleScript>().GetWinner();
            if(Winner == CharacterPanel)
            {
                WinnerStar.SetActive(true);
            }
            else
            {
                WinnerStar.SetActive(false);
            }
        }
        catch(System.NullReferenceException err)
        {
            Debug.Log($"PostMatch can't get the winner from the battleScreen: {err.Message}");
        }
    }
}
