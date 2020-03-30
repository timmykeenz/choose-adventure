using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    private void Start()
    {
        //Grab all resolutions
        resolutions = Screen.resolutions;
        //Clear any options that may be in the dropdown
        resolutionDropdown.ClearOptions();
        //Create a list that will store all available resolutions
        List<string> options = new List<string>();
        //Loop through to retrieve resolutions and populate the list
        int currentResolutionIndex = 0;
        string option;
        for (int i = 0; i < resolutions.Length; i++)
        {
            option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            //Check if the index is the current screen's resolution
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                //If so, set the resolution
                currentResolutionIndex = i;
            }
        }
        //Add the list to our options for our dropdown menu
        resolutionDropdown.AddOptions(options);
        //Change the dropdown value to the current screen resolution
        resolutionDropdown.value = currentResolutionIndex;
        //Update the value
        resolutionDropdown.RefreshShownValue();
    }

    //Function allows volume slider to adjust in-game volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    //Function sets the in-game graphics quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //Function sets the game to fullscreen/windowed
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        //Grab the resolution chosen from the dropdown
        Resolution resolution = resolutions[resolutionIndex];
        //Set the resolution and keep the fullscreen according to the fullscreen checkbox
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
