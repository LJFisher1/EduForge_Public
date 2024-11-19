using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PrimeNumbersPuzzle : MathPuzzle
{
    public TextMeshProUGUI primeNumberText;     // UI to display the number
    private int currentNumber;                  // The number to check
    public bool isPrime;                       // To store if the number is prime or not
    private List<int> primeFactors;             // To store the puzzle factors
    private string currentQuestion;             // Store the question text
    protected string currentPuzzleType;
    private string puzzleType;
    private string[] puzzleTypes = { "IsPrime", "NextPrime", "PrimeFactorization" };


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

        currentPuzzleType = "PrimeNumber";
        puzzleType = puzzleTypes[Random.Range(0, puzzleTypes.Length)];

        switch (puzzleType)
        {
            case "IsPrime":
                currentPuzzleType += ": IsPrime";
                isPrime = IsPrime(currentNumber);
                currentQuestion = $"Is {currentNumber} a prime number? (Yes/No)";
                Debug.Log($"Generated number: {currentNumber}");
                Debug.Log($"Is prime: {isPrime}");
                break;

            case "NextPrime":
                currentPuzzleType += ": NextPrime";
                int nextPrime = FindNextPrime(currentNumber);
                currentQuestion = $"What is the next prime number after {currentNumber}?";
                Debug.Log($"Generated number: {currentNumber}, Next Prime: {nextPrime}");
                break;

            case "PrimeFactorization":
                currentPuzzleType += ": PrimeFactorization";
                primeFactors = GetPrimeFactors(currentNumber);
                currentQuestion = $"What are the prime factors of {currentNumber}? (Input in form X, Y, Z)";
                Debug.Log($"Generated number: {currentNumber}, Prime Factors: {string.Join(", ", primeFactors)}");
                break;
        }

        primeNumberText.text = currentQuestion;

        Debug.Log($"Generated number: {currentNumber}");
        Debug.Log($"Current Puzzle Type: {currentPuzzleType}");
        Debug.Log($"Is prime: {isPrime}");

        isPuzzleGenerated = true;
    }

    bool IsPrime(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }
        return true;
    }

    int FindNextPrime(int number)
    {
        int nextNumber = number + 1;
        while (!IsPrime(nextNumber))
        {
            nextNumber++;
        }
        return nextNumber;
    }

    List<int> GetPrimeFactors(int number)
    {
        List<int> factors = new List<int>();
        for (int i = 2; i <= number; i++)
        {
            while (number % i == 0)
            {
                factors.Add(i);
                number /= i;
            }
        }
        return factors;
    }

    protected override void CheckAnswer(string userAnswer)
    {
        switch (puzzleType)
        {
            case "IsPrime":
                if (int.TryParse(userAnswer, out int parsedAnswer) && (parsedAnswer == 1 || parsedAnswer == 0))
                {
                    bool playerAnswerIsYes = parsedAnswer == 1;
                    bool correctAnswer = isPrime == playerAnswerIsYes;
                    puzzleSolved = HandlePrimeAnswer(correctAnswer);
                }
                else
                {
                    Debug.Log("Invalid input. Please enter 1 for Yes or 0 for No.");
                    DisplayFeedback("Invalid input.  Please enter 1 for Yes or 0 for No.", false);
                }
                break;

            case "NextPrime":
                if (int.TryParse(userAnswer, out int nextPrimeAnswer))
                {
                    int nextPrime = FindNextPrime(currentNumber);
                    puzzleSolved = HandlePrimeAnswer(nextPrimeAnswer == nextPrime);
                }
                else
                {
                    Debug.Log("Invalid input. Please enter a number.");
                    DisplayFeedback("Invalid input. Please enter a number.", false);
                }
                break;

            case "PrimeFactorization":
                string[] factorsInput = userAnswer.Split(',');
                List<int> parsedFactors = new List<int>();
                foreach (var factor in factorsInput)
                {
                    if (int.TryParse(factor.Trim(), out int factorValue))
                    {
                        parsedFactors.Add(factorValue);
                    }
                    else
                    {
                        Debug.Log("Invalid input. Please enter numbers separated by commas.");
                        DisplayFeedback("Invalid input. Please enter numbers separated by commas.", false);
                        return;
                    }
                }

                parsedFactors.Sort();
                primeFactors.Sort();

                bool isCorrect = parsedFactors.SequenceEqual(primeFactors);
                puzzleSolved = HandlePrimeAnswer(isCorrect);
                break;
        }
    }

    private bool HandlePrimeAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Correct! Well done.");
            DisplayFeedback("Correct! Well done.", true);
            inputField.text = "";
            EndPuzzle();
            ResetPuzzleState();
            return true;
        }
        else
        {
            Debug.Log("Incorrect. Try again.");
            DisplayFeedback("Incorrect. Try again.", false);
            inputField.text = "";
            return false;
        }
    }

    public override void ResetPuzzleState()
    {
        if (puzzleSolved || !isPuzzleGenerated)
        {
            isPuzzleGenerated = false;
            puzzleSolved = false;
            currentNumber = 0;
            primeFactors = null;
            primeNumberText.text = "";
            currentPuzzleType = "";
        }
    }

    public override void StartPuzzle()
    {
        if (isPuzzleGenerated && !IsPuzzleSolved())
        {
            Debug.Log("Resuming unsolved Prime Number puzzle.");
            primeNumberText.text = currentQuestion;
            puzzleUI.SetActive(true);
            playerMovement.TogglePuzzleMode(true);
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
        currentNumber = Random.Range(10, 51);

    }

    public override void SetMediumDifficulty()
    {
        currentNumber = Random.Range(20, 101);

    }

    public override void SetHardDifficulty()
    {
        currentNumber = Random.Range(50, 151);
    }
}

