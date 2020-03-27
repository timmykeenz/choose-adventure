using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    //This is the song that will be passed through
    public AudioSource backgroundMusic;
    //Switch turns on/off the song
    private bool songSwitch;
    [HideInInspector] public bool activate;
    private bool runMethod;
    // Start is called before the first frame update
    void Start()
    {
        //When the object is initiated, the song is not going to play
        songSwitch = false;
        activate = false;
        runMethod = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the key 'p' was pressed
        if (activate && runMethod)
        {
            //If the song is not playing, make it play and vice-versa
            songSwitch = !songSwitch;
            //Check if song is playing
            if (songSwitch)
            {
                //If not, start the music
                backgroundMusic.Play();
            }
            else
            {
                //If it is, stop the music
                backgroundMusic.Stop();
            }
            //Set runMethod to false so the song doesn't start an infinite amount of times
            runMethod = false;
        }
        //Once the user has let go of the button, reset runMethod
        else if (!activate)
        {
            runMethod = true;
        }
    }
}
