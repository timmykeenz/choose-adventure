using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject inGameHUD;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        //Check if the escape key was hit to pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }
    //Function resumes gameplay
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        //Add player's in-game HUD
        inGameHUD.SetActive(true);
    }
    //Function pauses game
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        //Remove player's in-game HUD
        inGameHUD.SetActive(false);
    }
    public void LoadMenu()
    {
        //Unpause game
        this.Resume();
        //Scene of index 0 should always be menu...
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
