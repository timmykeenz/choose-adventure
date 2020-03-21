using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioSource backgroundMusic;
    private bool songSwitch;
    // Start is called before the first frame update
    void Start()
    {
        songSwitch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            songSwitch = !songSwitch;
            if (songSwitch)
            {
                backgroundMusic.Play();
            }
            else
            {
                backgroundMusic.Stop();
            }
        }
    }
}
