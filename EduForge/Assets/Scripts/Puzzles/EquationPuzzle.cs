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
        int a = Random.Range(1, 20);
        int b = Random.Range(1, 20);
        string[] operators = { "+", "-", "*" };
        string selectedOperator = operators[Random.Range(0, operators.Length)];

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

        equationText.text = $"{a} {selectedOperator} {b} = ?";
    }

    protected override void CheckAnswer(string userAnswer)
    {
        if (int.TryParse(userAnswer, out int parsedAnswer))
        {
            if (parsedAnswer == solution)
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