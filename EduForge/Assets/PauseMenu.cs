using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (!isActive)
        {
            Time.timeScale = 0f;
            PauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            PauseButton.SetActive(true);
        }
    }

    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
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
