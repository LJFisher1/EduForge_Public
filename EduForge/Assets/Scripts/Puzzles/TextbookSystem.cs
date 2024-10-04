using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextbookSystem : MonoBehaviour
{
    public TextMeshProUGUI textbookDisplay;
    public PuzzleInputController inputController;

    public class Example
    {
        public string Problem { get; set; }
        public string SolutionExplanation { get; set; }

        public Example(string problem, string solutionExplanation)
        {
            Problem = problem;
            SolutionExplanation = solutionExplanation;
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

        equationInfo.AddExample(new Example("What is 4 + 5?", "To solve this problem, add 4 and 5 together. The result is 9."));
        equationInfo.AddExample(new Example("What is 10 - 3?", "To solve this problem, subtract 3 from 10. The result is 7."));
        textbook.Add("Equation", equationInfo);

        // Sequence Puzzle Info
        var sequenceInfo = new PuzzleInfo
            ("Sequence",
            "A sequence is an ordered list of numbers that follows a specific pattern or rule."
            );

        sequenceInfo.AddExample(new Example("What is the next number in the sequence: 2, 4, 6, 8, ?", "The pattern is adding 2. The next number is 10."));
        sequenceInfo.AddExample(new Example("What is the next number in the sequence: 3, 9, 27, ?", "The pattern is multiplying by 3. The next number is 81."));
        textbook.Add("Sequence", sequenceInfo);

        //DisplayPuzzleInfo("Sequence"); // For testing
    }

    public void DisplayPuzzleInfo(string puzzleType)
    {
        if (textbook.TryGetValue(puzzleType, out PuzzleInfo puzzleInfo))
        {
            string displayContent = "Puzzle: " + puzzleInfo.PuzzleName + "\n\n" +
                "Description: " + puzzleInfo.PuzzleDescription + "\n\n";

            foreach (var example in puzzleInfo.Examples)
            {
                displayContent += "Example Problem:\n" + example.Problem + "\n\n" +
                    "Solution: " + example.SolutionExplanation + "\n\n";
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
                    DisplayExampleForPuzzleType(puzzleType);
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
