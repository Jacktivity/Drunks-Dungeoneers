using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheMainMenu : MonoBehaviour
{
    //John Shone - 10/08/2018
    //Public Variables
    public Button play;
    public Button quit;
    public Button options;
    public Button scoreboard;
    public Button backSB;
    public Button backOp;


    public GameObject mainMenuPanel;
    public GameObject scoreBoardPanel;
    public GameObject optionsPanel;

    public OptionsScript optionsScript;

    //Private Variables


	void Start ()
    {
        mainMenuPanel.SetActive(true);
        scoreBoardPanel.SetActive(false);
        optionsPanel.SetActive(false);

        play.onClick.AddListener(TogglePlayButton);
        quit.onClick.AddListener(ToggleQuitButton);
        scoreboard.onClick.AddListener(ToggleScoreboardButton);
        options.onClick.AddListener(ToggleOptionButton);
        backOp.onClick.AddListener(delegate { ToggleOptionsBackButton();  });
        backSB.onClick.AddListener(delegate { ToggleBackButton(); });
    }

    public void TogglePlayButton()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void ToggleScoreboardButton()
    {
        mainMenuPanel.SetActive(false);
        scoreBoardPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ToggleOptionButton()
    {
        mainMenuPanel.SetActive(false);
        scoreBoardPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ToggleBackButton()
    {
        mainMenuPanel.SetActive(true);
        scoreBoardPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }
    public void ToggleOptionsBackButton()
    {
        mainMenuPanel.SetActive(true);
        scoreBoardPanel.SetActive(false);
        optionsPanel.SetActive(false);

        optionsScript.SaveUserSettings();
    }

    public void ToggleQuitButton()
    {
        Application.Quit();
    }
}
