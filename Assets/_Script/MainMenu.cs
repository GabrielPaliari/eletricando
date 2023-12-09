using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public enum Scenes {
        MainMenu,
        CircuitBuild
    }
    public void PlayLevel()
    {
        SceneManager.LoadScene(Scenes.CircuitBuild.ToString());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
