using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public Camera playercamera;
    public GameObject PauseMenu;
    public GameObject EasyMenu;
    public GameObject SettingsMenu;
    private bool IsActive;
    private bool IsSettingActive;
    public AudioMixer LevelsMixer;
    public Slider Volume;
    public Toggle ScreenSettings;
    public Text VolumeText;
    public Text FOVtext;
    public Text Sensitivitytext;
    public Slider FOVslider;
    public Slider Sensslider;
    public GameObject OnEnterSettings, OnExitSettingsToHard, OnExitSettingToEasy;
    public GameObject GameUI;
    private int GravityIndex;
    public Image[] GravityArrows;

    void Start()
    {
        GravityIndex = 0;
        if (//Activators.level < 5 
            true)
        { 
            GameUI.SetActive(true);
        }
       
        if (Activators.isFullScreen)
        {
            ScreenSettings.isOn = true;
        }
        else
        {
            ScreenSettings.isOn = false;
        }
        PauseMenu.SetActive(false);
        EasyMenu.SetActive(false);
        IsActive = false;
        IsSettingActive = false;
        SettingsMenu.SetActive(false);
        FOVtext.text = Activators.defaultFOV.ToString();
        VolumeText.text = ((int)(Activators.volume * 100)).ToString();
        Sensitivitytext.text = ((int)(Activators.mouseSensitivity / 15)).ToString();
        FOVslider.value = Activators.defaultFOV;
        playercamera.fieldOfView = Activators.defaultFOV;
        Volume.value = Activators.volume;
        Sensslider.value = Activators.mouseSensitivity;
 
    }
    void Update()
    {
        for (int i = 0; i < GravityArrows.Length; i++)
        {
            if (GravityIndex == i)
            {
                GravityArrows[i].color = new Color(216f, 219f, 255f, 1f);
            }
            else
            {
                GravityArrows[i].color = new Color(216f, 219f, 255f, 0.5f);
            }
        }
        if (Activators.isEasy == 0)
        {
            PauseChanger(EasyMenu, OnExitSettingToEasy);
        }
        else
        {
            PauseChanger(PauseMenu, OnExitSettingsToHard);
        }
        if (//Activators.level > 4 && 
            Input.GetButtonDown("Change") )
        {
            GravityChanger();
        }
        if (//Activators.level > 4 && 
            Input.GetButtonDown("Apply"))
        {
            GravityApply();
        }
    }
    
    private void GravityApply()
    {
        Activators.gravityIndex++;
        Activators.gravityIndex %= 6;
    }
    private void GravityChanger()
    {
        GravityIndex++;
        GravityIndex %= 5;
    }
    private void PauseChanger(GameObject type, GameObject selected)
    {
        if (Input.GetButtonDown("Cancel") && !IsSettingActive)
        {
            if (!IsActive)
            {
                Time.timeScale = 0f;
                type.SetActive(true);
                EventSystem.current.SetSelectedGameObject(selected);
                IsActive = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                type.SetActive(false);
                IsActive = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
            }
        }
        if (Input.GetButtonDown("Cancel") && IsSettingActive)
        {
            SettingsMenu.SetActive(false);
            IsSettingActive = false;
            PlayerPrefs.SetFloat("sensitivity", Activators.mouseSensitivity);
            PlayerPrefs.SetFloat("volume", Activators.volume);
            PlayerPrefs.SetFloat("FOV", Activators.defaultFOV);
            if (Activators.isFullScreen)
            {
                Activators.forscreen = 0;
            }
            else
            {
                Activators.forscreen = 1;
            }
            PlayerPrefs.SetInt("FullScreen", Activators.forscreen);
            PlayerPrefs.Save();
            type.SetActive(true);
            EventSystem.current.SetSelectedGameObject(selected);
        }
    }
    public void Load(GameObject type)
    {
        if (Activators.IsSaved > 0)
        {
            type.SetActive(false);
            IsActive = false;
            Activators.IsLoaded = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        Activators.gravityIndex = 0;
    }
    public void SetSensitivity(float sensitivity)
    {
        Activators.mouseSensitivity = sensitivity;
        Sensitivitytext.text = ((int)(Activators.mouseSensitivity / 15)).ToString();
    }
    public void SetFOV(float FOV)
    {
        Activators.defaultFOV = FOV;
        FOVtext.text = Activators.defaultFOV.ToString();
        playercamera.fieldOfView = Activators.defaultFOV;

    }
    public void SetVolume(float volume)
    {
        Activators.volume = volume;
        LevelsMixer.SetFloat("Levels", Mathf.Log(Activators.volume) * 20);
        VolumeText.text = ((int)(Activators.volume * 100)).ToString();
    }
    public void FullScreenToggle(bool isfullscreen)
    {
        Activators.isFullScreen = isfullscreen;
        Screen.fullScreen = Activators.isFullScreen;
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        EasyMenu.SetActive(false);
        IsActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("main menu");
    }
    public void Settings()
    {
        PauseMenu.SetActive(false);
        EasyMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        IsSettingActive = true;
        EventSystem.current.SetSelectedGameObject(OnEnterSettings);
    }
    public void Back()
    {
        SettingsMenu.SetActive(false);
        IsSettingActive = false;
        PlayerPrefs.SetFloat("sensitivity", Activators.mouseSensitivity);
        PlayerPrefs.SetFloat("volume", Activators.volume);
        PlayerPrefs.SetFloat("FOV", Activators.defaultFOV);
        if (Activators.isFullScreen)
        {
            Activators.forscreen = 0;
        }
        else
        {
            Activators.forscreen = 1;
        }
        PlayerPrefs.SetInt("FullScreen", Activators.forscreen);
        PlayerPrefs.Save();

        if (Activators.isEasy == 0)
        {
            EasyMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(OnExitSettingToEasy);
        }
        else
        {
            PauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(OnExitSettingsToHard);
        }
        
    }
}
