using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject hpUI;
    public GameObject optionsMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        
        pauseMenuUI.SetActive(false);
        hpUI.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused = false;

    }

    void Pause()
    {
        hpUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void LoadOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Back()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
