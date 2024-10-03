using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GeometricMeasurementsPuzzle : MathPuzzle
{
    public TextMeshProUGUI geometricText;
    private string currentQuestion;
    private int measurementType;
    private int length, width, height, radius, triangleBase;
    protected string currentPuzzleType;

    protected override void GeneratePuzzle()
    {
        measurementType = Random.Range(1, 4); // 1 = Perimeter, 2 = Area, 3 = Volume
        switch (measurementType)
        {
            case 1: // Perimeter
                int perimeterShape = (Random.Range(0, 3));

                if (perimeterShape == 0) // Rectangle
                {
                    length = Random.Range(5, 15);
                    width = Random.Range(5, 15);
                    currentQuestion = $"What is the perimeter of a rectangle with length {length} and width {width}?";
                }
                else if (perimeterShape == 1) // Square
                {
                    length = (Random.Range(5, 15));
                    currentQuestion = $"What is the perimeter of a square with side length {length}?";
                }
                else // Circle
                {
                    radius = Random.Range(5, 15);
                    currentQuestion = $"What is the circumference of a circle with radius {radius}?";
                }
                break;

            case 2: // Area
                int areaShape = Random.Range(0, 3); // 0 = Triangle, 1 = Rectangle, 2 = Square
                if (areaShape == 0) // Triangle
                {
                    triangleBase = Random.Range(5, 15);
                    height = Random.Range(5, 15);
                    currentQuestion = $"What is the area of a triangle with base {triangleBase} and height {height}?";
                }
                else if (areaShape == 1) // Rectangle
                {
                    length = Random.Range(5, 15);
                    width = Random.Range(5, 15);
                    currentQuestion = $"What is the area of a rectangle with length {length} and width {width}?";
                }
                else // Square
                {
                    length = Random.Range(5, 15);
                    currentQuestion = $"What is the area of a square with side length {length}?";
                }
                break;

            case 3: // Volume
                int volumeShape = Random.Range(0, 3); // 0 = Cube, 1 = Cylinder, 2 = Rectangle
                if (volumeShape == 0) // Cube
                {
                    length = Random.Range(5, 15);
                    currentQuestion = $"What is the volume of a cube with side length {length}?";

                }
                else if (volumeShape == 1) // Cylinder
                {
                    radius = Random.Range(5, 15);
                    height = Random.Range(5, 15);
                    currentQuestion = $"What is the volume of a cylinder with radius {radius} and height {height}?";
                }
                else // Rectangle
                {
                    length = Random.Range(5, 15);
                    width = Random.Range(5, 15);
                    height = Random.Range(5, 15);
                    currentQuestion = $"What is the volume of a rectangular prism with length {length}, width {width} and height {height}?";
                }
                break;
        }
        geometricText.text = currentQuestion;
        isPuzzleGenerated = true;
    }

    protected override void CheckAnswer(string userAnswer)
    {
        if (float.TryParse(userAnswer, out float parsedAnswer))
        {
            float correctAnswer = 0;

            switch (measurementType)
            {
                case 1: // Perimeter
                        // Determine which shape it was
                    int perimeterShape = (currentQuestion.Contains("rectangle")) ? 0 :
                                         (currentQuestion.Contains("square")) ? 1 : 2; // 2 for circle

                    if (perimeterShape == 0) // Rectangle
                    {
                        correctAnswer = 2 * (length + width);
                    }
                    else if (perimeterShape == 1) // Square
                    {
                        correctAnswer = 4 * length;
                    }
                    else // Circle
                    {
                        correctAnswer = (float)(2 * Mathf.PI * radius);
                    }
                    break;

                case 2: // Area
                    int areaShape = (currentQuestion.Contains("triangle")) ? 0 :
                                    (currentQuestion.Contains("rectangle")) ? 1 : 2; // 2 for square
                    if (areaShape == 0) // Triangle
                    {
                        correctAnswer = (triangleBase * height) / 2;
                    }
                    else if (areaShape == 1) // Rectangle
                    {
                        correctAnswer = length * width;
                    }
                    else // Square
                    {
                        correctAnswer = length * length;
                    }
                    break;

                case 3: // Volume
                    int volumeShape = (currentQuestion.Contains("cube")) ? 0 :
                                      (currentQuestion.Contains("cylinder")) ? 1 : 2; // 2 for rectangular prism
                    if (volumeShape == 0) // Cube
                    {
                        correctAnswer = length * length * length;
                    }
                    else if (volumeShape == 1) // Cylinder
                    {
                        correctAnswer = (float)(Mathf.PI * radius * radius * height);
                    }
                    else // Rectangular Prism
                    {
                        correctAnswer = length * width * height;
                    }
                    break;
            }

            correctAnswer = Mathf.Round(correctAnswer * 100f) / 100f;
            float roundedParsedAnswer = Mathf.Round(parsedAnswer * 100f) / 100f;

            // Log the answers for debugging
            Debug.Log($"Parsed Answer: {roundedParsedAnswer}, Correct Answer: {correctAnswer}");

            // Using a tolerance for floating point comparison
            float tolerance = 0.01f;
            if (Mathf.Abs(roundedParsedAnswer - correctAnswer) < tolerance)
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
                Debug.Log($"Expected: {correctAnswer} | Got: {roundedParsedAnswer}");
                inputField.text = "";
            }
        }
        else
        {
            Debug.Log("Invalid input. Please enter a valid number.");
        }
    }

    public override void ResetPuzzleState()
    {
        if (puzzleSolved || !isPuzzleGenerated)
        {
            isPuzzleGenerated = false;
            puzzleSolved = false;
            currentQuestion = "";
            length = 0;
            width = 0;
            height = 0;
            radius = 0;
            triangleBase = 0;
            measurementType = 0;
            geometricText.text = "";
        }
    }

    public override void StartPuzzle()
    {
        if (isPuzzleGenerated && !IsPuzzleSolved())
        {
            Debug.Log("Resuming unsolved Geometric puzzle.");
            geometricText.text = currentQuestion;
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

}