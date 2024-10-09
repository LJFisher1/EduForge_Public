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

        // Equation: Add
        equationInfo.AddExample(new Example("What is 4 + 5?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the numbers: 4 and 5. \n" +
            "2. Add the two numbers together: 4 + 5 = 9. \n" +
            "3. The result is 9.", "+"));
        textbook.Add("Equation: +", equationInfo);

        //Equation: Subtract
        equationInfo.AddExample(new Example("What is 10 - 3?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the numbers: 10 and 3. \n" +
            "2. Subtract the second number from the first: 10 - 3 = 7. \n" +
            "3. The result is 7.", "-"));
        textbook.Add("Equation: -", equationInfo);

        //Equation: Multiply
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

        // Sequence: Arithmetic
        sequenceInfo.AddExample(new Example("What is the next number in the Arithmetic sequence: 2, 4, 6, 8, ?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the pattern: each number increases by 2. \n" +
            "2. Add 2 to the last number: 8 + 2 = 10. \n" +
            "3. The next number is 10.", "Arithmetic"));
        textbook.Add("Sequence: Arithmetic", sequenceInfo);

        // Sequence: Geometric
        sequenceInfo.AddExample(new Example("What is the next number in the Geometric sequence: 3, 9, 27, ?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the pattern: each number is multiplied by 3. \n" +
            "2. Multiply the last number by 3: 27 * 3 = 81. \n" +
            "3. The next number is 81.", "Geometric"));
        textbook.Add("Sequence: Geometric", sequenceInfo);

        // Sequence: Fibonacci
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

        // Decimal: Add
        decimalInfo.AddExample(new Example("What is 0.5 + 0.3?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the numbers: 0.5 and 0.3. \n" +
            "2. Add the two numbers together: 0.5 + 0.3 = 0.8. \n" +
            "3. The result is 0.8.", "+"));
        textbook.Add("Decimal: +", decimalInfo);

        // Decimal: Subtract
        decimalInfo.AddExample(new Example("What is 7.4 - 3.6?",
            "To solve this problem, follow these steps: \n" +
            "1. Identify the numbers: 7.4 and 3.6. \n" +
            "2. Subtract the second number from the first: 7.4 - 3.6 = 3.8. \n" +
            "3. The result is 3.8.", "-"));
        textbook.Add("Decimal: -", decimalInfo);

        // Decimal: Multiply
        decimalInfo.AddExample(new Example("What is 0.6 * 0.4?",
            "To solve this problem, follow these steps: \n" +
            "1. Ignore the decimal points and multiply as if they were whole numbers: 6 * 4 = 24. \n" +
            "2. Count the total number of decimal places in both numbers (0.6 has 1 decimal place, and 0.4 has 1 decimal place, totaling 2). \n" +
            "3. Place the decimal point in the result (24) so that there are 2 decimal places: The result is 0.24.", "*"));
        textbook.Add("Decimal: *", decimalInfo);


        // Percentage Puzzle Info
        var percentageInfo = new PuzzleInfo
            ("Percentage",
            "Percentages represent a ratio or fraction out of 100, used to express how one value relates to another."
            );

        // Percentage: Of
        percentageInfo.AddExample(new Example("What is 20% of 50?",
            "To solve this problem, follow the steps: \n" +
            "1. Convert the percentage to a decimal by dividing by 100: 20% becomes 0.20. \n" +
            "2. Multiply the decimal by the value: 0.20 * 50 = 10. \n" +
            "The result is 10.", "Percentage Of"));
        textbook.Add("Percentage: Percentage Of", percentageInfo);

        // Percentage: Discount
        percentageInfo.AddExample(new Example("What is 15% off of 100?",
            "To solve this problem, follow the steps: \n" +
            "1. Convert the percentage to a decimal by dividing by 100. 15% becomes 0.15. \n" +
            "2. Multiply the decimal by the original price: 0.15 * 100 = 15. \n" +
            "3. Subtract the discount from the original price: 100 - 15 = 85. \n" +
            "The result is 85.", "Percentage Discount"));
        textbook.Add("Percentage: Percentage Discount", percentageInfo);

        // Percentage: Ratio
        percentageInfo.AddExample(new Example("What percent of 200 is 25?",
            "To solve this problem, follow the steps: \n" +
            "1. Divide the part by the whole: 25 / 200 = 0.125. \n" +
            "2. Multiply the result by 100 to convert it to a percentage: 0.125 * 100 = 12.5%. \n" +
            "The result is 12.5%.", "Percentage Ratio"));
        textbook.Add("Percentage: Percentage Ratio", percentageInfo);

        // Percentage: Increase
        percentageInfo.AddExample(new Example("If a value increases from 50 to 75, what is the percentage increase?",
            "To solve this problem, follow the steps: \n" +
            "1. Find the increase by subtracting the original value from the new value: 75 - 50 = 25. \n" +
            "2. Divide the increase by the original value: 25 / 50 = 0.50. \n" +
            "3. Multiply by 100 to convert to a percentage: 0.50 * 100 = 50%. \n" +
            "The result is a 50% increase.", "Percentage Increase"));
        textbook.Add("Percentage: Percentage Increase", percentageInfo);

        // Percentage: Decrease
        // If a value decreases from 100 to 80, what is the percent decrease?
        percentageInfo.AddExample(new Example("If a value decreases from 100 to 80, what is the percentage decrease?",
            "To solve this problem, follow the steps: \n" +
            "1. Find the decrease by subtracting the new value from the original value: 100 - 80 = 20. \n" +
            "2. Divide the decrease by the original value: 20 / 100 = 0.20. \n" +
            "3. Multiply by 100 to convert to a percentage: 0.20 * 100 = 20%. \n" +
            "The result is a 20% decrease.", "Percentage Decrease"));
        textbook.Add("Percentage: Percentage Decrease", percentageInfo);

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
