using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void LevelZero()
    {
        SceneManager.LoadScene(1);
    }
    public void LevelOne()
    {
        SceneManager.LoadScene(2);
    }
}
