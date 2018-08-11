using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {


    //Current active ingame UI
    enum OpenGameWindow
    {
        None
    }

    private OpenGameWindow currentGameWindow;

    //References to UI windows
    public GameObject PauseWindow;
    public GameObject OptionsWindow;
    public GameObject HelpWindow;


    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseWindow.SetActive(true);
        OptionsWindow.SetActive(false);
        HelpWindow.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        if(!PauseWindow.activeSelf)
        {
            Time.timeScale = 0;
            PauseWindow.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PauseWindow.SetActive(false);
        }
    }

    public void OpenOptionsWindow()
    {
        PauseWindow.SetActive(false);
        OptionsWindow.SetActive(true);
        HelpWindow.SetActive(false);
    }

    public void OpenHelpWindow()
    {
        PauseWindow.SetActive(false);
        OptionsWindow.SetActive(false);
        HelpWindow.SetActive(true);
    }

    public void OpenPauseWindow()
    {
        PauseWindow.SetActive(true);
        OptionsWindow.SetActive(false);
        HelpWindow.SetActive(false);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }

}
