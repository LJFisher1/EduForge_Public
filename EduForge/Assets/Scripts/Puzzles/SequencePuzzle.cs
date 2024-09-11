using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SequencePuzzle : MathPuzzle
{
    public TextMeshProUGUI sequenceText;
    private int nextValueInSequence;

    protected override void GeneratePuzzle()
    {
        string[] patternTypes = { "Arithmetic", "Geometric", "Fibonacci" };
        string selectedPattern = patternTypes[Random.Range(0, patternTypes.Length)];
        //string selectedPattern = "Fibonacci"; // This is for debugging

        // Debug to show which puzzle was generated
        Debug.Log("Generating " + selectedPattern + " sequence.");

        List<int> sequence = new List<int>();

        switch (selectedPattern)
        {
            case "Arithmetic":
                GenerationArithmeticSequence(sequence);
                break;

            case "Geometric":
                GenerateGeometricSequence(sequence);
                break;

            case "Fibonacci":
                GenerateFibonacciSequence(sequence);
                break;
        }

        // Display the sequence to the player, hiding the last value
        sequenceText.text = string.Join(", ", sequence.GetRange(0, sequence.Count - 1)) + ", ?";
        Debug.Log(nextValueInSequence);
    }

    // Tested extensively
    private void GenerationArithmeticSequence(List<int> sequence)
    {
        // Sequence is either + or -
        int start = Random.Range(1, 20);
        int difference = Random.Range(1, 10);

        // Randomly choose between addition and subtraction
        bool useAddition = Random.Range(0, 2) == 0;

        Debug.Log($"Starting value: {start}, Difference: {difference}, Using Addition: {useAddition}");

        for (int i = 0; i < 5; i++) // Start occupies the 0 space, so its only adding 4 additional numbers even though it loops 5 times.
        {
            if (useAddition)
            {
                sequence.Add(start + i * difference);
            }
            else
            {
                sequence.Add(start - i * difference);
            }
        }

        // The next value in the sequence
        if (useAddition)
        {
            nextValueInSequence = start + 4 * difference;
        }
        else
        {
            nextValueInSequence = start - 4 * difference;
        }

        Debug.Log($"Next Value in Sequence: {nextValueInSequence}");
    }

    // Tested extensively
    private void GenerateGeometricSequence(List<int> sequence)
    {
        // Sequence is * or /
        int start = Random.Range(20, 100); // Starting value
        int ratio = Random.Range(2, 5); // Common ratio (integer)

        bool useMultiplication = Random.Range(0, 2) == 0;

        // Initialize sequence with starting value
        sequence.Clear(); // Clear any invalid values from previous generations
        sequence.Add(start);

        Debug.Log($"Starting value: {start}, Ratio: {ratio}, Using Multiplication: {useMultiplication}");

        for (int i = 0; i < 4; i++) // Generate 4 additional values to make 5 in total
        {
            if (useMultiplication)
            {
                start *= ratio; // Multiply by the ratio
            }
            else
            {
                if (start % ratio == 0)
                {
                    start /= ratio; // Ensure division won't create fractions
                }
                else
                {
                    // If division would result in a fraction, retry with a new sequence
                    GenerateGeometricSequence(sequence); // Recursively generate a new sequence
                    return; // Exit to avoid adding incorrect values

                }
            }
            sequence.Add(start);
            Debug.Log($"Value at step {i + 1}: {start}");
        }
        nextValueInSequence = start;
    }

    private void GenerateFibonacciSequence(List<int> sequence)
    {
        int a = Random.Range(0, 10);  // Random starting value for the sequence
        int b = Random.Range(1, 10);  // Random second value for the sequence
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

    protected override void CheckAnswer()
    {
        if (int.TryParse(inputField.text, out int userAnswer))
        {
            if (userAnswer == nextValueInSequence)
            {
                Debug.Log("Correct! Well done.");
                puzzleSolved = true;
                inputField.text = "";
                EndPuzzle();
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

}
