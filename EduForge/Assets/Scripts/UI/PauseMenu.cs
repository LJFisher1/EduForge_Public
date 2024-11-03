using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu; // pause menu ui panel
    [SerializeField] GameObject SettingsUI; // settings ui panel

    [SerializeField] Text hintCounterText; // text element for hint counter
    [SerializeField] GameObject hintCounterUI; // hint counter ui panel
    [SerializeField] GameObject HintPopUpUI; // hint pop-up ui panel
    [SerializeField] Text hintText; // text element for the hint message

    [SerializeField] Slider difficultySlider; // difficulty slider
    private MathPuzzle mathPuzzleInstance; // reference to math puzzle script

    // array for hint counter on the top right of the screen. goes from 5 to 0 and stops. hard-coded list currently til hints are in place
    private int hintCounter = 5;
    private string[] hints = {
        "Hint Example One",
        "Hint Example Two",
        "Hint Example Three",
        "Hint Example Four",
        "Hint Example Five"
    };

    private string[] difficultyOptions = { "Easy", "Medium", "Hard" }; // array of difficulty options
    private bool isPuzzleMode = false; // bool to check if a puzzle is active or not

    public void Start()
    {
        PauseMenu.SetActive(false); // pause menu not open
        HintPopUpUI.SetActive(false); // hint pop-up not open
        SettingsUI.SetActive(false); // settings page not open
        UpdateHintCounterUI(); // hint counter auto-set to 5
        hintCounterUI.SetActive(true); // hint counter appearing

        difficultySlider.value = 0; // difficulty slider set to easy
        mathPuzzleInstance = FindObjectOfType<MathPuzzle>(); // find mathpuzzle instance
        SetInitialDifficulty();
    }

    // sets the initial difficulty for the puzzle when the game starts
    private void SetInitialDifficulty()
    {
        if (mathPuzzleInstance != null)
        {
            mathPuzzleInstance.SetDifficulty(GetSelectedDifficulty());
        }
    }

    // returns the difficulty selected on the slider as a string
    public string GetSelectedDifficulty()
    {
        int difficultyIndex = (int)difficultySlider.value;
        return difficultyOptions[difficultyIndex];
    }

    // checks if the player presses the escp key, toggle pause menu if not in a current puzzle
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    // Game resumes
    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        HintPopUpUI.SetActive(false);
        hintCounterUI.SetActive(true);
    }

    // toggles the pause menu open or closed if no puzzle is currently active
    public void TogglePauseMenu()
    {
        if (isPuzzleMode) return; // dont open pause menu if a puzzle is active

        bool isActive = PauseMenu.activeSelf;
        PauseMenu.SetActive(!isActive); // pause menu visibility

        if (!isActive)
        {
            Time.timeScale = 0f; // pause the game while menu is open
            hintCounterUI.SetActive(false); // hide the hint counter
            Cursor.lockState = CursorLockMode.None; // unlock the cursor for UI interaction
            Cursor.visible = true; // make cursor visible
        }
        else
        {
            Time.timeScale = 1f; // resume the game
            HintPopUpUI.SetActive(false); // hide the hint pop-up if open
            hintCounterUI.SetActive(true); // show the hint counter
            Cursor.lockState = CursorLockMode.Locked; // lock the cursor for gameplay
            Cursor.visible = false; // hide the cursor during gameplay
        }
    }

    // restarts current level
    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    // a hint was used. decreases hint counter by 1 and hint pops up.
    public void UseHint()
    {
        if (hintCounter > 0)
        {
            hintCounter--; // decrease available hints
            UpdateHintCounterUI(); // update hint counter
            DisplayHint(); // show the next hint
        }
    }

    // updates the hint counter text UI to match the current hint counter value
    private void UpdateHintCounterUI()
    {
        hintCounterText.text = hintCounter.ToString();
    }

    // displays the current hint based on remaining hints and shows the hint pop-up
    private void DisplayHint()
    {
        hintText.text = hints[5 - hintCounter - 1]; // show next hint
        HintPopUpUI.SetActive(true); // display hint

        Time.timeScale = 1; // resume gameplay
        PauseMenu.SetActive(false); // hide pause menu
        hintCounterUI.SetActive(true); // show hint counter
    }

    // back button to exit out of hint pop-up
    public void CloseHintAndResume()
    {
        HintPopUpUI.SetActive(false);
        Resume();
    }

    // closes only the hint pop-up without resuming the game
    public void CloseHintPopUp()
    {
        HintPopUpUI.SetActive(false);
    }

    // open settings ui panel
    public void GoToSettings()
    {
        PauseMenu.SetActive(false);
        SettingsUI.SetActive(true);
    }

    // close settings ui panel
    public void CloseSettings()
    {
        SettingsUI.SetActive(false);
        PauseMenu.SetActive(true);
    }

    // exit the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
