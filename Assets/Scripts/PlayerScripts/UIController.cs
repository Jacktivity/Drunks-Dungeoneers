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

    //Current active pause UI
    enum OpenPauseWindow
    {
        Unpaused,
        Paused,
        Options
    }

    private OpenGameWindow currentGameWindow;
    private OpenPauseWindow currentPauseWindow;

    //References to UI windows
    public GameObject PauseWindow;
    public GameObject OptionsWindow;



    public void PauseGame(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
            PauseWindow.SetActive(true);
            SetActivePauseWindow(OpenPauseWindow.Paused);

        }
        else
        {
            Time.timeScale = 1;
            PauseWindow.SetActive(false);
            SetActivePauseWindow(OpenPauseWindow.Paused);
        }
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

    public void OpenOptions(bool value)
    {
        if(value)
        {
            PauseWindow.SetActive(false);
            OptionsWindow.SetActive(value);
            SetActivePauseWindow(OpenPauseWindow.Options);
        }
        else
        {
            OptionsWindow.SetActive(value);
        }
    
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }

    private OpenGameWindow CheckActiveGameWindow()
    {
        return currentGameWindow;
    }

    private void SetActiveGameWindow(OpenGameWindow newWindow)
    {
        currentGameWindow = newWindow;
    }


    private OpenPauseWindow CheckActivePauseWindow()
    {
        return currentPauseWindow;
    }

    private void SetActivePauseWindow(OpenPauseWindow newWindow)
    {
        currentPauseWindow = newWindow;
    }
}
