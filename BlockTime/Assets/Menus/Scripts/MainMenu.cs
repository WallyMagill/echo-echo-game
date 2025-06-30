using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string playScene = "PrimitiveLevel";
    public string tutorialScene;
    public GameObject mainMenu;
    public GameObject settingsPanel;


    public void Start()
    {
        settingsPanel.SetActive(false);
        GameObject darkenFilter = transform.parent.Find("DarkenFilter").gameObject;
        darkenFilter.SetActive(false);
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(playScene);
    }

    public void PlayTutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(tutorialScene);
    }

    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        settingsPanel.SetActive(true);
        GameObject darkenFilter = transform.parent.Find("DarkenFilter").gameObject;
        darkenFilter.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
