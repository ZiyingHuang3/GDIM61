using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SartMenu : MonoBehaviour
{

    public string firstSceneName = "Sart";
    public void SartGame()
    {
        SceneManager.LoadScene(firstSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
