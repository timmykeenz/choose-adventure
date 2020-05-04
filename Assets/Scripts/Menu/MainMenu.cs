using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /**
     * Play game boots the first level in the game, which is currently the test level
     */
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /**
     * This function exits the game when called
     */
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
