using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquationPuzzle : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI equationText;
    public Button submitButton;

    private int solution;


    // Start is called before the first frame update
    private void Start()
    {
        GenerateEquation();
        submitButton.onClick.AddListener(CheckAnswer);
    }

    private void GenerateEquation()
    {
        int a = Random.Range(1, 20); // Random #1 1-20
        int b = Random.Range(1, 20); // Random #2 1-20
        string[] operators = { "+", "-", "*"}; // Array of operators
        string selectedOperator = operators[Random.Range(0, operators.Length)]; // Randomly chooses an operator

        // Calculate the solution based on operator
        switch(selectedOperator)
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

    private void CheckAnswer()
    {
        if (int.TryParse(inputField.text, out int userAnswer))
        {
            if (userAnswer == solution)
            {
                Debug.Log("Correct! Well done.");
            }
            else
            {
                Debug.Log("Incorrect. Try again.");
            }
        }
        else
        {
            Debug.Log("Invalid input. Please enter a number.");
        }
    }


}
