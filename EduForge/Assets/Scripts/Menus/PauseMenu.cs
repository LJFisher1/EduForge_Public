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

    [SerializeField] Slider difficultySlider;
    private MathPuzzle mathPuzzleInstance; // Reference to math puzzle script

    // Hint counter on the top right of the screen. Goes from 5 to 0 and stops.
    private int hintCounter = 5;
    private string[] hints = {
        "Hint Example One",
        "Hint Example Two",
        "Hint Example Three",
        "Hint Example Four",
        "Hint Example Five"
    };

    private string[] difficultyOptions = { "Easy", "Medium", "Hard" };
    private string currentDifficulty;

    public string GetSelectedDifficulty()
    {
        int difficultyIndex = (int)difficultySlider.value;
        return difficultyOptions[difficultyIndex];
    }

    public void OnDifficultySliderChanged()
    {
        int difficultyIndex = (int)difficultySlider.value;
        string selectedDifficulty = difficultyOptions[difficultyIndex];

        if (mathPuzzleInstance != null)
        {
            mathPuzzleInstance.SetDifficulty(selectedDifficulty);
        }
    }

    public void Start()
    {
        PauseMenu.SetActive(false);
        HintPopUpUI.SetActive(false);
        SettingsUI.SetActive(false);
        UpdateHintCounterUI();
        hintCounterUI.SetActive(true);

        difficultySlider.value = 0;
        currentDifficulty = GetSelectedDifficulty();
        mathPuzzleInstance = FindObjectOfType<MathPuzzle>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        int difficultyIndex = (int)difficultySlider.value;
        string selectedDifficulty = difficultyOptions[difficultyIndex];

        if (currentDifficulty != selectedDifficulty)
        {
            currentDifficulty = selectedDifficulty;
            if (mathPuzzleInstance != null)
            {
                mathPuzzleInstance.SetDifficulty(currentDifficulty);
            }
        }
    }

    // Pause button pressed
    public void Pause()
    {
        Time.timeScale = 0;
        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
        hintCounterUI.SetActive(false);
    }

    // Game resumes
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

    // Restarts current level
    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    // A hint was used. Decreases hint counter by 1 and hint pops up.
    public void UseHint()
    {
        if (hintCounter > 0)
        {
            hintCounter--;
            UpdateHintCounterUI();
            DisplayHint();
        }
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
        PauseButton.SetActive(false);
    }

    // Back button to exit out of hint pop-up.
    public void CloseHintAndResume()
    {
        HintPopUpUI.SetActive(false);
        Resume();
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
