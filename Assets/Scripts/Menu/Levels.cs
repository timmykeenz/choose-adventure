using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void LoadMenu()
    {
        //Make cursor visible and usable for menu (Resume will make it invisible)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //Scene of index 0 should always be menu...
        SceneManager.LoadScene(0);
    }
    public void LevelZero()
    {
        SceneManager.LoadScene(1);
    }
    public void LevelOne()
    {
        SceneManager.LoadScene(2);
    }
}
