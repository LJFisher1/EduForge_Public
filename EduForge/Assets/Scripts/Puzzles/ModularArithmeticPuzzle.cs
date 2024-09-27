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

    protected override void GeneratePuzzle()
    {
        a = Random.Range(1, 20);
        b = Random.Range(1, 20);
        x = Random.Range(1, 50);
        y = Random.Range(1, 50);

        string[] puzzleTypes = { "Basic Modulo", "Equivalence", "Word Problem" };

        if (b >= a)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        puzzleType = puzzleTypes[Random.Range(0, puzzleTypes.Length)];

        switch (puzzleType)
        {
            case "Basic Modulo":
                currentEquation = $"What is {a} % {b}?";
                break;

            case "Equivalence":
                currentEquation = $"Are {x} and {y} congruent under modulo {b}? (yes/no)";
                break;

            case "Word Problem":
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
            Debug.Log("Resuming unsolved decimal puzzle.");
            modularText.text = currentEquation;
            puzzleUI.SetActive(true);
            playerMovement.TogglePuzzleMode(true); // Disable movement/camera controls
            return;
        }
        base.StartPuzzle();
    }

}


