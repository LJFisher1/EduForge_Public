using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatisticsPuzzle : MathPuzzle
{
    public TextMeshProUGUI statisticsText;     // UI to display the number
    List<int> numbers = new List<int>();       // To store the numbers
    private string currentQuestion;            // Store the equation text

    private string[] puzzleTypes = { "Mean", "Median", "Range" }; // "Mode" is another type but with only 5 elements, the chances of Mode being applicable are almost zero
    private string currentPuzzleType;

    protected override void GeneratePuzzle()
    {
        for (int i = 0; i < 5; i++)
        {
            numbers.Add(Random.Range(1, 101));
        }

        currentPuzzleType = puzzleTypes[Random.Range(0, puzzleTypes.Length)];

        switch (currentPuzzleType)
        {
            case "Mean":
                currentQuestion = $"Calculate the mean of the numbers: {string.Join(", ", numbers)}";
                break;

            case "Median":  // Adjust in the future if elements changes from 5
                currentQuestion = $"Calculate the median of the numbers: {string.Join(", ", numbers)}";
                break;

            case "Range":
                currentQuestion = $"Find the range of the numbers: {string.Join(", ", numbers)}";
                break;
        }

        statisticsText.text = currentQuestion;

        isPuzzleGenerated = true;
    }

    public override void ResetPuzzleState()
    {
        if (puzzleSolved || !isPuzzleGenerated)
        {
            isPuzzleGenerated = false;
            numbers.Clear();
            currentQuestion = "";
            statisticsText.text = "";
        }
    }

    protected override void CheckAnswer(string userAnswer)
    {
        if (float.TryParse(userAnswer, out float parsedAnswer))
        {
            float correctAnswer = 0;

            switch (currentPuzzleType)
            {
                case "Mean":
                    correctAnswer = (float)(Math.Round(numbers.Average(), 2));
                    break;

                case "Median":
                    numbers.Sort();
                    correctAnswer = numbers[2];
                    break;

                case "Range":
                    int minVal = numbers.Min();
                    int maxVal = numbers.Max();
                    correctAnswer = maxVal - minVal;
                    break;
            }

            Debug.Log($"User's answer: {parsedAnswer}");
            Debug.Log($"Correct {currentPuzzleType}: {correctAnswer}");

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
        if (isPuzzleGenerated && IsPuzzleSolved())
        {
            Debug.Log("Resuming unsolved Statistics puzzle.");
            statisticsText.text = currentQuestion;
            puzzleUI.SetActive(true);
            playerMovement.TogglePuzzleMode(true);
            return;
        }

        base.StartPuzzle();
    }
}
