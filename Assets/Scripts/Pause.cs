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
    public GameObject OnEnterSettings, OnExitSettings;

    //public GameObject GameUI;
    //public GameObject EtherealUI;
    //public GameObject GravityUI;
    void Start()
    {
        //GameUI.SetActive(false);

        if (Activators.isFullScreen)
        {
            ScreenSettings.isOn = true;
        }
        else
        {
            ScreenSettings.isOn = false;
        }
        PauseMenu.SetActive(false);
        IsActive = false;
        IsSettingActive = false;
        SettingsMenu.SetActive(false);
        FOVtext.text = Activators.defaultFOV.ToString();
        VolumeText.text = ((int)(Activators.volume * 100)).ToString();
        Sensitivitytext.text = ((int)(Activators.mouseSensitivity / 5)).ToString();
        FOVslider.value = Activators.defaultFOV;
        Volume.value = Activators.volume;
        Sensslider.value = Activators.mouseSensitivity;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsSettingActive)
        {
            if (!IsActive)
            {
                Time.timeScale = 0f;
                PauseMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(OnExitSettings);
                IsActive = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                PauseMenu.SetActive(false);
                IsActive = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = Activators.timeLine;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && IsSettingActive)
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
            PauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(OnExitSettings);
        }
        if (Input.GetKeyDown(KeyCode.Q) && Activators.level > 4)
        {
            Activators.gravityIndex++;
            Activators.gravityIndex = Activators.gravityIndex % 6;
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = Activators.timeLine;
        Activators.gravityIndex = 0;
    }
    public void SetSensitivity(float sensitivity)
    {
        Activators.mouseSensitivity = sensitivity;
        Sensitivitytext.text = ((int)(Activators.mouseSensitivity / 5)).ToString();
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
        IsActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = Activators.timeLine;
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("main menu");
    }
    public void Settings()
    {
        PauseMenu.SetActive(false);
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
        PauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(OnExitSettings);
    }
}
