using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleInputController : MonoBehaviour
{
    public TMP_InputField inputField;

    // This method will be called by each button when clicked
    public void OnKeyPress(string keyPressed)
    {
        // Append the pressed key to the current input text
        inputField.text += keyPressed;
    }

    // This method is linked to the clear button clears input field
    public void ClearInput()
    {
        inputField.text = "";
    }

    // This method is linked to the del key and removes last character of the input field
    public void DeleteKeyPressed()
    {
        inputField.text = inputField.text.Remove(inputField.text.Length - 1,1);
    }

    // This method handles submission logic
    // Still Needs a way to check answer of current puzzle etc
    public void SubmitAnswer()
    {
        Debug.Log("Submitted answer: " + inputField.text);
        
    }
}