using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModularArithmeticPuzzle : MathPuzzle
{
    public TextMeshProUGUI modularText;
    private string currentEquation;
    private string puzzleType;
    private int a, b;

    protected override void GeneratePuzzle()
    {
        a = Random.Range(1, 20);
        b = Random.Range(1, 20);
        string[] puzzleTypes = { "Basic Modulo", "Missing Number", "Equivalence", "Word Problem" };

        puzzleType = puzzleTypes[Random.Range(0, puzzleTypes.Length)];

        switch (puzzleType)
        {
            case "Basic Modulo":

                break;

            case "Missing Number":

                break;

            case "Equivalence":

                break;

            case "Word Problem":

                break;
        }

    }
    public override void ResetPuzzleState()
    {
        throw new System.NotImplementedException();
    }

    protected override void CheckAnswer(string userAnswer)
    {
        throw new System.NotImplementedException();
    }
    public override void StartPuzzle()
    {
        if (isPuzzleGenerated && !IsPuzzleSolved())
        {
            Debug.Log("Resuming unsolved decimal puzzle.");
            modularText.text = currentEquation;
            puzzleUI.SetActive(true);
            playerMovement.TogglePuzzleMode(true); // Disable movement/camera controls
            return;
        }
        base.StartPuzzle();
    }

}


