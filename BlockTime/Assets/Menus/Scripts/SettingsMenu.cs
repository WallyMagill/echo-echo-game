using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public GameObject mainMenu;
    public GameObject settingsPanel;

    Resolution[] resolutions;

    void Start()
    {
        // Default to full screen
        Screen.fullScreen = true;

        // Get resolutions and clear the dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // String list to store resolutions
        List<string> options = new List<string>();

        // To store current resolution index
        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            // Format resolution and store in list
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            
            // Check for current resolution
            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }
        // Add resolutions to dropdown and set value to current resolution
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("soundVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void BackToMain()
    {
        GameObject darkenFilter = transform.parent.Find("DarkenFilter").gameObject;
        mainMenu.SetActive(true);
        settingsPanel.SetActive(false);
        darkenFilter.SetActive(false);
    }
}
