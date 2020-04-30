using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    /**
     * Function loads the game's menu (Located at index 0) on call
     */
    public void LoadMenu()
    {
        //Make cursor visible and usable for menu (Resume will make it invisible)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //Scene of index 0 should always be menu...
        SceneManager.LoadScene(0);
    }
    /**
     * This loads our test level on call
     */
    public void LevelZero()
    {
        SceneManager.LoadScene(1);
    }
    /**
     * This loads the first level, aka Tutorial level at time of call
     */
    public void LevelOne()
    {
        SceneManager.LoadScene(2);
    }
}
