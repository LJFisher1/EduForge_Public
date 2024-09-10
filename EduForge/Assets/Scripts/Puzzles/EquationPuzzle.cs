using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquationPuzzle : MathPuzzle
{
    public TextMeshProUGUI equationText;
    private int solution;

    protected override void GeneratePuzzle()
    {
        int a = Random.Range(1, 20); // Random #1 1-20
        int b = Random.Range(1, 20); // Random #2 1-20
        string[] operators = { "+", "-", "*" }; // Array of operators
        string selectedOperator = operators[Random.Range(0, operators.Length)]; // Randomly chooses an operator

        // Calculate the solution based on operator
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
        equationText.text = $"{a} {selectedOperator} {b} = ?"; // Display the generation equation
    }

    protected override void CheckAnswer()
    {
        if (int.TryParse(inputField.text, out int userAnswer))
        {
            if (userAnswer == solution)
            {
                Debug.Log("Correct! Well done.");
                puzzleSolved = true; // Mark puzzle as solved
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
