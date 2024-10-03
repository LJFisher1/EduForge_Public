using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Subsystems;

public class TextbookSystem : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    // Class to hold information about each puzzle type
    public class PuzzleInfo
    {
        public string PuzzleName { get; set; }
        public string PuzzleDescription { get; set; }
        public List<Example> Examples { get; set; }

        public PuzzleInfo(string puzzleName, string puzzleDescription, List<Example> examples)
        {
            PuzzleName = puzzleName;
            PuzzleDescription = puzzleDescription;
            Examples = examples;
        }
    }

    // Class to hold example problems and their descriptions
    public class Example
    {
        public string Problem { get; set; }
        public string SolutionExpanation { get; set; }

        public Example(string problem, string solutionExpanation)
        {
            Problem = problem;
            SolutionExpanation = solutionExpanation;
        }
    }

    // Dictionary to store textbook data
    private Dictionary<string, PuzzleInfo> textbook = new Dictionary<string, PuzzleInfo>();

    private void Start()
    {
        InitializeTextbook();
    }

    public void InitializeTextbook()
    {
        textbook.Add("Equation",
            new PuzzleInfo(
            "Equations",
            "An equation is a mathematical statement that asserts the equality of two expressions.",
            new List<Example>
            {
                new Example("4x + 5 = 25", "To solve for x, subtract 5 from both sides, then divide by 4: x = 5")
            }
            ));
        //textbook.Add("Sequence",
        //    new PuzzleInfo(
        //        "Sequences",
        //        "")

    }

    public void DisplayPuzzleInfo(string puzzleType, TextMeshProUGUI displayText)
    {
        if (textbook.ContainsKey(puzzleType))
        {
            PuzzleInfo puzzleInfo = textbook[puzzleType];
            string displayContent = "Puzzle: " + puzzleInfo.PuzzleName + "\n\n" +
                                    "Description: " + puzzleInfo.PuzzleDescription + "\n\n";

            foreach (var example in puzzleInfo.Examples)
            {
                displayContent += "Example Problem: " + example.Problem + "\n" +
                                  "Solution: " + example.SolutionExpanation + "\n\n";
            }

            displayText.text = displayContent;
        }
        else
        {
            Debug.Log("Puzzle type not found in Textbook.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Get the current active puzzle (ensure there's a reference to MathPuzzle)
            MathPuzzle currentPuzzle = FindObjectOfType<MathPuzzle>();

            if (currentPuzzle != null)
            {
                // Call GetCurrentPuzzleType() from the current puzzle instance
                string puzzleType = currentPuzzle.GetCurrentPuzzleType();

                // Display the appropriate info using the retrieved puzzleType
                DisplayPuzzleInfo(puzzleType, displayText);
            }
            else
            {
                Debug.LogWarning("No active puzzle found.");
            }
        }
    }
}
