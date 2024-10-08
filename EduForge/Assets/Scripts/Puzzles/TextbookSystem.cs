using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextbookSystem : MonoBehaviour
{
    public TextMeshProUGUI textbookDisplay;
    public PuzzleInputController inputController;
    private bool isTextbookVisible = false;

    public class Example
    {
        public string Problem { get; set; }
        public string SolutionExplanation { get; set; }
        public string Operation { get; }

        public Example(string problem, string solutionExplanation, string operation)
        {
            Problem = problem;
            SolutionExplanation = solutionExplanation;
            Operation = operation;
        }
    }

    public class PuzzleInfo
    {
        public string PuzzleName { get; set; }
        public string PuzzleDescription { get; set; }
        public List<Example> Examples { get; set; }

        public PuzzleInfo(string puzzleName, string puzzleDescription)
        {
            PuzzleName = puzzleName;
            PuzzleDescription = puzzleDescription;
            Examples = new List<Example>();
        }

        public void AddExample(Example example)
        {
            Examples.Add(example);
        }
    }

    private Dictionary<string, PuzzleInfo> textbook = new Dictionary<string, PuzzleInfo>();
    private void Start()
    {
        InitializeTextbook();
    }

    private void InitializeTextbook()
    {
        // Equation Puzzle Info
        var equationInfo = new PuzzleInfo
            ("Equation",
            "An equation is a mathematical statement that asserts the equality of two expressions."
            );

        equationInfo.AddExample(new Example("What is 4 + 5?", "To solve this problem, add 4 and 5 together. The result is 9.", "+"));
        textbook.Add("Equation: +", equationInfo);
        equationInfo.AddExample(new Example("What is 10 - 3?", "To solve this problem, subtract 3 from 10. The result is 7.", "-"));
        textbook.Add("Equation: -", equationInfo);
        equationInfo.AddExample(new Example("What is 17 * 5?", "To solve this problem, multiply 17 by 5. The result is 85.", "*"));
        textbook.Add("Equation: *", equationInfo);

        // Sequence Puzzle Info
        var sequenceInfo = new PuzzleInfo
            ("Sequence",
            "A sequence is an ordered list of numbers that follows a specific pattern or rule."
            );

        sequenceInfo.AddExample(new Example("What is the next number in the Arithmetic sequence: 2, 4, 6, 8, ?", "The pattern is adding 2. The next number is 10.", "Arithmetic"));
        textbook.Add("Sequence: Arithmetic", sequenceInfo);
        sequenceInfo.AddExample(new Example("What is the next number in the Geometric sequence: 3, 9, 27, ?", "The pattern is multiplying by 3. The next number is 81.", "Geometric"));
        textbook.Add("Sequence: Geometric", sequenceInfo);
        sequenceInfo.AddExample(new Example("What is the next number in the Fibonacci sequence: 0, 1, 1, 2, 3, 5, 8, ?", "The pattern is that each number is the sum of the two preceding numbers." +
            " The next number would be 13.", "Fibonacci"));
        textbook.Add("Sequence: Fibonacci", sequenceInfo);

        //DisplayPuzzleInfo("Sequence"); // For testing
    }

    public void DisplayPuzzleInfo(string puzzleType, string operation = null)
    {
        string[] puzzleParts = puzzleType.Split(':');
        string basePuzzleType = puzzleParts[0].Trim();
        operation = puzzleParts.Length > 1 ? puzzleParts[1].Trim() : null;

        if (textbook.TryGetValue(puzzleType, out PuzzleInfo puzzleInfo))
        {
            string displayContent = "Puzzle: " + puzzleInfo.PuzzleName + "\n\n" +
                "Description: " + puzzleInfo.PuzzleDescription + "\n\n";

            foreach (var example in puzzleInfo.Examples)
            {
                if (operation == null || example.Operation == operation)
                {
                    displayContent += "Example Problem:\n" + example.Problem + "\n\n" +
                        "Solution: " + example.SolutionExplanation + "\n\n";
                }
            }

            textbookDisplay.text = displayContent;
        }
        else
        {
            textbookDisplay.text = "Puzzle type not found in Textbook.";
        }
    }

    public void DisplayExampleForPuzzleType(string puzzleType)
    {
        DisplayPuzzleInfo(puzzleType);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {

            if (inputController.currentPuzzle != null)
            {
                string puzzleType = inputController.currentPuzzle.GetCurrentPuzzleType();
                if (!string.IsNullOrEmpty(puzzleType))
                {
                    if (isTextbookVisible)
                    {
                        textbookDisplay.text = "";
                        isTextbookVisible = false;
                    }
                    else
                    {
                        DisplayExampleForPuzzleType(puzzleType);
                        isTextbookVisible = true;
                    }
                }
                else
                {
                    Debug.Log("Puzzle type is null or empty.");
                }
            }
            else
            {
                Debug.Log("No active puzzle found.");
            }
        }
    }
}
