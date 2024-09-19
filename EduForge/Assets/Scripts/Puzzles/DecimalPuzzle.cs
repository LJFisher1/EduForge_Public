using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DecimalPuzzle : MathPuzzle
{
    public TextMeshProUGUI decimalText;
    private float solution;

    protected override void GeneratePuzzle()
    {
        float a = Random.Range(0.1f, 10.0f);
        float b = Random.Range(0.1f, 10.0f);
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
        Debug.Log($"Generated equation: {a:F2} {selectedOperator} {b:F2} = ?");
        Debug.Log($"The correct answer is: {solution:F2}");

        decimalText.text = $"{a:F2} {selectedOperator} {b:F2} = ?";
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
        throw new System.NotImplementedException();
    }
}
