using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class MathPuzzle : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button submitButton;
    public GameObject puzzleUI; // The puzzle UI object
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script

    protected bool puzzleSolved = false; // Track if the puzzle was solved
    protected bool isPuzzleGenerated = false; // Track if the puzzle has been generated

    // Start is called before the first frame update
    protected virtual void Start()
    {
        submitButton.onClick.AddListener(CheckAnswer);

        // Hide the puzzle UI initially
        puzzleUI.SetActive(false);
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
        if (!isPuzzleGenerated || IsPuzzleSolved())
        {
            GeneratePuzzle(); // Abstract method to be implemented by derived classes
            isPuzzleGenerated = true;
            puzzleSolved = false; // Reset puzzleSolved when generating a new puzzle
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
    protected abstract void CheckAnswer();

    // Method to generate the puzzle; to be implemented by each puzzle type
    protected abstract void GeneratePuzzle();

    public bool IsPuzzleSolved()
    {
        return puzzleSolved;
    }
}
