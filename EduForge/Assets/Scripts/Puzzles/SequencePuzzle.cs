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

    // Values used in puzzles
    int initialValue = 0;
    int stepValue = 0;
    int firstValue = 0;
    int secondValue = 0;

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

        string[] patternTypes = { "Arithmetic", "Geometric", "Fibonacci" };
        string selectedPattern = patternTypes[Random.Range(0, patternTypes.Length)];
        currentPuzzleType = "Sequence";

        Debug.Log("Generating " + selectedPattern + " sequence.");
        currentSequence.Clear();


        switch (selectedPattern)
        {
            case "Arithmetic":
                currentPuzzleType += ": Arithmetic";
                GenerationArithmeticSequence(currentSequence);
                break;

            case "Geometric":
                currentPuzzleType += ": Geometric";
                GenerateGeometricSequence(currentSequence);
                break;

            case "Fibonacci":
                currentPuzzleType += ": Fibonacci";
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
        bool useAddition = Random.Range(0, 2) == 0;

        for (int i = 0; i < 5; i++)
        {
            sequence.Add(useAddition ? initialValue + i * stepValue : initialValue - i * stepValue);
        }

        nextValueInSequence = useAddition ? initialValue + 4 * stepValue : initialValue - 4 * stepValue;
    }

    private void GenerateGeometricSequence(List<int> sequence)
    {
        bool useMultiplication = Random.Range(0, 2) == 0;

        sequence.Add(initialValue);
        for (int i = 0; i < 4; i++)
        {
            if (useMultiplication)
            {
                initialValue *= stepValue;
            }
            else
            {
                if (initialValue % stepValue == 0)
                {
                    initialValue /= stepValue;
                }
                else
                {
                    GenerateGeometricSequence(sequence);
                }
            }
            sequence.Add(initialValue);
        }
        nextValueInSequence = initialValue;
    }

    private void GenerateFibonacciSequence(List<int> sequence)
    {
        int nextValue = 0;

        sequence.Add(firstValue);
        sequence.Add(secondValue);

        for (int i = 2; i < 5; i++)
        {
            nextValue = firstValue + secondValue;
            sequence.Add(nextValue);
            firstValue = secondValue;
            secondValue = nextValue;
        }
        nextValueInSequence = nextValue;
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
        currentPuzzleType = "";
    }

    public override string GetCurrentPuzzleType()
    {
        return currentPuzzleType;
    }

    public override void SetEasyDifficulty()
    {
        initialValue = Random.Range(1, 11);
        stepValue = Random.Range(1, 6);
        firstValue = Random.Range(1, 6);
        secondValue = Random.Range(1, 6);
    }

    public override void SetMediumDifficulty()
    {
        initialValue = Random.Range(10, 31);
        stepValue = Random.Range(5, 11);
        firstValue = Random.Range(5, 16);
        secondValue = Random.Range(5, 16);
    }

    public override void SetHardDifficulty()
    {
        // With the multiplication, anything above these initial/step values
        // exceeds the max 32-bit int limit
        initialValue = Random.Range(10, 31);
        stepValue = Random.Range(10, 16);
        firstValue = Random.Range(10, 31);
        secondValue = Random.Range(10, 31);
    }
}