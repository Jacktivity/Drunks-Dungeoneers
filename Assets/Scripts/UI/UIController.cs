using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private List<GameObject> healthBar;
    public GameObject HealthTemplate;
    public GameObject HealthBarHolder;

    public GameObject PlayerRef;

  


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

    private void UpdateHealth()
    {
        if (healthBar.Count > PlayerRef.GetComponent<Maid>().GetHealth())
            RemoveHealthIcon();
        else if (healthBar.Count < PlayerRef.GetComponent<Maid>().GetHealth())
            AddHealthIcon();
    }

    private void InitHealthUI()
    {
        healthBar = new List<GameObject>();

        int health = PlayerRef.GetComponent<Maid>().GetHealth();

        for (int i = 0; i < health;  i++)
        {
            GameObject heart = Instantiate(HealthTemplate, new Vector3(0, 0, 0), Quaternion.identity);
            heart.transform.SetParent(HealthBarHolder.transform);
            heart.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * 100.0f, 0.0f,0.0f);
            heart.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            healthBar.Add(heart);
        }
    }

    private void RemoveHealthIcon()
    {
        Destroy(healthBar[healthBar.Count - 1]);
        healthBar.Remove(healthBar[healthBar.Count - 1]);
    }

    private void AddHealthIcon()
    {
        GameObject heart = Instantiate(HealthTemplate, new Vector3(0, 0, 0), Quaternion.identity);
        heart.transform.SetParent(HealthBarHolder.transform);
        heart.GetComponent<RectTransform>().anchoredPosition = new Vector3(healthBar.Count * 100.0f, 0.0f, 0.0f);
        heart.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        healthBar.Add(heart);
    }

    private void Start()
    {
        InitHealthUI();
    }

    private void Update()
    {
        UpdateHealth();
    }

}
