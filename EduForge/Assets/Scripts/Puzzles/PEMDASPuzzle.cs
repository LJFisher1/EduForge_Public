using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PEMDASPuzzle : MathPuzzle
{
    public TextMeshProUGUI pemdasText;

    // To store puzzle data
    private float solution;             // Store the solution
    private string currentEquation;     // Store the equation text
    private float a, b, c, d, e;        // Store the operands
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

        // Randomly select the structure of the equation based on PEMDAS rules 
        int structureType = Random.Range(0, 6); // Expand if you have more equation structures added
        currentPuzzleType = "PEMDAS";

        switch (structureType)
        {
            case 0: // (A + B) * (C - D)
                    // BPM (Basic Parentheses and Multiplication)
                currentPuzzleType += ": BPM";
                solution = (a + b) * (c - d);
                currentEquation = $"({a} + {b}) * ({c} - {d}) = ?";
                break;

            case 1: // A * B + C / D
                    // MD (Multiplication and Divison)
                currentPuzzleType += ": MD";
                solution = a * b + c / d;
                currentEquation = $"{a} * {b} + {c} / {d} = ?";
                break;

            case 2: // A + B * C - D
                    // AMS (Addition, Multiplication and Subtraction)
                currentPuzzleType += ": AMS";
                solution = a + b * c - d;
                currentEquation = $"{a} + {b} * {c} - {d} = ?";
                break;

            case 3: // A + (B * C) - (D / E)
                    // PMS (Parentheses with Multiplication and Subtraction)
                currentPuzzleType += ": PMS";
                solution = a + (b * c) - (d / e);
                currentEquation = $"{a} + ({b} * {c}) - ({d} / {e}) = ?";
                break;

            case 4: // (A + B) / (C - D) * E
                    // DMS (Divison and Multiplication of Sums)
                if (c - d == 0)
                {
                    c = d + 1.0f;
                }
                currentPuzzleType += ": DMS";
                solution = (a + b) / (c - d) * e;
                currentEquation = $"({a} + {b}) / ({c} - {d}) * {e} = ?";
                break;

            case 5: // A * (B + C) - (D + E)
                    // NMA (Nested Multiplication and Addition)
                currentPuzzleType += ": NMA";
                solution = a * (b + c) - (d + e);
                currentEquation = $"{a} * ({b} + {c}) - ({d} + {e}) = ?";
                break;
        }

        pemdasText.text = currentEquation;

        Debug.Log($"Generated equation: {currentEquation}");
        Debug.Log($"The correct answer is: {solution}");

        isPuzzleGenerated = true;
    }

    protected override void CheckAnswer(string userAnswer)
    {
        if (float.TryParse(userAnswer, out float parsedAnswer))
        {
            float roundedParsedAnswer = Mathf.Round(parsedAnswer * 100f) / 100f;
            float roundedSolution = Mathf.Round(solution * 100f) / 100f;

            Debug.Log($"Rounded Parsed Answer: {roundedParsedAnswer}");
            Debug.Log($"Rounded Correct Answer: {roundedSolution}");

            if (roundedSolution == roundedParsedAnswer)
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
            isPuzzleGenerated = false;
            solution = 0;
            a = 0;
            b = 0;
            c = 0;
            d = 0;
            e = 0;
            currentEquation = "";
            pemdasText.text = "";
            currentPuzzleType = "";
        }
    }

    public override void StartPuzzle()
    {
        if (isPuzzleGenerated && !IsPuzzleSolved())
        {
            Debug.Log("Resuming unsolved PEMDAS puzzle.");
            pemdasText.text = currentEquation;
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
        a = Mathf.Round(Random.Range(1.0f, 20.0f));
        b = Mathf.Round(Random.Range(1.0f, 10.0f));
        c = Mathf.Round(Random.Range(1.0f, 20.0f));
        d = Mathf.Round(Random.Range(1.0f, 10.0f));
        e = Mathf.Round(Random.Range(1.0f, 10.0f));
    }

    public override void SetMediumDifficulty()
    {
        a = Mathf.Round(Random.Range(10.0f, 50.0f));
        b = Mathf.Round(Random.Range(5.0f, 25.0f));
        c = Mathf.Round(Random.Range(10.0f, 50.0f));
        d = Mathf.Round(Random.Range(5.0f, 25.0f));
        e = Mathf.Round(Random.Range(2.0f, 15.0f));
    }

    public override void SetHardDifficulty()
    {
        a = Mathf.Round(Random.Range(50.0f, 100.0f));
        b = Mathf.Round(Random.Range(10.0f, 50.0f));
        c = Mathf.Round(Random.Range(50.0f, 100.0f));
        d = Mathf.Round(Random.Range(10.0f, 50.0f));
        e = Mathf.Round(Random.Range(5.0f, 25.0f));
    }
}

