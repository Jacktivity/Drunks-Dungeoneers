using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    //John Shone - 10/08/2018
    //Public Variables
    public Toggle fullScreenToggle;
    public Dropdown resDropdown;
    public Dropdown gameQuality;
    public Slider volumeSlider;
    public Text volumeText;
    public Button btnBack;

    public AudioSource musicSource;
    public Resolution[] resolutions;
    public SettingsClass settings;

    void Start ()
    {
        fullScreenToggle.onValueChanged.AddListener(delegate { OnToggleFullscreen(); });
        resDropdown.onValueChanged.AddListener(delegate { OnChangeResolution(); });
        gameQuality.onValueChanged.AddListener(delegate { OnChangeGameQuality(); });
        volumeSlider.onValueChanged.AddListener(delegate { OnChangeVolume(); });

        settings = new SettingsClass();

        resolutions = Screen.resolutions;

        foreach (Resolution resolution in resolutions)
        {
            //add each resolution to the dropdown box.
            resDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            //add each quality level to the game quality dropdown box.
            gameQuality.options.Add(new Dropdown.OptionData(QualitySettings.names[i].ToString()));
        }
        LoadUserSettings();
    }

    public void OnToggleFullscreen()
    {
        //We set the players fullscreen to equal the toggle option, and then the settings to equal the players fullscreen.
        settings.fullscreen = Screen.fullScreen = fullScreenToggle.isOn;
    }

    public void OnChangeResolution()
    {
        //First we set the settings class resoultion index to equal that of the dropdownbox value.
        settings.resolutionInd = resDropdown.value;
        //then we set the screens resolutions to equal that of the dropdownbox value.
        Screen.SetResolution(resolutions[resDropdown.value].width, resolutions[resDropdown.value].height, Screen.fullScreen);
    }

    public void OnChangeGameQuality()
    {
        //first we set the settings class quality level to equal the value of the gameQuality dropdownbox
        settings.qualityLevel = gameQuality.value;
        //then we set the QualitySettings quality level to equal the settings class quality level.
        QualitySettings.SetQualityLevel(gameQuality.value);

    }

    public void OnChangeVolume()
    {
        //first we set the settings class musicvolume to equal the volumeslider value.
        //then we set the musicSources volume to equal that of the settings class.
        musicSource.volume = settings.musicVolume = volumeSlider.value;
        volumeText.GetComponent<Text>().text = ("" + (volumeSlider.value * 100)); 
    }

    public void SaveUserSettings()
    {
        //next we save the user settings by creating a json file.
        string jsonData = JsonUtility.ToJson(settings, true);
        File.WriteAllText(Application.persistentDataPath + "/userSettings.json", jsonData);
    }

    public void LoadUserSettings()
    {
        //first we check to see if a settings file exists and if not then we manually,
        //set all the settings class values and run the save settings function to create a file.
        if (!File.Exists(Application.persistentDataPath + "/userSettings.json"))
        {
            settings.qualityLevel = 3;
            settings.fullscreen = true;
            settings.musicVolume = 0.5f;
            settings.resolutionInd = resolutions.Length;
            SaveUserSettings();
        }
        //once that is done we load the values from the json file and update all the options values.
        settings = JsonUtility.FromJson<SettingsClass>(File.ReadAllText(Application.persistentDataPath + "/userSettings.json"));
        musicSource.volume = volumeSlider.value = settings.musicVolume;
        resDropdown.value = settings.resolutionInd;
        gameQuality.value = settings.qualityLevel;
        Screen.fullScreen = fullScreenToggle.isOn = settings.fullscreen;
        QualitySettings.SetQualityLevel(settings.qualityLevel);
        resDropdown.RefreshShownValue();
        gameQuality.RefreshShownValue();
    }
}
