using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject inGameHUD;
    public GameObject pauseMenuUI;
    public AudioSource audioSource;

    //On start, make the mouse hidden
    void Start()
    {
        //Make cursor invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
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
        //Resume the music
        audioSource.UnPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        //Add player's in-game HUD
        inGameHUD.SetActive(true);
        //Make cursor invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //Function pauses game
    private void Pause()
    {
        //Pause the music
        audioSource.Pause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        //Remove player's in-game HUD
        inGameHUD.SetActive(false);
        //Make cursor visible and usable
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void LoadMenu()
    {
        //Unpause game
        this.Resume();
        //Make cursor visible and usable for menu (Resume will make it invisible)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //Scene of index 0 should always be menu...
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
