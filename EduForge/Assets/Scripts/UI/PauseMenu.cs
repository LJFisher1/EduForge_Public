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
    [SerializeField] GameObject hintCounterUI1; // hint counter ui panel
    [SerializeField] GameObject HintPopUpUI1; // hint pop-up ui panel
    [SerializeField] Text hintText; // text element for the hint message

    [SerializeField] Slider difficultySlider; // difficulty slider
    private MathPuzzle mathPuzzleInstance; // reference to math puzzle script
    private PlayerMovement playerMovement; // reference to the player movement script

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
    private bool isInMainMenu; // bool to check if the settings was opened from the main menu or in-game

    public void Start()
    {
        isInMainMenu = SceneManager.GetActiveScene().name == "MainMenu"; // checks if the main menu scene is open
        PauseMenu.SetActive(false); // pause menu not open
        HintPopUpUI1.SetActive(false); // hint pop-up not open
        SettingsUI.SetActive(false); // settings page not open
        UpdateHintCounterUI(); // hint counter auto-set to 5
        hintCounterUI1.SetActive(true); // hint counter appearing

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
        HintPopUpUI1.SetActive(false);
        hintCounterUI1.SetActive(true);
        Cursor.visible = false;
    }

    public void TogglePauseMenu()
    {
        if (isPuzzleMode) return; // don't open pause menu if a puzzle is active
        if (isInMainMenu) return; // don't open pause menu if main menu is open

        bool isActive = PauseMenu.activeSelf;
        PauseMenu.SetActive(!isActive); // toggle pause menu visibility

        if (!isActive) // pausing the game
        {
            Time.timeScale = 0f; // pause gameplay
            hintCounterUI1.SetActive(false); // hide the hint counter if assigned
            Cursor.lockState = CursorLockMode.None; // unlock the cursor for UI interaction
            Cursor.visible = true; // make cursor visible
        }
        else // resuming the game
        {
            Time.timeScale = 1f; // resume gameplay
            HintPopUpUI1.SetActive(false); // hide the hint pop-up if open
            hintCounterUI1.SetActive(true); // show the hint counter if assigned
            Cursor.lockState = CursorLockMode.Locked; // lock the cursor for gameplay
            Cursor.visible = false; // hide the cursor during gameplay
        }
    }

    // restarts current level
    public void Restart()
    {
        Time.timeScale = 1;
        Debug.Log("Restart button clicked");
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
        HintPopUpUI1.SetActive(true); // display hint

        Time.timeScale = 1; // resume gameplay
        PauseMenu.SetActive(false); // hide pause menu
        hintCounterUI1.SetActive(true); // show hint counter
    }

    // back button to exit out of hint pop-up
    public void CloseHintAndResume()
    {
        HintPopUpUI1.SetActive(false);
        Resume();
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

        if (isInMainMenu)
        {
            PauseMenu.SetActive(false);
        }
        else
        {
            PauseMenu.SetActive(true);
        }
    }

    // exit the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
