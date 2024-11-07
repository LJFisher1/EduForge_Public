using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;



public abstract class MathPuzzle : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button submitButton;
    public GameObject puzzleUI; // The puzzle UI object
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public PauseMenuScript pauseMenuScript; // Reference to the PauseMenu script
    public TextMeshProUGUI feedbackText; // Feedback text for the user


    // Declare the event that will be triggered when the puzzle is solved
    public UnityEvent<string> onPuzzleSolved;

    protected bool puzzleSolved = false; // Track if the puzzle was solved
    protected bool isPuzzleGenerated = false; // Track if the puzzle has been generated

    protected string selectedDifficulty; // To hold the difficulty 

    // Start is called before the first frame update
    protected virtual void Start()
    {
        submitButton.onClick.AddListener(OnSubmitAnswer);

        // Initialize the UnityEvent if not already done
        if (onPuzzleSolved == null)
        {
            onPuzzleSolved = new UnityEvent<string>();
        }

        // Hide the puzzle UI initially
        puzzleUI.SetActive(false);

        // Attempts to find the Pause Menu script if it wasn't assigned in the editor
        if (pauseMenuScript == null)
        {
            GameObject pauseMenuObject = GameObject.Find("PauseMenu");
            if (pauseMenuObject != null)
            {
                pauseMenuScript = pauseMenuObject.GetComponent<PauseMenuScript>();
            }
        }
        // Get the difficulty from the PauseMenu script
        if (pauseMenuScript != null)
        {
            selectedDifficulty = pauseMenuScript.GetSelectedDifficulty();
        }
        else
        {
            Debug.LogWarning("PauseMenuScript not found in the scene. Ensure the PauseMenu GameObject has the PauseMenuScript attached.");
        }
    }

    protected virtual void Update()
    {
        if (puzzleUI.activeSelf) // If the puzzle UI is active
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // Exit the puzzle
            {
                EndPuzzle();
            }
        }
    }

    // Method to start the puzzle
    public virtual void StartPuzzle()
    {
        if (pauseMenuScript != null)
        {
            selectedDifficulty = pauseMenuScript.GetSelectedDifficulty(); // Gets the selected difficulty from the Pause Menu script
        }
        else
        {
            Debug.LogWarning("PauseMenuScript is not assigned. Difficulty level may not be set correctly.");
        }


        if (!isPuzzleGenerated || IsPuzzleSolved())
        {
            GeneratePuzzle(); // Abstract method to be implemented by derived classes
            isPuzzleGenerated = true;
            puzzleSolved = false; // Reset puzzleSolved when generating a new puzzle
            ResetFeedback();
        }
        else
        {
            // Ensure the UI is displayed properly without regenerating the puzzle
            puzzleUI.SetActive(true);
            playerMovement.TogglePuzzleMode(true); // Disable movement/camera controls
            return;
        }

        puzzleUI.SetActive(true); // Shows the puzzle UI
        playerMovement.TogglePuzzleMode(true); // Disable movement/camera controls
    }

    public virtual void EndPuzzle()
    {
        puzzleUI.SetActive(false); // Hide the puzzle UI
        playerMovement.TogglePuzzleMode(false); // Re-enable movement/camera controls
    }

    // Check the player's answer; to be implemented by each puzzle type
    protected abstract void CheckAnswer(string userAnswer); // Re-protected so only the correct puzzle can call it

    //public void SubmitPuzzleAnswer(string userAnswer)        // To be used by the classes to call check answer
    //{
    //    CheckAnswer(userAnswer);
    //}

    // Method to generate the puzzle; to be implemented by each puzzle type
    protected abstract void GeneratePuzzle();

    public bool IsPuzzleSolved() => puzzleSolved;

    // A method to handle the submit button interaction
    public virtual void OnSubmitAnswer()
    {
        // Prevent answer checking if the puzzle is already solved
        if (puzzleSolved)
            return;

        CheckAnswer(inputField.text);

        // If the puzzle is solved, invoke the onPuzzleSolved event
        if (puzzleSolved)
        {
            DisplayFeedback("Correct!", true);
            // Trigger the event, passing the puzzle's name or ID
            onPuzzleSolved.Invoke(this.gameObject.name);
        }
        else
        {
            DisplayFeedback("Incorrect. Try again.", false);
        }
    }

    public abstract void ResetPuzzleState();

    public abstract string GetCurrentPuzzleType();

    // Placeholders for the difficulty methods to be overridden by each child puzzle class
    public virtual void SetEasyDifficulty()
    {
        Debug.Log("Easy difficulty not implemented for this puzzle type.");
    }
    public virtual void SetMediumDifficulty()
    {
        Debug.Log("Medium difficulty not implemented for this puzzle type.");
    }
    public virtual void SetHardDifficulty()
    {
        Debug.Log("Hard difficulty not implemented for this puzzle type.");
    }

    public void SetDifficulty(string difficulty)
    {
        selectedDifficulty = difficulty;
    }

    protected void DisplayFeedback(string message, bool isCorrect)
    {
        feedbackText.text = message;
        feedbackText.color = new Color(feedbackText.color.r, feedbackText.color.g, feedbackText.color.b, 1); // Ensure it's fully opaque

        // float fadeDuration = isCorrect ? 5f : 3f;
        // StartCoroutine(FadeFeedback(fadeDuration));
        float hideDuration = 3f;
        StartCoroutine(HideFeedbackAfterTime(hideDuration));
    }

    public void ResetFeedback()
    {
        feedbackText.text = "";
    }

    private IEnumerator FadeFeedback(float duration)
    {
        Color originalColor = feedbackText.color;
        float startAlpha = originalColor.a;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            feedbackText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(startAlpha, 0, normalizedTime));
            yield return null;
        }

        feedbackText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }

    private IEnumerator HideFeedbackAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        feedbackText.text = "";
    }


}