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

        equationInfo.AddExample(new Example("What is 4 + 5?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the numbers: 4 and 5. \n" +
            "2. Add the two numbers together: 4 + 5 = 9. \n" +
            "3. The result is 9.", "+"));
        textbook.Add("Equation: +", equationInfo);

        equationInfo.AddExample(new Example("What is 10 - 3?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the numbers: 10 and 3. \n" +
            "2. Subtract the second number from the first: 10 - 3 = 7. \n" +
            "3. The result is 7.", "-"));
        textbook.Add("Equation: -", equationInfo);

        equationInfo.AddExample(new Example("What is 17 * 5?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the numbers: 17 and 5. \n" +
            "2. Multiply the two numbers together: 17 * 5 = 85. \n" +
            "3. The result is 85.", "*"));
        textbook.Add("Equation: *", equationInfo);

        // Sequence Puzzle Info
        var sequenceInfo = new PuzzleInfo
            ("Sequence",
            "A sequence is an ordered list of numbers that follows a specific pattern or rule."
            );

        sequenceInfo.AddExample(new Example("What is the next number in the Arithmetic sequence: 2, 4, 6, 8, ?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the pattern: each number increases by 2. \n" +
            "2. Add 2 to the last number: 8 + 2 = 10. \n" +
            "3. The next number is 10.", "Arithmetic"));
        textbook.Add("Sequence: Arithmetic", sequenceInfo);

        sequenceInfo.AddExample(new Example("What is the next number in the Geometric sequence: 3, 9, 27, ?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the pattern: each number is multiplied by 3. \n" +
            "2. Multiply the last number by 3: 27 * 3 = 81. \n" +
            "3. The next number is 81.", "Geometric"));
        textbook.Add("Sequence: Geometric", sequenceInfo);

        sequenceInfo.AddExample(new Example("What is the next number in the Fibonacci sequence: 0, 1, 1, 2, 3, 5, 8, ?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the pattern: each number is the sum of the two preceding numbers. \n" +
            "2. Add the last two numbers: 5 + 8 = 13. \n" +
            "3. The next number is 13.", "Fibonacci"));
        textbook.Add("Sequence: Fibonacci", sequenceInfo);

        // Decimal Puzzle Info
        var decimalInfo = new PuzzleInfo
            ("Decimal",
            "Decimals are numbers that include a decimal point, representing fractions in a base-10 system."
            );

        decimalInfo.AddExample(new Example("What is 0.5 + 0.3?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the numbers: 0.5 and 0.3. \n" +
            "2. Add the two numbers together: 0.5 + 0.3 = 0.8. \n" +
            "3. The result is 0.8.", "+"));
        textbook.Add("Decimal: +", decimalInfo);

        decimalInfo.AddExample(new Example("What is 7.4 - 3.6?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the numbers: 7.4 and 3.6. \n" +
            "2. Subtract the second number from the first: 7.4 - 3.6 = 3.8. \n" +
            "3. The result is 3.8.", "-"));
        textbook.Add("Decimal: -", decimalInfo);

        decimalInfo.AddExample(new Example("What is 0.6 * 0.4?",
            "To solve this problem, follow these steps: \n" +
            "1. Ignore the decimal points and multiply as if they were whole numbers: 6 * 4 = 24. \n" +
            "2. Count the total number of decimal places in both numbers (0.6 has 1 decimal place, and 0.4 has 1 decimal place, totaling 2). \n" +
            "3. Place the decimal point in the result (24) so that there are 2 decimal places: The result is 0.24.", "*"));
        textbook.Add("Decimal: *", decimalInfo);


        // Percentage Puzzle Info


        // PEMDAS Puzzle Info


        // Prime Number Puzzle Info


        // Statistics Puzzle Info


        // Modular Arithmetic Puzzle Info


        // Geometric Measurements Puzzle Info


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

            textbookDisplay.text = displayContent + "\n Press 'T' to turn display on/off.";
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
