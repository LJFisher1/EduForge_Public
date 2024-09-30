using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject PauseButton;
    [SerializeField] GameObject SettingsUI;

    public void Start()
    {
        PauseMenu.SetActive(false);
        SettingsUI.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        bool isActive = PauseMenu.activeSelf;
        PauseMenu.SetActive(!isActive);

        if (!isActive) // If pause menu is activated
        {
            Time.timeScale = 0f;
            PauseButton.SetActive(false);
        }
        else // If pause menu is deactivated
        {
            Time.timeScale = 1f;
            PauseButton.SetActive(true);
        }
    }

    public void GoToSettings()
    {
        PauseMenu.SetActive(false);
        SettingsUI.SetActive(true);
    }

    public void CloseSettings()
    {
        SettingsUI.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
