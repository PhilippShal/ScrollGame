using System;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void RunGame()
    {
        SceneManager.LoadScene(Constants.GameSceneName);
    }
}
