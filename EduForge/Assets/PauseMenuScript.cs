using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject PauseButton;

    // Start is called before the first frame update
    public void Start()
    {
        PauseMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;

        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;

        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
    }
}
