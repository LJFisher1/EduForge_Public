using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PercentagePuzzle : MathPuzzle
{
    public TextMeshProUGUI percentageText;

    // To store puzzle data
    private float solution;             // Store the solution
    private string currentQuestion;     // Store the equation text
    private string puzzleType;          // Store the operation
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
       

        string[] puzzleTypes = { "Percentage Of", "Percentage Discount", "Percentage Ratio", "Percentage Increase", "Percentage Decrease" };
        puzzleType = puzzleTypes[Random.Range(0, puzzleTypes.Length)];
        currentPuzzleType = "Percentage";

        switch (puzzleType)
        {
            case "Percentage Of":
                currentPuzzleType += ": Percentage Of";
                solution = (a / 100f) * b;
                currentQuestion = $"What is {a:F2}% of {b:F2}?";
                break;

            case "Percentage Discount":
                currentPuzzleType += ": Percentage Discount";
                solution = b - (a / 100f) * b;
                currentQuestion = $"{a:F2}% off of {b:F2} is?";
                break;

            case "Percentage Ratio":
                currentPuzzleType += ": Percentage Ratio";
                solution = (a / b) * 100f;
                currentQuestion = $"What percentage is {a:F2} of {b:F2}?";
                break;

            case "Percentage Increase":
                currentPuzzleType += ": Percentage Increase";
                float newValueIncrease = b + Random.Range(10f, 100f);  // New value for increase
                solution = ((newValueIncrease - b) / b) * 100f;
                currentQuestion = $"If a value increases from {b:F2} to {newValueIncrease:F2}, what is the percentage increase?";
                break;

            case "Percentage Decrease":
                currentPuzzleType += ": Percentage Decrease";
                float newValueDecrease = b - Random.Range(10f, b - 10f);  // New value for decrease
                solution = ((b - newValueDecrease) / b) * 100f;
                currentQuestion = $"If a value decreases from {b:F2} to {newValueDecrease:F2}, what is the percentage decrease?";
                break;
        }

        percentageText.text = currentQuestion;

        Debug.Log($"Generated question: {currentQuestion}");
        Debug.Log($"The correct answer is: {solution:F2}");

        isPuzzleGenerated = true;
    }

    protected override void CheckAnswer(string userAnswer)
    {
        if (float.TryParse(userAnswer, out float parsedAnswer))
        {
            float roundedSolution = Mathf.Round(solution * 100f) / 100f;
            float roundedParsedAnswer = Mathf.Round(parsedAnswer * 100f) / 100f;

            Debug.Log($"Rounded Parsed Answer: {roundedParsedAnswer}");
            Debug.Log($"Rounded Correct Answer: {roundedSolution}");

            if (roundedSolution == roundedParsedAnswer)
            {
                Debug.Log("Correct! Well done.");
                puzzleSolved = true;
                inputField.text = "";
                EndPuzzle();
                ResetPuzzleState();
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


    public override void ResetPuzzleState()
    {
        if (puzzleSolved || !isPuzzleGenerated)
        {
            isPuzzleGenerated = false;
            solution = 0;
            puzzleType = "";
            a = 0;
            b = 0;
            currentQuestion = "";
            percentageText.text = "";
            currentPuzzleType = "";
        }
    }

    public override void StartPuzzle()
    {
        if (isPuzzleGenerated && !IsPuzzleSolved())
        {
            Debug.Log("Resuming unsolved percentage puzzle.");
            percentageText.text = currentQuestion;
            puzzleUI.SetActive(true);
            playerMovement.TogglePuzzleMode(true); // Disable movement/camera controls
            return;
        }

        base.StartPuzzle();
    }
    public override string GetCurrentPuzzleType()
    {
        return currentPuzzleType;
    }
    public override void SetEasyDifficulty()
    {
        a = Mathf.Round(Random.Range(1.0f, 50.0f));     // Whole numbers for percentages
        b = Mathf.Round(Random.Range(10.0f, 100.0f));   // Whole numbers for value
    }

    public override void SetMediumDifficulty()
    {
        a = Mathf.Round(Random.Range(1.0f, 100.0f) * 10f) / 10f;
        b = Mathf.Round(Random.Range(100.0f, 500.0f) * 10f) / 10f;
    }

    public override void SetHardDifficulty()
    {
        a = Mathf.Round(Random.Range(0.1f, 100.0f) * 100f) / 100f;
        b = Mathf.Round(Random.Range(500.0f, 1000.0f) * 100f) / 100f;
    }
}

