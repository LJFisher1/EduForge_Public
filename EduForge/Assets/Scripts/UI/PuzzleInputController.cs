using System.Collections;
using UnityEngine;
using TMPro;

public class PuzzleInputController : MonoBehaviour
{
    public TMP_InputField inputField;
    public MathPuzzle currentPuzzle; // This will be updated by the PuzzleTrigger

    // This method will be called by each button when clicked
    public void OnKeyPress(string keyPressed)
    {
        if (inputField != null)
        {
            inputField.text += keyPressed;
        }
        else
        {
            Debug.LogError("InputField is not assigned in the Inspector");
        }
    }

    // This method is linked to the clear button and clears input field
    public void ClearInput()
    {
        if (inputField != null)
        {
            inputField.text = "";
        }
        else
        {
            Debug.LogError("InputField is not assigned in the Inspector");
        }
    }

    // This method is linked to the del key and removes the last character of the input field
    public void DeleteKeyPressed()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Remove(inputField.text.Length - 1, 1);
        }
    }

    // This method handles submission logic
    public void SubmitAnswer()
    {
        if (inputField == null)
        {
            Debug.LogError("InputField is not assigned in the Inspector.");
            return;
        }

        if (currentPuzzle == null)
        {
            Debug.LogError("currentPuzzle is not assigned or has been reset.");
            return;
        }

        Debug.Log("Submitted answer: " + inputField.text + " for puzzle: " + currentPuzzle.name);

        // Pass the submitted answer to the current puzzle's CheckAnswer method
        currentPuzzle.SubmitPuzzleAnswer(inputField.text);
    }

    public void YesButton()
    {
        inputField.text += "1";
        currentPuzzle.SubmitPuzzleAnswer(inputField.text);
    }

    public void NoButton()
    {
        inputField.text += "0";
        currentPuzzle.SubmitPuzzleAnswer(inputField.text);
    }
}