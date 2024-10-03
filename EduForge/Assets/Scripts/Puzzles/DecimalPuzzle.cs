using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DecimalPuzzle : MathPuzzle
{
    public TextMeshProUGUI decimalText;

    // To store puzzle data 
    private float solution;             // Store the solution
    private string currentEquation;     // Store the equation text
    private string selectedOperator;    // Store the selected operator
    private float a, b;                 // Store the operands
    protected string currentPuzzleType;

    protected override void GeneratePuzzle()
    {
        a = Random.Range(0.1f, 10.0f);
        b = Random.Range(0.1f, 10.0f);
        string[] operators = { "+", "-", "*" };
        selectedOperator = operators[Random.Range(0, operators.Length)];
        currentPuzzleType = "Decimal";

        switch (selectedOperator)
        {
            case "+":
                solution = a + b;
                break;
            case "-":
                solution = a - b;
                break;
            case "*":
                solution = a * b;
                break;
        }
        currentEquation = $"{a:F2} {selectedOperator} {b:F2} = ?";
        decimalText.text = currentEquation;

        Debug.Log($"Generated equation: {currentEquation}");
        Debug.Log($"The correct answer is: {solution:F2}");

        isPuzzleGenerated = true;
    }

    protected override void CheckAnswer(string userAnswer)
    {
        if (float.TryParse(userAnswer, out float parsedAnswer))
        {

            float roundedSolution = Mathf.Round(solution * 100f) / 100f;
            float roundedParsedAnswer = Mathf.Round(parsedAnswer * 100f) / 100f;

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
            isPuzzleGenerated = false;      // Reset to allow a new puzzle to be generated
            solution = 0;                   // Clear the solution, float is non-nullable
            selectedOperator = "";          // Clear the selected operator
            a = 0;                          // Clear operand a
            b = 0;                          // Clear operand b
            currentEquation = "";           // Clear the equation text
            decimalText.text = "";          // Clear the displayed equation
        }
    }

    public override void StartPuzzle()
    {
        if (isPuzzleGenerated && !IsPuzzleSolved())
        {
            Debug.Log("Resuming unsolved decimal puzzle.");
            decimalText.text = currentEquation;
            puzzleUI.SetActive(true);
            playerMovement.TogglePuzzleMode(true); // Disable movement/camera controls
            return;
        }
        
        // Otherwise, generate a new puzzle
        base.StartPuzzle();
    }
    public override string GetCurrentPuzzleType()
    {
        return currentPuzzleType;
    }
}
