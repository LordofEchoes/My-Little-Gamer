using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFinishScript : MonoBehaviour
{
    /*
    Simple reusable script:
    Moves the Scene to the next one
    */
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CloseDialog()
    {
        NextScene();
    }
}
