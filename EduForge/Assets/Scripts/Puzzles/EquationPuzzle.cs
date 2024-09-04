using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquationPuzzle : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI equationText;
    public Button submitButton;
    public GameObject puzzleUI; // The puzzle UI object that we interact with to trigger the puzzle
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script

    private int solution;
    private bool puzzleSolved = false; // Track if the puzzle was solved


    // Start is called before the first frame update
    private void Start()
    {
        submitButton.onClick.AddListener(CheckAnswer);

        // Hide the puzzle UI initially
        puzzleUI.SetActive(false);
    }

    private void Update()
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
    public void StartPuzzle()
    {
        GenerateEquation();
        puzzleUI.SetActive(true); // Shows the puzzle UI
        playerMovement.TogglePuzzleMode(true); // Disables the movement and camera controls
        puzzleSolved = false; // Reset puzzle solved status
    }

    public void EndPuzzle()
    {
        puzzleUI.SetActive(false); // Hides puzzle UI
        playerMovement.TogglePuzzleMode(false); // Re-enables the movement and camera controls

    }

    private void GenerateEquation()
    {
        int a = Random.Range(1, 20); // Random #1 1-20
        int b = Random.Range(1, 20); // Random #2 1-20
        string[] operators = { "+", "-", "*" }; // Array of operators
        string selectedOperator = operators[Random.Range(0, operators.Length)]; // Randomly chooses an operator

        // Calculate the solution based on operator
        switch (selectedOperator)
        {
            case "+":
                solution = a + b;
                break;
            case "-":
                solution = a - b;
                break;
            case "*":
                solution = a * b;
                break;
        }

        equationText.text = $"{a} {selectedOperator} {b} = ?"; // Display the generation equation
    }

    private void CheckAnswer()
    {
        if (int.TryParse(inputField.text, out int userAnswer))
        {
            if (userAnswer == solution)
            {
                Debug.Log("Correct! Well done.");
                puzzleSolved = true; // Mark puzzle as solved
                inputField.text = "";
                EndPuzzle();
            }
            else
            {
                Debug.Log("Incorrect. Try again.");
                inputField.text = "";
            }
        }
        else
        {
            Debug.Log("Invalid input. Please enter a number.");
        }
    }

    public bool IsPuzzleSolved()
    {
        return puzzleSolved;
    }
}
