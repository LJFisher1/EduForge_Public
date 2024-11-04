using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DecimalPuzzle : MathPuzzle
{
    public TextMeshProUGUI decimalText;

    // To store puzzle data 
    private float solution;             // Store the solution
    private string currentEquation;     // Store the equation text
    private string selectedOperator;    // Store the selected operator
    private float a, b;                 // Store the operands
    protected string currentPuzzleType;

    protected override void GeneratePuzzle()
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
        currentPuzzleType = "Decimal";

        switch (selectedOperator)
        {
            case "+":
                currentPuzzleType += ": +";
                solution = a + b;
                Debug.Log($"Solution: {solution}");
                break;
            case "-":
                currentPuzzleType += ": -";
                solution = a - b;
                Debug.Log($"Solution: {solution}");
                break;
            case "*":
                currentPuzzleType += ": *";
                solution = a * b;
                Debug.Log($"Solution: {solution}");
                break;
        }

        currentEquation = $"{a:F2} {selectedOperator} {b:F2} = ?";
        decimalText.text = currentEquation;



        isPuzzleGenerated = true;
    }

    protected override void CheckAnswer(string userAnswer)
    {
        if (float.TryParse(userAnswer, out float parsedAnswer))
        {
            solution = (solution * 100f) / 100f;
            parsedAnswer = (parsedAnswer * 100f) / 100f;
            float tolerance = 0.01f; // Small tolerance for comparison
            float difference = Mathf.Abs(solution - parsedAnswer);

            // Had an issue where the answer was 12.25 but the program thought 12.24, giving a false negative.
            if (difference <= tolerance)
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

    public override void ResetPuzzleState()
    {
        if (puzzleSolved || !isPuzzleGenerated)
        {
            isPuzzleGenerated = false;      // Reset to allow a new puzzle to be generated
            solution = 0;                   // Clear the solution, float is non-nullable
            selectedOperator = "";          // Clear the selected operator
            a = 0;                          // Clear operand a
            b = 0;                          // Clear operand b
            currentEquation = "";           // Clear the equation text
            decimalText.text = "";          // Clear the displayed equation
            currentPuzzleType = "";
        }
    }

    public override void StartPuzzle()
    {
        if (isPuzzleGenerated && !IsPuzzleSolved())
        {
            Debug.Log("Resuming unsolved decimal puzzle.");
            decimalText.text = currentEquation;
            puzzleUI.SetActive(true);
            playerMovement.TogglePuzzleMode(true); // Disable movement/camera controls
            return;
        }

        // Otherwise, generate a new puzzle
        base.StartPuzzle();
    }
    public override string GetCurrentPuzzleType()
    {
        return currentPuzzleType;
    }

    public override void SetEasyDifficulty()
    {
        a = Mathf.Round(Random.Range(0.1f, 20.0f) * 100f) / 100f;
        b = Mathf.Round(Random.Range(0.01f, 20.0f) * 100f) / 100f;
    }

    public override void SetMediumDifficulty()
    {
        a = Mathf.Round(Random.Range(1.0f, 100.0f) * 100f) / 100f;
        b = Mathf.Round(Random.Range(1.0f, 100.0f) * 100f) / 100f;
    }

    public override void SetHardDifficulty()
    {
        a = Mathf.Round(Random.Range(10.0f, 500.0f) * 100f) / 100f;
        b = Mathf.Round(Random.Range(10.0f, 500.0f) * 100f) / 100f;
    }
}
