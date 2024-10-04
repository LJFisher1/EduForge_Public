using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SequencePuzzle : MathPuzzle
{
    public TextMeshProUGUI sequenceText;
    protected string currentPuzzleType; // To store the current puzzle type for the Textbook

    // To store puzzle data 
    private int nextValueInSequence; // The next value in the sequence (also the solution)
    private List<int> currentSequence = new List<int>(); // To store the generated sequence

    protected override void GeneratePuzzle()
    {
        string[] patternTypes = { "Arithmetic", "Geometric", "Fibonacci" };
        string selectedPattern = patternTypes[Random.Range(0, patternTypes.Length)];
        currentPuzzleType = "Sequence";

        Debug.Log("Generating " + selectedPattern + " sequence.");
        currentSequence.Clear();

        switch (selectedPattern)
        {
            case "Arithmetic":
                GenerationArithmeticSequence(currentSequence);
                break;

            case "Geometric":
                GenerateGeometricSequence(currentSequence);
                break;

            case "Fibonacci":
                GenerateFibonacciSequence(currentSequence);
                break;
        }

        // Display the sequence to the player, hiding the last value
        sequenceText.text = string.Join(", ", currentSequence.GetRange(0, currentSequence.Count - 1)) + ", ?";
        Debug.Log(nextValueInSequence);

        isPuzzleGenerated = true;
    }

    private void GenerationArithmeticSequence(List<int> sequence)
    {
        int start = Random.Range(1, 20);
        int difference = Random.Range(1, 10);
        bool useAddition = Random.Range(0, 2) == 0;

        for (int i = 0; i < 5; i++)
        {
            sequence.Add(useAddition ? start + i * difference : start - i * difference);
        }

        nextValueInSequence = useAddition ? start + 4 * difference : start - 4 * difference;
    }

    private void GenerateGeometricSequence(List<int> sequence)
    {
        int start = Random.Range(20, 100);
        int ratio = Random.Range(2, 5);
        bool useMultiplication = Random.Range(0, 2) == 0;

        sequence.Add(start);
        for (int i = 0; i < 4; i++)
        {
            if (useMultiplication)
            {
                start *= ratio;
            }
            else
            {
                if (start % ratio == 0)
                {
                    start /= ratio;
                }
                else
                {
                    GenerateGeometricSequence(sequence);
                    return;
                }
            }
            sequence.Add(start);
        }
        nextValueInSequence = start;
    }

    private void GenerateFibonacciSequence(List<int> sequence)
    {
        int a = Random.Range(0, 10);
        int b = Random.Range(1, 10);
        int next = 0;

        sequence.Add(a);
        sequence.Add(b);

        for (int i = 2; i < 5; i++)
        {
            next = a + b;
            sequence.Add(next);
            a = b;
            b = next;
        }
        nextValueInSequence = next;
    }

    protected override void CheckAnswer(string userAnswer)
    {
        if (int.TryParse(userAnswer, out int parsedAnswer))
        {
            if (parsedAnswer == nextValueInSequence)
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

    public override void StartPuzzle()
    {
        if (isPuzzleGenerated && !IsPuzzleSolved())
        {
            if (currentSequence.Count > 0)
            {
                // Only display the sequence if it's not empty
                Debug.Log("Resuming unsolved sequence.");
                sequenceText.text = string.Join(", ", currentSequence.GetRange(0, currentSequence.Count - 1)) + ", ?";
            }
            else
            {
                // Handle the case where there is no sequence to display
                Debug.LogWarning("Current sequence is empty. Generating a new puzzle.");
                GeneratePuzzle();
            }

            puzzleUI.SetActive(true);
            playerMovement.TogglePuzzleMode(true); // Disable movement/camera controls
            return;
        }

        // Otherwise, generate a new puzzle
        base.StartPuzzle();
    }


    public override void ResetPuzzleState()
    {
        isPuzzleGenerated = false;  // Reset to allow a new puzzle to be generated
        nextValueInSequence = 0;    // Clear the next value in the sequence
        currentSequence.Clear();    // Clear the stored sequence
        sequenceText.text = "";     // Clear the displayed sequence
    }

    public override string GetCurrentPuzzleType()
    {
        return currentPuzzleType;
    }
}