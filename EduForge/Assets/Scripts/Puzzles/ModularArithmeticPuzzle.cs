using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;

public class ModularArithmeticPuzzle : MathPuzzle
{
    public TextMeshProUGUI modularText;
    private string currentEquation;
    private string puzzleType;
    private int a, b, x, y;
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

        string[] puzzleTypes = { "Basic Modulo", "Equivalence", "Word Problem" };

        if (b >= a)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        puzzleType = puzzleTypes[Random.Range(0, puzzleTypes.Length)];
        currentPuzzleType = "Modular Arithmetic";

        switch (puzzleType)
        {
            case "Basic Modulo":
                currentPuzzleType += ": Basic Modulo";
                currentEquation = $"What is {a} % {b}?";
                break;

            case "Equivalence":
                currentPuzzleType += ": Equivalence";
                currentEquation = $"Are {x} and {y} congruent under modulo {b}? (yes/no)";
                break;

            case "Word Problem":
                currentPuzzleType += ": Word Problem";
                currentEquation = $"You have {a} items and {b} boxes. How many items are left after distributing evenly?";
                break;
        }

        modularText.text = currentEquation;

        isPuzzleGenerated = true;

    }
    public override void ResetPuzzleState()
    {
        if (puzzleSolved || !isPuzzleGenerated)
        {
            isPuzzleGenerated = false;
            a = 0;
            b = 0;
            x = 0;
            y = 0;
            currentEquation = "";
            modularText.text = "";
            currentPuzzleType = "";
        }
    }

    protected override void CheckAnswer(string userAnswer)
    {
        if (int.TryParse(userAnswer, out int parsedAnswer))
        {
            int correctAnswer = 0;

            switch (puzzleType)
            {
                case "Basic Modulo":
                    correctAnswer = a % b;
                    break;

                case "Equivalence":
                    correctAnswer = (x % b == y % b) ? 1 : 0;
                    break;

                case "Word Problem":
                    correctAnswer = a % b;
                    break;
            }

            if (parsedAnswer == correctAnswer)
            {
                Debug.Log("Correct! The answer is correct.");
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
            Debug.Log("Invalid input. Please enter a valid number.");
        }

    }

    public override void StartPuzzle()
    {
        if (isPuzzleGenerated && !IsPuzzleSolved())
        {
            Debug.Log("Resuming unsolved modular arithmetic puzzle.");
            modularText.text = currentEquation;
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
        a = Random.Range(1, 11);
        b = Random.Range(1, 11);
        x = Random.Range(1, 21);
        y = Random.Range(1, 21);

    }

    public override void SetMediumDifficulty()
    {
        a = Random.Range(1, 31);
        b = Random.Range(1, 31);
        x = Random.Range(1, 51);
        y = Random.Range(1, 51);
    }

    public override void SetHardDifficulty()
    {
        a = Random.Range(20, 101);
        b = Random.Range(20, 101);
        x = Random.Range(1, 101);
        y = Random.Range(1, 101);
    }
}


