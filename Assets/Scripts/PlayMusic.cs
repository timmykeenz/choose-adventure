using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    //This is the song that will be passed through
    public AudioSource audioSource;

    //Method to set the audio source that this file plays
    public void setAudio(AudioSource audio)
    {
        this.audio = audio;
    }
}
