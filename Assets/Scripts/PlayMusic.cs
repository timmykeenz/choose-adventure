using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    //This is the song that will be passed through
    public AudioSource audio;
    //Switch turns on/off the song
    private bool songSwitch;
    [HideInInspector] public bool activate;
    private bool runMethod;
    private bool paused = false;
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
        //Pause music if pause menu is open
        if (PauseMenu.gameIsPaused)
        {
            audio.Pause();
            paused = true;
        }
        //Otherwise, if an audio clip was playing, resume
        else if (!audio.isPlaying && paused == true)
        {
            audio.UnPause();
            paused = false;
        }
        //Check if the key 'p' was pressed
        if (activate && runMethod)
        {
            //If the song is not playing, make it play and vice-versa
            songSwitch = !songSwitch;
            //Check if song is playing
            if (songSwitch)
            {
                //If not, start the music
                audio.Play();
            }
            else
            {
                //If it is, stop the music
                audio.Stop();
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
    public void StopAudio()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
    }
    //Method to set the audio source that this file plays
    public void setAudio(AudioSource audio)
    {
        this.audio = audio;
    }
}
