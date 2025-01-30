using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resDropdown;

    Resolution[] res;
    void Start()
    {
        res = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < res.Length; i++)
        {
            string option = res[i].width + " x " + res[i].height;

            options.Add(option);
            if (res[i].width == Screen.currentResolution.width 
                && res[i].height == Screen.currentResolution.height) currentResIndex = i;
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();


    }

    public void SetResolution(int index)
    {
        Resolution resolution = res[index];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
