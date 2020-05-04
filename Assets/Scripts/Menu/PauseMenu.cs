using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject inGameHUD;
    public GameObject pauseMenuUI;
    private AudioSource[] audioSources;
    [HideInInspector] public static bool gameIsPaused = false;

    //On start, make the mouse hidden
    void Start()
    {
        audioSources = Component.FindObjectsOfType<AudioSource>();
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
    /**
     * Function pauses all active audio in the game
     */
    public void PauseAllAudio()
    {
        // Loop through and pause each audio source
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Pause();
        }
    }
    /**
     * Resumes all active audio that is playing in the game
     */
    public void ResumeAllAudio()
    {
        // Loop through and resume each audio source
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.UnPause();
        }
    }
    /**
     * Function resumes gameplay
     */
    public void Resume()
    {
        //Resume the music
        ResumeAllAudio();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        //Add player's in-game HUD
        inGameHUD.SetActive(true);
        //Make cursor invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    /**
     * Function pauses game
     */
    private void Pause()
    {
        //Pause the music
        PauseAllAudio();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        //Remove player's in-game HUD
        inGameHUD.SetActive(false);
        //Make cursor visible and usable
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    /**
     * Function loads the menu
     */
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
    /**
     * Function exits the game
     */
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
