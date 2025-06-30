using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseOptions;
    public static bool isPaused = false;
    private bool inOptions = false;
    public string menuScene;
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        pauseOptions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (inOptions)
                {
                    closeOptions();
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void openOptions()
    {
        pauseMenu.SetActive(false);
        pauseOptions.SetActive(true);
        inOptions = true;
    }

    public void closeOptions()
    {
        pauseOptions.SetActive(false);
        pauseMenu.SetActive(true);
        inOptions = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync(menuScene);
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
}
