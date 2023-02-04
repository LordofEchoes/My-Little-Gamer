using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseControl : MonoBehaviour
{
    public static bool GameIsPaused;
    public TextMeshProUGUI PlayOrPauseText;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameIsPaused = !GameIsPaused;
            PauseGame();
        }
    }

    public void OnChange()
    {
        GameIsPaused = !GameIsPaused;
        PauseGame();
    }

    void PauseGame()
    {
        if(GameIsPaused)
        {
            Time.timeScale = 0;
            PlayOrPauseText.text = "Play";
        }
        else
        {
            Time.timeScale = 1;
            PlayOrPauseText.text = "Pause";
        }
    }
}
