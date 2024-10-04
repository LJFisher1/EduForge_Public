using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextbookSystem : MonoBehaviour
{
    public TextMeshProUGUI displayText; // Reference to the UI TextMeshPro component
    private MathPuzzle currentPuzzle; // Reference to the current puzzle

    // Class to hold information about each puzzle type
    public class PuzzleInfo
    {
        public string PuzzleName { get; set; }
        public string PuzzleDescription { get; set; }
        public Dictionary<string, List<Example>> Examples { get; set; }

        public PuzzleInfo(string puzzleName, string puzzleDescription)
        {
            PuzzleName = puzzleName;
            PuzzleDescription = puzzleDescription;
            Examples = new Dictionary<string, List<Example>>();
        }

        public void AddExample(string type, Example example)
        {
            if (!Examples.ContainsKey(type))
            {
                Examples[type] = new List<Example>();
            }
            Debug.Log($"Adding example of type: {type} with problem: {example.Problem}");
            Examples[type].Add(example);
        }
    }

    // Class to hold example problems and their explanations
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

    // Dictionary to store textbook data
    private Dictionary<string, PuzzleInfo> textbook = new Dictionary<string, PuzzleInfo>();

    private void Start()
    {
        InitializeTextbook(); // Initialize the textbook data
    }

    public void InitializeTextbook()
    {
        // Equation Info
        var equationInfo = new PuzzleInfo(
            "Equation",
            "An equation is a mathematical statement that asserts the equality of two expressions.");

        // Equation Examples
        equationInfo.AddExample("Equation: +", new Example("What is 4 + 5?", "To solve this problem, add 4 and 5 together. The result is 9."));
        equationInfo.AddExample("Equation: -", new Example("What is 10 - 3?", "To solve this problem, subtract 3 from 10. The result is 7."));
        equationInfo.AddExample("Equation: *", new Example("What is 6 * 7?", "To solve this problem, multiply 6 by 7. The result is 42."));
        textbook.Add("Equation", equationInfo);

        // Sequence Info
        var sequenceInfo = new PuzzleInfo(
            "Sequence",
            "A sequence is an ordered list of numbers that follows a specific pattern or rule. Common types of sequences include arithmetic sequences, geometric sequences, and the Fibonacci sequence.");

        // Sequence Examples
        sequenceInfo.AddExample("Sequence: Arithmetic", new Example("What is the next number in the sequence 2, 4, 6, 8, ?", "The pattern is adding 2. The next number is 10."));
        sequenceInfo.AddExample("Sequence: Geometric", new Example("What is the next number in the sequence 3, 9, 27, ?", "The pattern is multiplying by 3. The next number is 81."));
        sequenceInfo.AddExample("Sequence: Fibonacci", new Example("What is the next number in the Fibonacci sequence 0, 1, 1, 2, 3, ?", "The pattern is adding the two previous numbers. The next number is 5."));
        textbook.Add("Sequence", sequenceInfo);
    }

    public void SetCurrentPuzzle(MathPuzzle puzzle)
    {
        currentPuzzle = puzzle; // Set the current puzzle
    }

    public void DisplayExampleForCurrentPuzzle()
    {
        if (currentPuzzle == null)
        {
            Debug.LogWarning("Current puzzle is not set.");
            return;
        }

        string puzzleType = currentPuzzle.GetCurrentPuzzleType();
        Debug.Log($"Current Puzzle Type: {puzzleType}");

        if (textbook.TryGetValue(puzzleType, out var puzzleInfo))
        {
            // Clear the existing text
            displayText.text = $"{puzzleInfo.PuzzleName}\n{puzzleInfo.PuzzleDescription}\n\nExamples:\n";

            // Display examples for the current puzzle type
            foreach (var example in puzzleInfo.Examples)
            {
                foreach (var ex in example.Value)
                {
                    Debug.Log($"Example Problem: {ex.Problem}"); // Log each example
                    displayText.text += $"{ex.Problem}\nExplanation: {ex.SolutionExplanation}\n\n";
                }
            }
        }
        else
        {
            Debug.LogWarning("Puzzle type not found in the textbook.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Find the current puzzle instance
            MathPuzzle currentPuzzle = FindObjectOfType<MathPuzzle>();
            if (currentPuzzle != null)
            {
                SetCurrentPuzzle(currentPuzzle); // Set the current puzzle
                DisplayExampleForCurrentPuzzle(); // Display examples
            }
            else
            {
                Debug.LogWarning("No active puzzle found.");
            }
        }
    }
}
