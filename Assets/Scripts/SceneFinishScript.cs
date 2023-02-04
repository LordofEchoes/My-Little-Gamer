using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFinishScript : MonoBehaviour
{
    // public GameObject Player;
    public int NextSceneIndex;
    /*
    Simple reusable script:
    Moves the Scene to the next one
    */
    public void NextScene()
    {
        // int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(NextSceneIndex);
        // SceneManager.MoveGameObjectToScene(Player, SceneManager.GetSceneByName(NextSceneName));
    }

    public void CloseDialog()
    {
        NextScene();
    }
}
