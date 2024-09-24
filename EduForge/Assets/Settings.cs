using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuScript : MonoBehaviour
{
    public void BackToPauseMenu()
    {
        if (!string.IsNullOrEmpty(PauseMenuScript.previousScene))
        {
            SceneManager.LoadScene(PauseMenuScript.previousScene);
            Time.timeScale = 0;
        }
    }
}