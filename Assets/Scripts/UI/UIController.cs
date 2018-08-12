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
    public GameObject pauseWindow;
    public GameObject optionsWindow;
    public GameObject helpWindow;

    private List<GameObject> healthBar;
    public GameObject healthTemplate;
    public GameObject healthBarHolder;

    public GameObject coinText;
    private Text coinTextRef;

    public GameObject drinkImage;

    public GameObject timerText;
    private Text timerTextRef;

    private GameObject playerRef;
    private Maid maidScript;


    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseWindow.SetActive(true);
        optionsWindow.SetActive(false);
        helpWindow.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        if(!pauseWindow.activeSelf)
        {
            Time.timeScale = 0;
            pauseWindow.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseWindow.SetActive(false);
        }
    }

    public void OpenOptionsWindow()
    {
        pauseWindow.SetActive(false);
        optionsWindow.SetActive(true);
        helpWindow.SetActive(false);
    }

    public void OpenHelpWindow()
    {
        pauseWindow.SetActive(false);
        optionsWindow.SetActive(false);
        helpWindow.SetActive(true);
    }

    public void OpenPauseWindow()
    {
        pauseWindow.SetActive(true);
        optionsWindow.SetActive(false);
        helpWindow.SetActive(false);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void UpdateHealth()
    {
        if (healthBar.Count > playerRef.GetComponent<Maid>().GetHealth())
        {
            RemoveHealthIcon();
        }
        else if (healthBar.Count < playerRef.GetComponent<Maid>().GetHealth())
        {
            AddHealthIcon();
        }
    }

    private void InitHealthUI()
    {
        healthBar = new List<GameObject>();

        int health = playerRef.GetComponent<Maid>().GetHealth();

        for (int i = 0; i < health;  i++)
        {
            GameObject heart = Instantiate(healthTemplate, new Vector3(0, 0, 0), Quaternion.identity);
            heart.transform.SetParent(healthBarHolder.transform);
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
        GameObject heart = Instantiate(healthTemplate, new Vector3(0, 0, 0), Quaternion.identity);
        heart.transform.SetParent(healthBarHolder.transform);
        heart.GetComponent<RectTransform>().anchoredPosition = new Vector3(healthBar.Count * 100.0f, 0.0f, 0.0f);
        heart.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        healthBar.Add(heart);
    }

    private void UpdateCoins()
    {
        coinTextRef.text = playerRef.GetComponent<Maid>().GetCoins().ToString();
    }

    private void UpdateDrinks()
    {
        drinkImage.GetComponent<Image>().sprite = maidScript.Getdrink().uiSprite;
    }

    public void UpdateTimerText(string newTime)
    {
        timerTextRef.text = newTime;
    }

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        InitHealthUI();

        //Get component references.
        coinTextRef = coinText.GetComponent<Text>();
        maidScript = playerRef.GetComponent<Maid>();
        timerTextRef = timerText.GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("p"))
            TogglePauseMenu();

        UpdateHealth();
        UpdateCoins();
        UpdateDrinks();
    }

}
