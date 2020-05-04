using UnityEngine;

public class AudioSync : MonoBehaviour
{
    //User manipulatable variables
    public int bpm;
    public int ticksPerLoop;
    public AudioSource bgMusic;
    //Hidden/Private variables
    [HideInInspector] public static bool inSync;
    private float bps;
    private float syncTime;
    //Static variables
    private static float syncTimeStatic;
    private static AudioSource bgMusicStatic;
    private static float offset;
    // Start is called before the first frame update
    void Start()
    {
        //CHANGE OFFSET TO ADJUST TIME MIXING
        offset = 0.025f;
        //Variables that require some simple math to figure out when the beat should change
        bps = 60f / (float)bpm;
        inSync = false;
        syncTime = bps * (float)ticksPerLoop;
        //Static variables to imitate passed through values
        syncTimeStatic = syncTime;
        bgMusicStatic = bgMusic;
    }
    /**
     * Function gets the time till the next beat loop
     * 
     * @return - Returns the time in seconds till the start of the next beat
     */
    public static float GetTime()
    {
        return syncTimeStatic - (bgMusicStatic.time % syncTimeStatic) - offset;
    }
    /**
     * Function changes a song clip to a given clipURL
     * 
     * @param clipURL - The file location of the new audio clip to be played
     */
    public static void ChangeSong(string clipURL)
    {
        //Check to make sure string is not empty
        bgMusicStatic.Stop();
        bgMusicStatic.clip = (AudioClip)Resources.Load(clipURL);
        bgMusicStatic.Play();
    }
}
