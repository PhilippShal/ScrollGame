using System;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public bool InSettings = false;
    private GameObject settingsMenu;

    public void ExitClick()
    {
        Application.Quit();
    }

    public void ResumeClick()
    {
        InSettings = false;
        Time.timeScale = 1f;
        settingsMenu.SetActive(false);
    }

    public void SettingsClick()
    {
        if (InSettings)
        {
            return;
        }

        InSettings = true;
        Time.timeScale = 0;
        settingsMenu.SetActive(true);
    }

    public void Start()
    {
        settingsMenu = GameObject.Find("SettingsCanvas");
        settingsMenu.SetActive(false);
    }
}
