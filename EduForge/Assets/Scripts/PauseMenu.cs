using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject PauseButton;
    [SerializeField] GameObject SettingsUI;
    [SerializeField] Text hintCounterText;
    [SerializeField] GameObject hintCounterUI;
    [SerializeField] GameObject HintPopUpUI;
    [SerializeField] Text hintText;

    private int hintCounter = 5;
    private string[] hints = {
        "Hint Example One",
        "Hint Example Two",
        "Hint Example Three",
        "Hint Example Four",
        "Hint Example Five"
    };

    public void Start()
    {
        PauseMenu.SetActive(false);
        HintPopUpUI.SetActive(false);
        SettingsUI.SetActive(false);
        UpdateHintCounterUI();
        hintCounterUI.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
        hintCounterUI.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false);
        HintPopUpUI.SetActive(false);
        hintCounterUI.SetActive(true);
    }

    public void TogglePauseMenu()
    {
        bool isActive = PauseMenu.activeSelf;
        PauseMenu.SetActive(!isActive);

        if (!isActive)
        {
            Time.timeScale = 0f;
            PauseButton.SetActive(false);
            hintCounterUI.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            PauseButton.SetActive(true);
            HintPopUpUI.SetActive(false);
            hintCounterUI.SetActive(true);
        }
    }

    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void UseHint()
    {
        if (hintCounter > 0)
        {
            hintCounter--;
            UpdateHintCounterUI();
            DisplayHint();
        }

        Resume();
    }

    private void UpdateHintCounterUI()
    {
        hintCounterText.text = hintCounter.ToString();
    }

    private void DisplayHint()
    {
        hintText.text = hints[5 - hintCounter - 1];
        HintPopUpUI.SetActive(true);

        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        hintCounterUI.SetActive(true);
        PauseButton.SetActive(true);
    }

    public void CloseHintPopUp()
    {
        HintPopUpUI.SetActive(false);
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
