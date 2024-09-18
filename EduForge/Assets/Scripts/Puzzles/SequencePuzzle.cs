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