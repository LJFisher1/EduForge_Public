using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PEMDASPuzzle : MathPuzzle
{
    public TextMeshProUGUI pemdasText;

    // To store puzzle data
    private float solution;           // Store the solution
    private string currentEquation; // Store the equation text
    private float a, b, c, d, e;         // Store the operands
    protected string currentPuzzleType;

    protected override void GeneratePuzzle()
    {
        a = Mathf.Round(Random.Range(1, 11));
        b = Mathf.Round(Random.Range(1, 11));
        c = Mathf.Round(Random.Range(1, 11));
        d = Mathf.Round(Random.Range(1, 11));
        e = Mathf.Round(Random.Range(1, 11));

        // Randomly select the structure of the equation based on PEMDAS rules 
        int structureType = Random.Range(0, 6); // Expand if you have more equation structures added
        currentPuzzleType = "PEMDAS";

        switch (structureType)
        {
            case 0: // (A + B) * (C - D)
                currentPuzzleType += ": BPM";
                solution = (a + b) * (c - d);
                currentEquation = $"({a} + {b}) * ({c} - {d}) = ?";
                break;

            case 1: // A * B + C / D
                currentPuzzleType += ": MD";
                solution = a * b + c / d;
                currentEquation = $"{a} * {b} + {c} / {d} = ?";
                break;

            case 2: // A + B * C - D
                currentPuzzleType += ": AMS";
                solution = a + b * c - d;
                currentEquation = $"{a} + {b} * {c} - {d} = ?";
                break;

            case 3: // A + (B * C) - (D / E)
                currentPuzzleType += ": PMS";
                solution = a + (b * c) - (d / e);
                currentEquation = $"{a:F2} + ({b:F2} * {c:F2}) - ({d:F2} / {e:F2}) = ?";
                break;

            case 4: // (A + B) / (C - D) * E
                if (c - d == 0)
                {
                    c = d + 1.0f;
                }
                currentPuzzleType += ": DMS";
                solution = (a + b) / (c - d) * e;
                currentEquation = $"({a:F2} + {b:F2}) / ({c:F2} - {d:F2}) * {e:F2} = ?";
                break;

            case 5: // A * (B + C) - (D + E)
                currentPuzzleType += ": NMA";
                solution = a * (b + c) - (d + e);
                currentEquation = $"{a:F2} * ({b:F2} + {c:F2}) - ({d:F2} + {e:F2}) = ?";
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
}

