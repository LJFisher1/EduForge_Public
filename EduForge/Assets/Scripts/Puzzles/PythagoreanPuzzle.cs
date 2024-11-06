using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PythagoreanPuzzle : MathPuzzle
{
    public TextMeshProUGUI pythagoreanText;

    // To store puzzle data 
    private double solution;               // Store the solution (hypotenuse)
    private string currentTheorem;     // Store the pythagorean theorum text
    private int a, b;                   // Store the operands
    private double c;
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

            currentPuzzleType = "Pythagorean Theorum";

            c = Math.Round(Math.Sqrt(a * a + b * b), 2);
            int missingVariable = UnityEngine.Random.Range(0, 3);

            switch (missingVariable)
            {
                case 0:
                    // Solve for a: a^2 + b^2 = c^2 becomes a = sqrt(c^2 - b^2)
                    currentPuzzleType += ": A";
                    solution = Math.Sqrt(c * c - b * b);  // Solve for a
                    currentTheorem = $"?^2 + {b}^2 = {c}^2";
                    break;

                case 1:
                    // Solve for b: a^2 + b^2 = c^2 becomes b = sqrt(c^2 - a^2)
                    currentPuzzleType += ": B";
                    solution = Math.Sqrt(c * c - a * a);  // Solve for b
                    currentTheorem = $"{a}^2 + ?^2 = {c}^2";
                    break;

                case 2:
                    // Solve for c: a^2 + b^2 = c^2
                    currentPuzzleType += ": C";
                    solution = c;
                    currentTheorem = $"{a}^2 + {b}^2 = ?^2";
                    break;
            }

            Debug.Log("Puzzle type set: " + currentPuzzleType);
            pythagoreanText.text = currentTheorem;
            isPuzzleGenerated = true;
        }
        else
        {
            // If the puzzle is already generated, reuse the existing equation
            pythagoreanText.text = currentTheorem;
        }
    }

    protected override void CheckAnswer(string userAnswer)
    {
        if (double.TryParse(userAnswer, out double parsedAnswer))
        {
            // Round both solution and parsedAnswer to two decimal places before comparison
            solution = Math.Round(solution, 2);
            parsedAnswer = Math.Round(parsedAnswer, 2);

            // Set tolerance for precision comparison
            double tolerance = 0.01;  // Small tolerance for comparison
            double difference = Math.Abs(solution - parsedAnswer);

            // Check if the answer is correct within the tolerance
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

    public override void StartPuzzle()
    {
        // If the puzzle is already generated and unsolved, show the existing equation
        if (isPuzzleGenerated && !IsPuzzleSolved())
        {
            // Display the existing equation without regenerating it
            Debug.Log("Resuming unsolved equation: " + currentTheorem);
            pythagoreanText.text = currentTheorem;
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
        currentTheorem = null;         // Clear the stored equation
        a = 0;                          // Clear operand a
        b = 0;                          // Clear operand b
        pythagoreanText.text = "";      // Clear the displayed equation (optional, but useful for cleanliness)
        currentPuzzleType = "";
    }
    public override string GetCurrentPuzzleType()
    {
        return currentPuzzleType;
    }

    public override void SetEasyDifficulty()
    {
        a = UnityEngine.Random.Range(1, 11); // Random number between 1 and 10
        b = UnityEngine.Random.Range(1, 11); // Random number between 1 and 10
    }

    public override void SetMediumDifficulty()
    {
        a = UnityEngine.Random.Range(10, 51); // Random number between 10 and 50
        b = UnityEngine.Random.Range(10, 51); // Random number between 10 and 50
    }

    public override void SetHardDifficulty()
    {
        a = UnityEngine.Random.Range(50, 101); // Random number between 50 and 100
        b = UnityEngine.Random.Range(50, 101); // Random number between 50 and 100
    }
}

