using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquationPuzzle : MathPuzzle
{
    public TextMeshProUGUI equationText;

    // To store puzzle data 
    private int solution;               // Store the solution
    private string currentEquation;     // Store the equation text
    private string selectedOperator;    // Store the selected operator
    private int a, b;                   // Store the operands
    protected string currentPuzzleType;

    protected override void GeneratePuzzle()
    {
        if (!isPuzzleGenerated)
        {
            // For testing purposes
            // selectedDifficulty = "Hard";
            // End testing
            switch (selectedDifficulty)
            {
                case "Easy":
                    SetEasyDifficulty();
                    break;

                case "Medium":
                    SetMediumDifficulty();
                    break;

                case "Hard":
                    SetHardDifficulty();
                    break;

                default:
                    SetEasyDifficulty();
                    break;
            }

            string[] operators = { "+", "-", "*" };
            selectedOperator = operators[Random.Range(0, operators.Length)];
            currentPuzzleType = "Equation";

            Debug.Log("Puzzle type set: " + currentPuzzleType);

            switch (selectedOperator)
            {
                case "+":
                    currentPuzzleType += ": +";
                    solution = a + b;
                    break;
                case "-":
                    currentPuzzleType += ": -";
                    solution = a - b;
                    break;
                case "*":
                    currentPuzzleType += ": *";
                    solution = a * b;
                    break;
            }
            Debug.Log("Puzzle type set: " + currentPuzzleType);
            currentEquation = $"{a} {selectedOperator} {b} = ?";
            equationText.text = currentEquation;
            isPuzzleGenerated = true;
        }
        else
        {
            // If the puzzle is already generated, reuse the existing equation
            equationText.text = currentEquation;
        }
    }

    protected override void CheckAnswer(string userAnswer)
    {
        if (int.TryParse(userAnswer, out int parsedAnswer))
        {
            if (parsedAnswer == solution)
            {
                Debug.Log("Correct! Well done.");
                DisplayFeedback("Correct! Well done.", true);
                puzzleSolved = true;
                inputField.text = "";
                EndPuzzle();
                ResetPuzzleState();
            }
            else
            {
                Debug.Log("Incorrect. Try again.");
                DisplayFeedback("Incorrect. Try again.", false);
                inputField.text = "";
            }
        }
        else
        {
            Debug.Log("Invalid input. Please enter a number.");
            DisplayFeedback("Invalid input. Please enter a number.", false);
        }
    }

    public override void StartPuzzle()
    {
        // If the puzzle is already generated and unsolved, show the existing equation
        if (isPuzzleGenerated && !IsPuzzleSolved())
        {
            // Display the existing equation without regenerating it
            Debug.Log("Resuming unsolved equation: " + currentEquation);
            equationText.text = currentEquation;
            puzzleUI.SetActive(true);
            playerMovement.TogglePuzzleMode(true); // Disable movement/camera controls
            return;
        }

        // Otherwise, generate a new puzzle
        base.StartPuzzle();
    }

    public override void ResetPuzzleState()
    {
        isPuzzleGenerated = false;      // Reset to allow a new puzzle to be generated
        currentEquation = null;         // Clear the stored equation
        selectedOperator = "";          // Clear the selected operator
        a = 0;                          // Clear operand a
        b = 0;                          // Clear operand b
        equationText.text = "";         // Clear the displayed equation (optional, but useful for cleanliness)
        currentPuzzleType = "";
    }
    public override string GetCurrentPuzzleType()
    {
        return currentPuzzleType;
    }

    public override void SetEasyDifficulty()
    {
        a = Random.Range(1, 11);
        b = Random.Range(1, 11);
    }

    public override void SetMediumDifficulty()
    {
        a = Random.Range(-10, 51);
        b = Random.Range(-10, 51);
    }

    public override void SetHardDifficulty()
    {
        a = Random.Range(-100, 101);
        b = Random.Range(-100, 101);
    }
}