using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
            "An equation is a mathematical statement that asserts the equality of two expressions. \nDouble negatives ( - - ) make a positive ( + )."
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
            "To solve this problem, follow these steps: \n" +
            "1. Convert the percentage to a decimal by dividing by 100: 20% becomes 0.20. \n" +
            "2. Multiply the decimal by the value: 0.20 * 50 = 10. \n" +
            "3. The result is 10.", "Percentage Of"));
        textbook.Add("Percentage: Percentage Of", percentageInfo);

        // Percentage: Discount
        percentageInfo.AddExample(new Example("What is 15% off of 100?",
            "To solve this problem, follow these steps: \n" +
            "1. Convert the percentage to a decimal by dividing by 100. 15% becomes 0.15. \n" +
            "2. Multiply the decimal by the original price: 0.15 * 100 = 15. \n" +
            "3. Subtract the discount from the original price: 100 - 15 = 85. \n" +
            "3. The result is 85.", "Percentage Discount"));
        textbook.Add("Percentage: Percentage Discount", percentageInfo);

        // Percentage: Ratio
        percentageInfo.AddExample(new Example("What percent of 200 is 25?",
            "To solve this problem, follow these steps: \n" +
            "1. Divide the part by the whole: 25 / 200 = 0.125. \n" +
            "2. Multiply the result by 100 to convert it to a percentage: 0.125 * 100 = 12.5%. \n" +
            "3. The result is 12.5%.", "Percentage Ratio"));
        textbook.Add("Percentage: Percentage Ratio", percentageInfo);

        // Percentage: Increase
        percentageInfo.AddExample(new Example("If a value increases from 50 to 75, what is the percentage increase?",
            "To solve this problem, follow these steps: \n" +
            "1. Find the increase by subtracting the original value from the new value: 75 - 50 = 25. \n" +
            "2. Divide the increase by the original value: 25 / 50 = 0.50. \n" +
            "3. Multiply by 100 to convert to a percentage: 0.50 * 100 = 50%. \n" +
            "4. The result is a 50% increase.", "Percentage Increase"));
        textbook.Add("Percentage: Percentage Increase", percentageInfo);

        // Percentage: Decrease
        percentageInfo.AddExample(new Example("If a value decreases from 100 to 80, what is the percentage decrease?",
            "To solve this problem, follow these steps: \n" +
            "1. Find the decrease by subtracting the new value from the original value: 100 - 80 = 20. \n" +
            "2. Divide the decrease by the original value: 20 / 100 = 0.20. \n" +
            "3. Multiply by 100 to convert to a percentage: 0.20 * 100 = 20%. \n" +
            "4. The result is a 20% decrease.", "Percentage Decrease"));
        textbook.Add("Percentage: Percentage Decrease", percentageInfo);

        // PEMDAS Puzzle Info
        var pemdasInfo = new PuzzleInfo(
            "PEMDAS",
            "PEMDAS stands for 1. Parentheses, 2. Exponent, 3. Multiplication, 3. Division, 4. Addition and 4. Subtraction. The numbers denote their order of operations. " +
            "If two operations share a priority, then go in order of left to right in the equation."
            );

        // PEMDAS: BPM (Basic Parentheses and Multiplication)
        pemdasInfo.AddExample(new Example("What is (4 + 5) * (10 - 3)?",
            "To solve this problem, follow these steps: \n" +
            "1. First, solve the operations inside the parentheses: (4 + 5) = 9 and (10 - 3) = 7. \n" +
            "2. Now multiply the results: 9 * 7 = 63. \n" +
            "3. The final answer is 63.", "BPM"));
        textbook.Add("PEMDAS: BPM", pemdasInfo);

        // PEMDAS: MD (Multiplication and Divison)
        pemdasInfo.AddExample(new Example("What is 3 * 6 + 12 / 4",
            "To solve this problem, follow these steps: \n" +
            "1. First, perform the multiplication: 3 * 6 = 18. \n" +
            "2. Next, perform the division: 12 / 4 = 3. \n" +
            "3. Finally, perform the addition: 18 + 3 = 21. \n" +
            "4. The final answer is 21.", "MD"));
        textbook.Add("PEMDAS: MD", pemdasInfo);

        // PEMDAS: AMS (Addition, Multiplication and Subtraction)
        pemdasInfo.AddExample(new Example("What is 2 + 3 * 4 - 1?",
            "To solve this problem, follow these steps: \n" +
            "1. First, perform the multiplication: 3 * 4 = 12. \n " +
            "2. Then perform the addition and subtraction in order: 2 + 12 = 14, and 14 - 1 = 13. \n" +
            "3. The final answer is 13.", "AMS"));
        textbook.Add("PEMDAS: AMS", pemdasInfo);

        // PEMDAS: PMS (Parentheses with Multiplication and Subtraction)
        pemdasInfo.AddExample(new Example("What is 5 + (3 * 4) - (12 / 4)?",
            "To solve this problem follow these steps: \n" +
            "1. First, calculate the operations inside the parentheses: (3 * 4) = 12 and (12 / 4) = 3. \n" +
            "2. Then perform addition and subtraction in order from left to right: 5 + 12 = 17 and 17 - 3 = 14. \n" +
            "3. The final answer is 14.", "PMS"));
        textbook.Add("PEMDAS: PMS", pemdasInfo);

        // PEMDAS: DMS (Divison and Multiplication of Sums)
        pemdasInfo.AddExample(new Example("What is (6 + 4) / (10 - 5) * 3?",
            "To solve this problem, follow these steps: \n" +
            "1. First, solve the operations inside the parentheses: (6 + 4) = 10 and (10 - 5) = 5.\n" +
            "2. Then perform the division: 10 / 5 = 2. \n" +
            "3. Finally, multiply the result by 3: 2 * 3 = 6. \n" +
            "4. The final answer is 6.", "DMS"));
        textbook.Add("PEMDAS: DMS", pemdasInfo);

        // PEMDAS: NMA (Nested Multiplication and Addition)
        pemdasInfo.AddExample(new Example("What is 4 * (2 + 3) - (1 + 6)?",
            "To solve this problem, follow these steps: \n" +
            "1. First solve the operations inside the parentheses: (2 + 3) = 5 and (1 + 6) = 7. \n" +
            "2. Then perform the multiplication: 4 * 5 = 20." +
            "3. Finally, perform the subtraction: 20 - 7 = 13." +
            "4. The final answer is 13.", "NMA"));
        textbook.Add("PEMDAS: NMA", pemdasInfo);

        // Prime Number Puzzle Info
        var primeInfo = new PuzzleInfo(
            "Prime Number",
            "Prime numbers are numbers that can only be divided by 1 and themselves."
            );

        // IsPrime
        primeInfo.AddExample(new Example("Is 29 a prime number?",
            "To solve this problem, check if 29 can be divided by any number other than 1 and itself: \n" +
            "1. Test division by numbers smaller than 29: 29 cannot be divided evenly by any number except 1 and 29. \n" +
            "2. Therefore, 29 is a prime number. The answer is yes.", "IsPrime"));
        textbook.Add("PrimeNumber: IsPrime", primeInfo);

        // NextPrime
        primeInfo.AddExample(new Example("What is the next prime number after 17?",
            "To find the next prime number: \n" +
            "1. The number 18 is not prime (it's divisible by 2). \n" +
            "2. The number 19 is prime, as it can only be divided by 1 and 19." +
            "3. Therefore, the next prime number after 17 is 19.", "NextPrime"));
        textbook.Add("PrimeNumber: NextPrime", primeInfo);

        // PrimeFactorization
        primeInfo.AddExample(new Example("What are the prime factors of 28?",
            "To find the prime factors of 28: \n" +
            "1. Start by dividing 28 by the smallest prime number, 2: 28 / 2 = 14. \n" +
            "2. Divide 14 by 2 again: 2 / 14 = 7. \n" +
            "3. The number 7 is prime, so you cannot divide further. \n" +
            "4. The prime factors of 28 are 2, 2, and 7.\n" +
            "Note: Exclude 1 from any answers.", "PrimeFactorization"));
        textbook.Add("PrimeNumber: PrimeFactorization", primeInfo);

        // Statistics Puzzle Info
        var statisticsInfo = new PuzzleInfo(
            "Statistics",
            "Statistics involves collecting and analyzing data to understand trends and make informed decisions."
            );

        // Mean
        statisticsInfo.AddExample(new Example("Calculate the mean of the numbers 3, 8, 12, 7, 10",
            "To solve this problem, follow these steps: \n" +
            "1. Add the numbers together: 3 + 8 + 12 + 7 + 10 = 40. \n" +
            "2. Divide by the number of data points: 40 / 5 = 8. \n" +
            "3. The mean is 8.", "Mean"));
        textbook.Add("Statistics: Mean", statisticsInfo);

        // Median
        statisticsInfo.AddExample(new Example("Calculate the median of the numbers: 5, 6, 3, 9, 7.",
            "To solve this problem, follow these steps: \n" +
            "1. First, order the numbers from smallest to largest: 3, 5, 6, 7, 9." +
            "2. The middle number is 6." +
            "3. Therefore, the median is 6.", "Median"));
        textbook.Add("Statistics: Median", statisticsInfo);

        // Range
        statisticsInfo.AddExample(new Example("Find the range of the numbers: 15, 2, 8, 19, 7.",
            "To find the range, follow these steps: \n" +
            "1. Subtract the smallest number from the largest number: 19 - 2 = 17." +
            "2. The range is 17.", "Range"));
        textbook.Add("Statistics: Range", statisticsInfo);

        // Modular Arithmetic Puzzle Info
        var modularArithmeticInfo = new PuzzleInfo(
            "Modular Arithmetic",
            "Modular arithmetic deals with remainders after division operations. % is the Modulo Operator."
            );

        // Basic Modulo
        modularArithmeticInfo.AddExample(new Example("What is 17 % 5?",
            "To solve this problem, follow these steps: \n" +
            "1. Divide 17 by 5: 17 / 5 = 3 (quotient), with a remainder of 2." +
            "2. The modulo operator (%) gives the remainder after division. \n" +
            "3. Therefore, 17 % 5 = 2.", "Basic Modulo"));
        textbook.Add("Modular Arithmetic: Basic Modulo", modularArithmeticInfo);

        // Equivalence
        modularArithmeticInfo.AddExample(new Example("Are 24 and 14 congruent under modulo 5? (Yes/No)",
            "To determine if two numbers are congruent under modulo 5: \n" +
            "Congruent means 'Equal' \n" +
            "1. Find the remainder of each number when divided by 5: \n" +
            "  24 % 5 = 4 \n" +
            "  14 % 5 = 4 \n" +
            "2. Since both remainders are the same, 24 and 14 are congruent under modulo 5." +
            "The answer is Yes.", "Equivalence"));
        textbook.Add("Modular Arithmetic: Equivalence", modularArithmeticInfo);

        // Word Problem
        modularArithmeticInfo.AddExample(new Example("You have 16 items and 3 boxes. How many items are left after distributing evenly?",
            "To solve this word problem, follow these steps: \n" +
            "1. Divide 16 items by 3 boxes: 16 / 3 = 5 (quotient), with a remainder of 1. \n" +
            "2. The remainder is the number of items left after distribution. \n" +
            "3. Therefore, 1 item is left.", "Word Problem"));
        textbook.Add("Modular Arithmetic: Word Problem", modularArithmeticInfo);

        // Geometric Measurements Puzzle Info
        var geometricInfo = new PuzzleInfo(
            "Geometric Measurements",
            "Geometric measurements deal with measurements regarding geometric shapes, such as circles, triangles, squares, rectangles and cylinders.");

        // Perimeter
        geometricInfo.AddExample(new Example("What is the perimeter of a rectangle with length 8 and width 6?",
            "Use the formula for rectangles: \n" +
            "8 + 6 = 14 * 2 = 28. The perimeter is 28 units \n" +
            "Other common shapes: \n" +
            "Circle (Circumference): 2 * pi * radius \n" +
            "Triangle: Add together the length of all three sides \n" +
            "Square: Multiply the side length by 4 \n" +
            "Rectangle: Add the length and width together, then multiply by 2", "Perimeter"));
        textbook.Add("Geometric Measurements: Perimeter", geometricInfo);

        // Area
        geometricInfo.AddExample(new Example("What is the area of a triangle with base 10 and height 7?",
            "Use the formula for triangles: \n" +
            "1/2 * 10 * 7 = 35. The area is 35 square units \n" +
            "Other common shapes: \n" +
            "Circle: pi * radius^2 \n" +
            "Triangle: 1/2 * base * height \n" +
            "Square: Length * Length, or Length^2 (Squared) \n" +
            "Rectangle: Length * width", "Area"));
        textbook.Add("Geometric Measurements: Area", geometricInfo);

        // Volume
        geometricInfo.AddExample(new Example("What is the volume of a cylinder with radius 4 and height 9?",
            "Use the formula for cylinders: \n" +
            "Pi * 4^2 * 9 = 3.14 * 144 = approx. 452.16 units \n" +
            "Other common shapes: \n" +
            "Cube: Length * Length * Length, or Length^3 (Cubed) \n" +
            "Cylinder: Pi * radius^2 * height \n" +
            "Rectangluar Prism: Length * Width * Height", "Volume"));
        textbook.Add("Geometric Measurements: Volume", geometricInfo);


        var pythagoreanInfo = new PuzzleInfo(
            "Pythagorean Theorem",
            "The Pythagorean Theorem finds a side length in right triangles using a^2 + b^2 = c^2, where c is the longest side."
            );

        // Solving for A
        pythagoreanInfo.AddExample(new Example(
            "Find the length of side A if side B is 5 units and the hypotenuse C is 13.01 units.",
            "Use: A = sqrt(C^2 - B^2)\n" +
            "1. C^2 = 13.01^2 = 169.2601\n" +
            "2. B^2 = 5^2 = 25\n" +
            "3. 169.2601 - 25 = 144.2601, sqrt(144.2601) = 12.00\n" +
            "Result: A is approximately 12.00 units (rounded to 2 decimal places).",
            "Solving for A"
        ));
        textbook.Add("Pythagorean Theorem: Solving for A", pythagoreanInfo);

        // Solving for B
        pythagoreanInfo.AddExample(new Example(
            "Find the length of side B if side A is 6 units and the hypotenuse C is 10.77 units.",
            "Use: B = sqrt(C^2 - A^2)\n" +
            "1. C^2 = 10.77^2 = 116.0329\n" +
            "2. A^2 = 6^2 = 36\n" +
            "3. 116.0329 - 36 = 80.0329, sqrt(80.0329) = 8.94\n" +
            "Result: B is approximately 8.94 units (rounded to 2 decimal places).",
            "Solving for B"
        ));
        textbook.Add("Pythagorean Theorem: Solving for B", pythagoreanInfo);

        // Solving for C (Hypotenuse)
        pythagoreanInfo.AddExample(new Example(
            "Find the hypotenuse C if side A is 4 units and side B is 10 units.",
            "Use: C = sqrt(A^2 + B^2)\n" +
            "1. A^2 = 4^2 = 16\n" +
            "2. B^2 = 10^2 = 100\n" +
            "3. 16 + 100 = 116, sqrt(116) = 10.77\n" +
            "Result: The hypotenuse C is approximately 10.77 units (rounded to 2 decimal places).",
            "Solving for C"
        ));
        textbook.Add("Pythagorean Theorem: Solving for C", pythagoreanInfo);


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
                    displayContent += "Example Problem:\n" + example.Problem + "\n" +
                        "Solution: " + example.SolutionExplanation + "\n";
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
                    isTextbookVisible = !isTextbookVisible;
                    if (isTextbookVisible == true)
                    {
                        textbookDisplay.text = "Interact with another puzzle!";
                    }
                    else
                    {
                        textbookDisplay.text = "";
                    }
                    Debug.Log("Puzzle type is null or empty.");
                }
            }
            else
            {
                isTextbookVisible = !isTextbookVisible;
                if (isTextbookVisible == true)
                {
                    textbookDisplay.text = "Interact with a puzzle first!";
                }
                else
                {
                    textbookDisplay.text = "";
                }
                Debug.Log("No active puzzle found.");
            }
        }
    }
}
